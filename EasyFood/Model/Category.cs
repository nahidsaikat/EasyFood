using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFood.Model
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Deleted { get; set; }
    }

    public class CategoryManager
    {
        public static List<Category> getCategory()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Category>();

            int i = 1;
            List<Category> categoryList = new List<Category>();
            var queryCategory = conn.Table<Category>();
            foreach (var category in queryCategory)
            {
                categoryList.Add(new Category()
                {
                    Id = i,
                    Name = category.Name
                });
                i++;
            }

            return categoryList;
        }

        public static List<string> comboCategory()
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Category>();
            
            List<string> categoryList = new List<string>();
            var queryCategory = conn.Table<Category>();
            foreach (var category in queryCategory)
            {
                categoryList.Add(Convert.ToString(category.Name));
            }

            return categoryList;
        }

    }
}
