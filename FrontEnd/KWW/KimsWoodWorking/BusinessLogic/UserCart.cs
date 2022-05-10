using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;
using Dapper;

namespace KimsWoodWorking.BusinessLogic
{
    public static class UserCart
    {
        public static List<UserCartItemModel> getUserCart() {

            var p = new DynamicParameters();

            p.Add("@UserId", GlobalVariables.CurrentUser_id);

            string sql = @"select * from user_cart where user_id = @UserId;";

            //get the data from the database
            List<UserCartDBModel> q1_results = SqliteDataAccess.LoadData<UserCartDBModel>(sql,p);
            
            //create a final list of userCartItems, this is what will be returned
            List<UserCartItemModel> results = new List<UserCartItemModel>();

            //iterate through the data and create a new userCartItem
            foreach (UserCartDBModel item in q1_results) {

                UserCartItemModel userCartItem = new UserCartItemModel();

                //data from the user_cart table
                userCartItem.quantity = item.quantity;
                userCartItem.user_Id = item.user_id;

                p = new DynamicParameters();

                p.Add("@ProductID", item.product_id);

                sql = "select * from product where product_id = @ProductID";

                List<ProductDBModel> products = SqliteDataAccess.LoadData<ProductDBModel>(sql,p);

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

            var p = new DynamicParameters();

            p.Add("@UserId",user);
            p.Add("@ProductId",product);

            string sql = @"delete from user_cart where user_id = @UserId and product_id = @ProductId";

            return SqliteDataAccess.executeStatment(sql,p);
        }

        public static int updateCartItem(int user, int product, int quantity) {

            var p = new DynamicParameters();

            p.Add("@Quantity",quantity);
            p.Add("@UserID", user);
            p.Add("@ProductID", product);

            string sql = @"update user_cart set quantity = @Quantity where user_id = @UserID and product_id = @ProductID";

            return SqliteDataAccess.executeStatment(sql,p);
        }
    }
}