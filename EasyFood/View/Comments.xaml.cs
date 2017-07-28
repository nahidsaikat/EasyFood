using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using EasyFood.Model;
using EasyFood.Controller;
using System.Collections.ObjectModel;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Comments : Page
    {
        public List<string> categoryList;
        public List<string> itemList;
        string comboCategory = string.Empty;
        string comboItem = string.Empty;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public Comments()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            categoryList = CategoryManager.comboCategory();
            CategoryComboBox.ItemsSource = categoryList;

            itemList = ItemManager.getItem(string.Empty);
            ItemComboBox.ItemsSource = itemList;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine((sender as ComboBox).SelectedValue);
            
            itemList = ItemManager.getItem((sender as ComboBox).SelectedValue.ToString());
            ItemComboBox.ItemsSource = itemList;

            comboCategory = CategoryComboBox.SelectedValue.ToString();
        }

        private void SubmitCommentButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(Login.userName);
            DateTime d = DateTime.Now;
            Debug.WriteLine(d.Day + "-" + d.Month + "-" + d.Year);
            Debug.WriteLine(d.Hour + "-" + d.Minute + "-" + d.Second);
            
            List<Item> item = (from I in conn.Table<Item>()
                         where I.Name == comboItem &&
                         I.CategoryName == comboCategory
                               select I).ToList<Item>();

            Debug.WriteLine(item.ToArray<Item>());
            int categoryId = item[0].CategoryId;
            int itemId = item[0].Id;
            int userId = Login.userId;
            string userName = Login.userName;
            string date = (d.Day + "-" + d.Month + "-" + d.Year).ToString();
            string time = (d.Hour + "-" + d.Minute + "-" + d.Second).ToString();
            string comment = CommentTextBox.Text;

            if(comment != string.Empty)
            {
                conn.CreateTable<Comment>();
                conn.Insert(new Comment()
                {
                    CategoryId = categoryId,
                    ItemId = itemId,
                    UserId = userId,
                    CategoryName = comboCategory,
                    ItemName = comboItem,
                    UserName = userName,
                    Date = date,
                    Time = time,
                    Comments = comment
                });

                CommentTextBox.Text = string.Empty;

                WarningTextBlock.Text = "Thank you for your comment.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
            }
            else
            {
                WarningTextBlock.Text = "Please do some comment then submit!";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
        }

        private void ItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboItem = ItemComboBox.SelectedValue.ToString();
        }
    }
}
