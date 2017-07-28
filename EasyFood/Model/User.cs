using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFood.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Type { get; set; }
    }

    public class UserManager
    {
        public static List<User> getUsers()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();

            int i = 1;
            List<User> userList = new List<User>();
            var queryUser = conn.Table<User>();
            foreach (var user in queryUser)
            {
                userList.Add(new User() {
                    Id = i,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    ContactNo = user.ContactNo,
                    Type = user.Type
                });
                i++;
            }
            return userList;
        }
        public static List<string> getUserName()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();
            
            List<string> userName = new List<string>();
            var queryUser = conn.Table<User>();
            foreach (var user in queryUser)
            {
                userName.Add(user.UserName);
            }
            return userName;
        }
    }
}
