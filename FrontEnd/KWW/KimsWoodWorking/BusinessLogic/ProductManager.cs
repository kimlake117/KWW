using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;
using KimsWoodWorking.Models.databaseModels;
using Dapper;

namespace KimsWoodWorking.BusinessLogic
{
    public static class ProductManager
    {
        public static List<ProductModel> GetProductList() { 

            List<ProductModel> products = new List<ProductModel>();

            var p = new DynamicParameters();   

            string sql = @"select * from product where active = 1";

            List<ProductDBModel> models = SqliteDataAccess.LoadData<ProductDBModel>(sql,p);

            foreach (var model in models) { 
                ProductModel product = new ProductModel();

                product.product_id = model.product_id;
                product.product_name = model.product_name;
                product.product_price = model.product_price;
                product.description = model.description;
                product.active = model.active;
                if (model.photo is null)
                {
                    product.photo = "";
                }
                else
                {
                    product.photo = model.photo;
                }

                products.Add(product);
            }
            return products;
        }

    }
}