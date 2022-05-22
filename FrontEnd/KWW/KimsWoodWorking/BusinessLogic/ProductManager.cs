using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models;
using KimsWoodWorking.Models.ViewModels;
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
        public static List<ProductModel> GetALLProductList(string productNameSearchedFor)
        {

            List<ProductModel> products = new List<ProductModel>();

            var p = new DynamicParameters();

            p.Add("@ProductName","%"+productNameSearchedFor+"%");

            string sql = @"select * from product where upper(product_name) like upper(@ProductName)";

            List<ProductDBModel> models = SqliteDataAccess.LoadData<ProductDBModel>(sql, p);

            foreach (var model in models)
            {
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
        public static ProductModel GetProductByID(int product_id)
        {

            ProductModel product = new ProductModel();

            var p = new DynamicParameters();

            p.Add("@ProductID", product_id);

            string sql = @"select * from product where product_id = @ProductID";

            List<ProductDBModel> models = SqliteDataAccess.LoadData<ProductDBModel>(sql, p);


                product.product_id = models[0].product_id;
                product.product_name = models[0].product_name;
                product.product_price = models[0].product_price;
                product.description = models[0].description;
                product.active = models[0].active;
                if (product.photo is null)
                {
                    product.photo = "";
                }
                else
                {
                    product.photo = models[0].photo;
                }
            return product;
        }
        public static int updateProduct(EditProductViewModel vm)
        {

            if (vm.editedProduct.product_name != null | vm.editedProduct.product_price > -1 |
                vm.editedProduct.description != null | vm.editedProduct.active != -1)
            {
                DynamicParameters p = new DynamicParameters();
                p.Add("@ProductID", vm.editedProduct.product_id);

                string sql = @"update product set ";

                bool hasSomethingBefore = false;

                if (vm.editedProduct.product_name != null) {
                    p.Add("@ProductName", vm.editedProduct.product_name);
                    sql += " product_name = @ProductName ";
                    hasSomethingBefore = true;
                }
                if (vm.editedProduct.product_price > -1)
                {
                    if (hasSomethingBefore) {
                        sql += ",";
                    }
                    p.Add("@ProductPrice", vm.editedProduct.product_price);
                    sql += " product_price = @ProductPrice ";
                    hasSomethingBefore = true;
                }
                if (vm.editedProduct.description != null)
                {
                    if (hasSomethingBefore)
                    {
                        sql += ",";
                    }
                    p.Add("@ProductDecription", vm.editedProduct.description);
                    sql += " description = @ProductDecription ";
                    hasSomethingBefore = true;
                }
                if (vm.editedProduct.active != -1)
                {
                    if (hasSomethingBefore)
                    {
                        sql += ",";
                    }
                    p.Add("@Active", vm.editedProduct.active);
                    sql += " active = @Active ";
                }

                sql += " where product_id = @ProductID";

                return SqliteDataAccess.executeStatment(sql,p);

            }
            else
            {
                return 0;
            }
        }

        public static string getProductName(int product_id) { 
            DynamicParameters p = new DynamicParameters();

            p.Add("@ProductID", product_id);

            string sql = "select product_name from product where product_id = @ProductID";

            string result = "";

            List<string> list = SqliteDataAccess.LoadData<string>(sql, p);

            result = list[0];

            return result;
        }
    }
}