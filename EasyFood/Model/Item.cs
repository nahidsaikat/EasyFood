using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFood.Model
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public double Prize { get; set; }
        public int Available { get; set; }
        public int Deleted { get; set; }
    }

    public class ItemManager
    {
        public static List<string> getItem(string categoryName)
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Item>();

            List<string> itemList = new List<string>();
            if (categoryName != string.Empty)
            {
                itemList = (from item in conn.Table<Item>()
                            where item.CategoryName == categoryName
                            select item.Name).ToList<string>();
            }
            else
            {
                itemList = (from item in conn.Table<Item>()
                            select item.Name).ToList<string>();
            }
            return itemList;
        }

        public static List<Item> getFullItem(string categoryName)
        {
            string path;
            SQLite.Net.SQLiteConnection conn;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Item>();

            List<Item> itemList = new List<Item>();
            if (categoryName != string.Empty)
            {
                itemList = (from item in conn.Table<Item>()
                            where item.CategoryName == categoryName && item.Available == 1
                            select item).ToList<Item>();
            }
            else
            {
                itemList = conn.Table<Item>().ToList<Item>();
            }
            return itemList;
        }
    }
}
