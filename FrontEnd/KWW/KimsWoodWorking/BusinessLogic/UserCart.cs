using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;

namespace KimsWoodWorking.BusinessLogic
{
    public static class UserCart
    {
        public static List<UserCartItemModel> getUserCart() {

            string sql = @"select * from user_cart where user_id = "+GlobalVariables.CurrentUser_id+";";

            //get the data from the database
            List<UserCartDBModel> q1_results = SqliteDataAccess.LoadData<UserCartDBModel>(sql);
            
            //create a final list of userCartItems, this is what will be returned
            List<UserCartItemModel> results = new List<UserCartItemModel>();

            //iterate through the data and create a new userCartItem
            foreach (UserCartDBModel item in q1_results) {

                UserCartItemModel userCartItem = new UserCartItemModel();

                //data from the user_cart table
                userCartItem.quantity = item.quantity;
                userCartItem.user_Id = item.user_id;

                sql = "select * from product where product_id =" + item.product_id;

                List<ProductDBModel> products = SqliteDataAccess.LoadData<ProductDBModel>(sql);

                //data from the product table
                userCartItem.product_id = products[0].product_id;
                userCartItem.product_name = products[0].product_name;
                userCartItem.product_price = products[0].product_price;

                //add newly created item to frinal results list
                results.Add(userCartItem);
            }
            return results;
        }

        public static double UserCartTotalValue()
        {
            List<UserCartItemModel> UserCart = getUserCart();

            double total = 0.0;

            foreach (UserCartItemModel item in UserCart)
            {
                double quantityTotal = item.quantity * item.product_price;

                total = total + quantityTotal;
            }
            return total;
        }

        public static int deleteCartItem(int user, int product) {
            string sql = @"delete from user_cart where user_id = " + user + " and product_id = " + product;

            return SqliteDataAccess.executeStatment(sql);
        }

        public static int updateCartItem(int user, int product, int quantity) {
            string sql = @"update user_cart set quantity = " + quantity + " where user_id = " + user + " and product_id = " + product;

            return SqliteDataAccess.executeStatment(sql);
        }

        public static int emptyUserCart() {
            string sql = "delete from user_cart where user_id = " + GlobalVariables.CurrentUser_id;

            return SqliteDataAccess.executeStatment(sql);
        }
    }
}