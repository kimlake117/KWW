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

        public static List<T> LoadData<T>(string sql) {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString())) {
                return conn.Query<T>(sql).ToList();
            }
        } 

        public static int SaveData<T>(string sql, T data) {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                return conn.Execute(sql,data);
            }
        }

        public static int executeStatment(string sql) {
            using (IDbConnection conn = new SQLiteConnection(LoadConnectionString()))
            {
                return conn.Execute(sql);
            }
        }
    }
}