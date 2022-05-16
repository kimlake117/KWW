using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;
using System.Security.Cryptography;
using Dapper;

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

            int rowsInserted =  SqliteDataAccess.SaveData(sql, userDBModel);

            //need this to get the userid we just created.
            UserModel userModel = new UserModel();
            userModel.UserName = username;

            rowsInserted += insertUserRole(getUserId(userModel));

            return rowsInserted;
        }
        private static int insertUserRole(int user_id) { 
            var p = new DynamicParameters();

            p.Add("@UserID", user_id);

            string sql = "insert into user_role(user_id, role_id) values(@UserID,1)";

            return SqliteDataAccess.executeStatment(sql, p);
        }
        public static int UpdateUserEmail(string newEmail) {
            UserDBModel userDBModel =new UserDBModel();

            userDBModel.UserName = GlobalVariables.currentUser.UserName;
            userDBModel.Email = newEmail;
            string sql = "update user set email = @email where user_name = @UserName";

            return SqliteDataAccess.SaveData(sql, userDBModel);
        }

        public static int UpdateUserPassword(string newPassword) {
            UserDBModel userDBModel =new UserDBModel();

            userDBModel.Password = HashPassword(newPassword);
            userDBModel.UserName = GlobalVariables.currentUser.UserName;

            string sql = "update user set password = @Password where user_name = @UserName";

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

            var p = new DynamicParameters();

            p.Add("@UserName", user.UserName);

            string sql = "select * from user where user_name = @UserName;";

            List<UserDBModel> storedUser = SqliteDataAccess.LoadData<UserDBModel>(sql,p);

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

        public static int getUserId(UserModel user) {

            var p = new DynamicParameters();

            p.Add("@UserName",user.UserName);

            string sql = "select * from user where user_name = @UserName;";

            List<UserDBModel> q_1 = SqliteDataAccess.LoadData<UserDBModel>(sql,p);

            return q_1.First().user_id;
        }

        public static List<RoleDBModel> getUserRoles() { 
            List<RoleDBModel> roles = new List<RoleDBModel>();
            if (GlobalVariables.currentUser.isSignedIn)
            {
                var p = new DynamicParameters();

                p.Add("@UserID", GlobalVariables.currentUser.user_id);

                string sql = @"select * from user_role 
                                inner join role on role.role_id = user_role.role_id
                                where user_id = @UserID";

                roles = SqliteDataAccess.LoadData<RoleDBModel>(sql,p);

                return roles;
            }
            else
            {
                return roles;
            }
        }
        // 1 = User; 2 = Admin; 3 = Site Focal
        public static bool userHasRole(UserModel user,int role) {
            foreach (var item in user.roleList) {
                if (item.role_id == role) { 
                    return true;
                }
            }
            return false;
        }
    }
}