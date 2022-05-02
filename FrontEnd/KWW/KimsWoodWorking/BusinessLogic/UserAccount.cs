using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;
using System.Security.Cryptography;

namespace KimsWoodWorking.BusinessLogic
{
    public static class UserAccount
    {
        public static int CreateNewUser(string username, string email, string password) { 

            UserDBModel userDBModel = new UserDBModel();

            userDBModel.UserName = username;
            userDBModel.Email = email;
            userDBModel.Password = HashPassword(password);

            string sql = @"insert into user(user_name,email,password)
                            values(@UserName, @Email, @Password)";

            return SqliteDataAccess.SaveData(sql, userDBModel);
        }

        private static string HashPassword(string pw)
        {
            string salt = "a4fgep57Vs2Q";
            if (String.IsNullOrEmpty(pw))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(pw + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }

        public static Boolean pwMatch(UserModel user) {

            string sql = "select * from user where user_name = '"+user.UserName+"';";

            List<UserDBModel> storedUser = SqliteDataAccess.LoadData<UserDBModel>(sql);

            //if there are no results in the query, return false
            if (storedUser.Count == 0) {
                return false;
            }

            string storedHash = storedUser.First().Password;
            string submittedPWHash = HashPassword(user.Password);

            if (storedHash == submittedPWHash)
            {
                return true;
            }
            else {
                return false;
            }

        }

    }
}