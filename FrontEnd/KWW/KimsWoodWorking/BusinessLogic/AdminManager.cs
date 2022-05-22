using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using static KimsWoodWorking.BusinessLogic.UserAccountManager;
using KimsWoodWorking.Models.ViewModels;
using Dapper;

namespace KimsWoodWorking.BusinessLogic
{
    public static class AdminManager
    {
        public static int changeUserPassword(ChangeUserPWViewModel vm) {
            string newPassword = HashPassword(vm.newPassword);

            DynamicParameters p = new DynamicParameters();

            p.Add("@NewPassword", newPassword);
            p.Add("@UserID", vm.selectedUserID);

            string sql = "update user set password = @NewPassword where user_id = @UserID";

            return SqliteDataAccess.executeStatment(sql, p);
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

        public static int deleteUserAccount(int user_id) { 
            DynamicParameters p = new DynamicParameters();
            p.Add("@UserID", user_id);

            string sql = @"update user set active = 0 where user_id = @UserID";

            return SqliteDataAccess.executeStatment(sql, p);

        }
    }
}