using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static KimsWoodWorking.BusinessLogic.UserCart;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;
using Dapper;
using System.Data.SQLite;
using System.Configuration;
using System.Data;

namespace KimsWoodWorking.BusinessLogic
{
    public static class OrderManager
    {
        //since SQlite3 does not have stored procedures everything needs to happen here.
        public static int createOrder(CheckOutModel com)
        {
            //open connection
            using (IDbConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["KWWDB"].ConnectionString))
            {
                conn.Open();
                //start transaction
                using (IDbTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //get the user cart
                        List<UserCartItemModel> cartItems = getUserCart();

                        //if the user does not have items in their cart
                        if (cartItems == null)
                        {
                            return -1;
                        }

                        //get the order total cost
                        double cost = UserCartTotalValue();
                        //get the current date

                        var p = new DynamicParameters();

                        p.Add("@UserID", GlobalVariables.CurrentUser_id);
                        p.Add("@TotalOrderCost", cost);

                        //insert user_id, order_status of new, total cost, and the data into the parent order table
                        string sql = @"insert into parent_order(user_id,order_status_id,total_order_cost,order_date,status_date) 
                          values(@UserID," + 1 + ",@TotalOrderCost,\"" + DateTime.Now.ToString() + "\",\"" + DateTime.Now.ToString() + "\")";

                        conn.Execute(sql,p);

                        //get the parent order id of the record that was just created
                        sql = "select parent_order_id from parent_order where user_id = @UserID and shipping_detail_id is NULL and total_order_cost = @TotalOrderCost";

                        List<queryResult> queryResults = conn.Query<queryResult>(sql,p).ToList();

                        int parent_order_id = queryResults[0].parent_order_id;

                        //insert into order_details
                        foreach (UserCartItemModel item in cartItems)
                        {
                            p = new DynamicParameters();

                            p.Add("@ParentOrderID", parent_order_id);
                            p.Add("@ProductID", item.product_id);
                            p.Add("@Quantity", item.quantity);

                            sql = "insert into order_detail(parent_order_id, product_id,quantity) values(@ParentOrderID,@ProductID,@Quantity)";

                            conn.Execute(sql,p);
                        }

                        p = new DynamicParameters();

                        //insert into shipping_detail
                        string s_name = com.customer_first_name + " " + com.customer_last_name;

                        p.Add("@ParentOrderID", parent_order_id);
                        p.Add("@ShippingName", s_name);
                        p.Add("@ShippingStreetAddress",com.shipping_street_address);
                        p.Add("@ShippingCity",com.shipping_city);
                        p.Add("@ShippingState",com.shipping_state);
                        p.Add("@ShippingPostalCode", com.shipping_postal_code);

                        sql = @"insert into shipping_detail(parent_order_id,shipping_name,shipping_street_address,shipping_city,shipping_state,shipping_postal_code) 
                                values(@ParentOrderID,@ShippingName,@ShippingStreetAddress,@ShippingCity,@ShippingState,@ShippingPostalCode)";

                        conn.Execute(sql,p);

                        //insert into Billing details
                        string b_name = com.billing_first_name + " " + com.billing_last_name;

                        p = new DynamicParameters();

                        p.Add("@ParentOrderID", parent_order_id);
                        p.Add("@BillingStreetAddress", com.billing_street_address);
                        p.Add("@BillingCity",com.billing_city);
                        p.Add("@BillingState", com.billing_state);
                        p.Add("@BillingPostalCode", com.billing_postal_code);
                        p.Add("@CCNumber", com.cc_number);
                        p.Add("@CCExpMonth",com.cc_exp_month);
                        p.Add("@CCExpYear", com.cc_exp_year);
                        p.Add("@CCCvc",com.cc_cvc);
                        p.Add("@BillingName", b_name);


                        sql = @"insert into billing_detail(parent_order_id,billing_street_address,billing_city,billing_state,billing_postal_code,cc_number,cc_exp_month,cc_exp_year,cc_cvc,billing_name)
                                values(@ParentOrderID,@BillingStreetAddress,@BillingCity,@BillingState,@BillingPostalCode,@CCNumber,@CCExpMonth,@CCExpYear,@CCCvc,@BillingName)";

                        conn.Execute(sql,p);

                        p = new DynamicParameters();

                        p.Add("@ParentOrderId", parent_order_id);

                        //update parent order with shipping_id
                        sql = "select shipping_detail_id from shipping_detail where parent_order_id = @ParentOrderId";

                        queryResults = conn.Query<queryResult>(sql,p).ToList();

                        int shipping_detail_id = queryResults[0].shipping_detail_id;

                        p.Add("@ShippingDetailId", shipping_detail_id);

                        sql = "update parent_order set shipping_detail_id = @ShippingDetailId where parent_order_id = @ParentOrderId";

                        conn.Execute(sql,p);

                        //update parent order with billing_id
                        sql = "select billing_detail_id from billing_detail where parent_order_id = @ParentOrderId";

                        queryResults = conn.Query<queryResult>(sql,p).ToList();

                        int billing_detail_id = queryResults[0].billing_detail_id;

                        p.Add("@BillingDetailID", billing_detail_id);

                        sql = "update parent_order set billing_detail_id = @BillingDetailID where parent_order_id = @ParentOrderId";

                        conn.Execute(sql,p);

                        //ToDo:
                        //email user

                        //delete user cart
                        p = new DynamicParameters();
                        p.Add("@UserID", GlobalVariables.CurrentUser_id);

                        sql = "delete from user_cart where user_id = @UserID";

                        conn.Execute(sql, p);

                        //commit the changes
                        trans.Commit();

                        return 1;
                    }
                    //if there is an exception, rollback the transaction
                    catch (Exception e)
                    {
                        trans.Rollback();
                        using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:/Users/Kim Lake/Documents/Job_Search_2022/Practice_Application/Source_Code/FrontEnd/KWW/KimsWoodWorking/Logs/KWW_Log.txt"))
                        {
                            string error = DateTime.Now.ToString() + e.Message;
                            streamWriter.WriteLine(error);
                        }
                        
                        return -1;
                    }
                }
            }
        }

        public static List<OrderSummaryModel> getUserOrders() { 
            //the final list that will be returned
            List<OrderSummaryModel> orders = new List<OrderSummaryModel>();

            var p = new DynamicParameters();

            p.Add("@UserID",GlobalVariables.CurrentUser_id);

            string sql = @"select PO.parent_order_id,PO.total_order_cost,order_date, order_status_description, status_date, shipping_name, shipping_street_address, shipping_city,state_name, shipping_postal_code
                            from parent_order PO INNER JOIN shipping_detail SD on PO.parent_order_id = SD.parent_order_id
                            inner join order_status OS on OS.order_status_id = PO.order_status_id
                            inner join state on state.state_id = SD.shipping_state
                            where user_id = @UserID";

            List<queryResult> queryResults = SqliteDataAccess.LoadData<queryResult>(sql,p);

            //translate the query result list into ordersumarrymodel and add them to the final list
            foreach (queryResult result in queryResults) { 
                OrderSummaryModel orderSummaryModel = new OrderSummaryModel();

                orderSummaryModel.parent_order_id = result.parent_order_id;
                orderSummaryModel.total_order_cost = result.total_order_cost;
                orderSummaryModel.order_date = result.order_date;
                orderSummaryModel.order_status_description = result.order_status_description;
                orderSummaryModel.status_date = result.status_date;
                orderSummaryModel.shipping_name = result.shipping_name;
                orderSummaryModel.shipping_street_address = result.shipping_street_address;
                orderSummaryModel.shipping_city = result.shipping_city;
                orderSummaryModel.shipping_state = result.state_name;
                orderSummaryModel.shipping_postal_code = result.shipping_postal_code;

                orders.Add(orderSummaryModel);
            }

            return orders;
        }

        public static List<OrderDetailItemModel> getOrderDetails(int PO) { 
            //the final list to be returned
            List<OrderDetailItemModel> orderDetails = new List<OrderDetailItemModel>();

            var p = new DynamicParameters();

            p.Add("@ParentOrderID",PO);

            string sql = @"select product_name,product_price,quantity
                            from order_detail OD
                            inner join product P on p.product_id = OD.product_id
                            where parent_order_id = @ParentOrderID";

            List<queryResult> queryResults = SqliteDataAccess.LoadData<queryResult>(sql,p);

            //translate, add to list, and return
            foreach (queryResult item in queryResults) {
                OrderDetailItemModel orderDetailItemModel = new OrderDetailItemModel();

                orderDetailItemModel.product_name = item.product_name;
                orderDetailItemModel.product_price = item.product_price;
                orderDetailItemModel.quantity = item.quantity;

                orderDetails.Add(orderDetailItemModel);
            }

            return orderDetails;
        }
        private class queryResult
        {
            public int parent_order_id { get; set; }
            public int shipping_detail_id { get; set; }
            public int billing_detail_id { get; set; }
            public string order_date { get; set; }
            public string order_status_description { get; set; }
            public string status_date { get; set; }
            public string shipping_name { get; set; }
            public string shipping_street_address { get; set; }
            public string shipping_city { get; set; }
            public string state_name { get; set; }
            public int shipping_postal_code { get; set; }
            public double total_order_cost { get; set; }
            public double product_price { get; set; }
            public int quantity { get; set; }
            public string product_name { get; set; }
        }
    }
}