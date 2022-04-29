using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;

namespace KimsWoodWorking.BusinessLogic
{
    public static class UserAccount
    {
        public static int CreateNewUser(string username, string email, string password) { 

            UserDBModel userDBModel = new UserDBModel();

            userDBModel.UserName = username;
            userDBModel.Email = email;
            userDBModel.Password = password;

            string sql = @"insert into user(user_name,email,password)
                            values(@UserName, @Email, @Password)";

            return SqliteDataAccess.SaveData(sql, userDBModel);
        }

    }
}