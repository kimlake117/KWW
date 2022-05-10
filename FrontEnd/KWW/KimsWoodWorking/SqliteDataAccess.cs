using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace KimsWoodWorking
{
    public class SqliteDataAccess
    {
        //returns a connection string from the web.config
        private static String LoadConnectionString(string id = "KWWDB")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //returns a list of the model you send it.
        public static List<T> LoadData<T>(string sql,DynamicParameters p)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Open();
                try
                {
                    return conn.Query<T>(sql, p).ToList();
                }
                catch (Exception ex) {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:/Users/Kim Lake/Documents/Job_Search_2022/Practice_Application/Source_Code/FrontEnd/KWW/KimsWoodWorking/Logs/KWW_Log.txt")) {
                        string error = DateTime.Now.ToString() + ex.Message;
                        streamWriter.WriteLine(error);
                    }
                    return null;
                }
            }
        }

        //returns number of rows affected
        public static int SaveData<T>(string sql, T data)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Open();
                try
                {
                    return conn.Execute(sql,data);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:/Users/Kim Lake/Documents/Job_Search_2022/Practice_Application/Source_Code/FrontEnd/KWW/KimsWoodWorking/Logs/KWW_Log.txt"))
                    {
                        string error = DateTime.Now.ToString() + ex.Message;
                        streamWriter.WriteLine(error);
                    }
                    return -1;
                }
            }
        }

        //returns number of rows affected
        public static int executeStatment(string sql, DynamicParameters p)
        {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    return conn.Execute(sql,p);
                }
                catch (Exception ex)
                {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("C:/Users/Kim Lake/Documents/Job_Search_2022/Practice_Application/Source_Code/FrontEnd/KWW/KimsWoodWorking/Logs/KWW_Log.txt"))
                    {
                        string error = DateTime.Now.ToString() + ex.Message;
                        streamWriter.WriteLine(error);
                    }
                    return -1;
                }
            }
        }
    }
}
