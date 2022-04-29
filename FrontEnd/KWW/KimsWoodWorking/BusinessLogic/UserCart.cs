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

            string sql = @"select * from user_cart";

            List<UserCartDBModel> q1_results = SqliteDataAccess.LoadData<UserCartDBModel>(sql);

            List<UserCartItemModel> results = new List<UserCartItemModel>();

            foreach (UserCartDBModel item in q1_results) {

                UserCartItemModel userCartItem = new UserCartItemModel();
                userCartItem.quantity = item.quantity;
                userCartItem.Id = item.user_id;

                sql = "select * from product where product_id =" + item.product_id;

                List<ProductDBModel> products = SqliteDataAccess.LoadData<ProductDBModel>(sql);

                ProductModel  convertedProductModel = new ProductModel();

                convertedProductModel.product_id = products[0].product_id;
                convertedProductModel.product_name = products[0].product_name;
                convertedProductModel.product_price = products[0].product_price;
                convertedProductModel.description = products[0].description;
                convertedProductModel.photo = products[0].photo;

                userCartItem.Product = convertedProductModel;

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
                double quantityTotal = item.quantity * item.Product.product_price;

                total = total + quantityTotal;
            }
            return total;
        }
    }
}