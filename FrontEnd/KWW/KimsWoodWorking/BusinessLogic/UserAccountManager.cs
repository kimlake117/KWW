using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KimsWoodWorking.Models.databaseModels;
using KimsWoodWorking.Models;
using System.Security.Cryptography;
using Dapper;
using KimsWoodWorking.Models.ViewModels;

namespace KimsWoodWorking.BusinessLogic
{
    public static class UserAccountManager
    {
        public static int CreateNewUser(string username, string email, string password) {

            DynamicParameters p = new DynamicParameters();

            p.Add("@UserName", username);
            p.Add("@Email", email);
            p.Add("@Password", HashPassword(password));

            string sql = @"insert into user(user_name,email,password,Active)
                            values(@UserName, @Email, @Password,1)";

            int rowsInserted =  SqliteDataAccess.executeStatment(sql, p);

            //need this to get the userid we just created.
            UserModel userModel = new UserModel
            {
                UserName = username
            };

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
            UserDBModel userDBModel = new UserDBModel
            {
                user_name = GlobalVariables.currentUser.UserName,
                email = newEmail
            };

            string sql = "update user set email = @email where user_name = @UserName";

            return SqliteDataAccess.SaveData(sql, userDBModel);
        }

        public static int UpdateUserPassword(string newPassword) {
            UserDBModel userDBModel = new UserDBModel
            {
                password = HashPassword(newPassword),
                user_name = GlobalVariables.currentUser.UserName
            };

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

            string storedHash = storedUser.First().password;
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

        public static string getUserName(int user_id) {
            string name = "";

            DynamicParameters p = new DynamicParameters();

            p.Add("@UserID", user_id);

            string sql = "select user_name from user where user_id = @UserID";

            List<string> results = SqliteDataAccess.LoadData<string>(sql,p);

            if (results.Count > 0) {
                name = results[0];
            } 
            return name;
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
        // 1 = User; 2 = Admin; 3 = Manager
        public static bool userHasRole(UserModel user,int role) {
            foreach (var item in user.roleList) {
                if (item.role_id == role) { 
                    return true;
                }
            }
            return false;
        }
        //returns a list of users based on the username entered by the Admin 
        public static List<UserDBModel> getUserList(string userSearchedFor) {

            DynamicParameters p = new DynamicParameters();

            p.Add("@UserName", "%"+userSearchedFor+"%");

            string sql = "select user_id,user_name,email,active from user where upper(user_name) like upper(@UserName) and active = 1";

            List<UserDBModel> results = SqliteDataAccess.LoadData<UserDBModel>(sql,p);

            return results;
        }
    }
}