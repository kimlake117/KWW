using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using KimsWoodWorking.Models;

namespace KimsWoodWorking
{
    public class SqliteDataAccess
    {
        private static String LoadConnectionString(string id = "KWWDB") {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //returns a list of products in a users cart
        public static List<UserCartItemModel> getUserCart() {
           
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())){
                 var output = cnn.Query<rawUserCartItem>("select * from user_cart", new DynamicParameters());

                //the database gives us a product id and the front end is expecting a product model, so we need to convert.
                List<UserCartItemModel> result = convertUserCartItem(output.ToList());
               
                return result;
            }
        }

        //converts the database version of user cart to a more useful version
        //in the database the product is a product id. but in the converted front end model,
        //the product is a product model. so we can use its attributes easier. 
        private static List<UserCartItemModel> convertUserCartItem(List<rawUserCartItem> input) {

            List<UserCartItemModel> result = new List<UserCartItemModel>();

            foreach (rawUserCartItem item in input) {
                using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {

                    //query the database for the product
                    var q_result = cnn.Query<ProductModel>("select * from product where product_id ="+item.product_id, new DynamicParameters());

                    //convert the results to a list
                    List<ProductModel> q_result_list = q_result.ToList();

                    //create a new product model to transfer the data from the list to
                    ProductModel convertedProductModel = new ProductModel();

                    //transfer data from query list to product model
                    convertedProductModel.product_price = Convert.ToDouble(q_result_list[0].product_price);
                    convertedProductModel.product_name = q_result_list[0].product_name;
                    convertedProductModel.description = q_result_list[0].description;
                    convertedProductModel.photo = q_result_list[0].photo;
                    convertedProductModel.product_id = Convert.ToInt32(q_result_list[0].product_id);

                    //make a new userCartItemModel to add to the overall results list
                    UserCartItemModel userCartItemModel = new UserCartItemModel();

                    //fill in the new model with the data it needs
                    userCartItemModel.Product = convertedProductModel;
                    userCartItemModel.quantity = item.quantity;
                    userCartItemModel.Id = item.user_id;

                    //add filled in model to the overall results list
                    result.Add(userCartItemModel);

                }
            }
            return result;
        }

        //returns the total dollar amount of the users cart
        public static double UserCartTotalValue() {
            List<UserCartItemModel> UserCart = getUserCart();

            double total = 0.0;

            foreach (UserCartItemModel item in UserCart) { 
                double quantityTotal = item.quantity * item.Product.product_price;

                total = total + quantityTotal;
            }
            return total;
        }

        private class rawUserCartItem{
            public int user_id { get; set; }
            public int product_id { get; set; }
            public int quantity { get; set; }
        }
    }
}