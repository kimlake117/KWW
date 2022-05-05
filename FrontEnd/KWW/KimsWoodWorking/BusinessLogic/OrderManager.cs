using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static KimsWoodWorking.BusinessLogic.UserCart;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;

namespace KimsWoodWorking.BusinessLogic
{
    public static class OrderManager
    {
        public static int createOrder(CheckOutModel com)
        {
            //get the user cart
            List<UserCartItemModel> cartItems = getUserCart();

            //if the user does not have items in their cart
            if (cartItems == null) {
                return -1;
            }

            //get the order total cost
            double cost = UserCartTotalValue();
            //get the current date

            //insert user_id, order_status of new, total cost, and the data into the parent order table
            string sql = @"insert into parent_order(user_id,order_status_id,total_order_cost,order_date,status_date) 
                          values(" + GlobalVariables.CurrentUser_id + "," + 1 + "," + cost + ",\"" + DateTime.Now.ToString() + "\",\"" + DateTime.Now.ToString() + "\")";

            SqliteDataAccess.executeStatment(sql);

            //get the parent order id of the record that was just created
            sql = "select parent_order_id from parent_order where user_id = " + GlobalVariables.CurrentUser_id + " and shipping_detail_id is NULL and total_order_cost = " + cost;

            List<queryResult> queryResults = SqliteDataAccess.LoadData<queryResult>(sql);

            int parent_order_id = queryResults[0].parent_order_id;

            //insert into order_details
            foreach (UserCartItemModel item in cartItems) {
                sql = "insert into order_detail(parent_order_id, product_id,quantity) values(" + parent_order_id + "," + item.product_id + "," + item.quantity + ")";

                SqliteDataAccess.executeStatment(sql);
            }

            //insert into shipping_detail
            string s_name = com.customer_first_name + " " + com.customer_last_name;
            sql = @"insert into shipping_detail(parent_order_id,shipping_name,shipping_street_address,shipping_city,shipping_state,shipping_postal_code) 
                    values("+ parent_order_id+",\""+ s_name+"\",\""+com.shipping_street_address+"\",\""+com.shipping_city+"\","+ com.shipping_state+","+com.shipping_postal_code+")";

            SqliteDataAccess.executeStatment(sql);

            //insert into Billing details
            string b_name = com.billing_first_name + " " + com.billing_last_name;
            sql = @"insert into billing_detail(parent_order_id,billing_street_address,billing_city,billing_state,billing_postal_code,cc_number,cc_exp_month,cc_exp_year,cc_cvc,billing_name)
                    values(" + parent_order_id + ",\"" + com.billing_street_address + "\",\"" + com.billing_city + "\"," + com.billing_state + "," + com.billing_postal_code + "," + com.cc_number + "," +
                    com.cc_exp_month + "," + com.cc_exp_year + "," + com.cc_cvc + ",\"" + b_name + "\")";

            SqliteDataAccess.executeStatment(sql);

            //update parent order with shipping_id
            sql = "select shipping_detail_id from shipping_detail where parent_order_id = " + parent_order_id;

            queryResults = SqliteDataAccess.LoadData<queryResult>(sql);

            int shipping_detail_id = queryResults[0].shipping_detail_id;

            sql = "update parent_order set shipping_detail_id = " + shipping_detail_id + " where parent_order_id = " + parent_order_id;

            SqliteDataAccess.executeStatment(sql);

            //update parent order with billing_id
            sql = "select billing_detail_id from billing_detail where parent_order_id = " + parent_order_id;

            queryResults = SqliteDataAccess.LoadData<queryResult>(sql);

            int billing_detail_id = queryResults[0].billing_detail_id;

            sql = "update parent_order set billing_detail_id = "+ billing_detail_id + " where parent_order_id = " + parent_order_id;

            SqliteDataAccess.executeStatment(sql);

            //ToDo:
            //email user

            //delete user cart
            emptyUserCart();

            return 1;

            
        }
        private class queryResult
        {
            public int parent_order_id { get; set; }
            public int shipping_detail_id { get; set; }
            public int billing_detail_id { get; set; }

        }
    }
}