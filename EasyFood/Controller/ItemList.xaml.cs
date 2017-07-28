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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ItemList : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public ItemList()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Category>();
            loadListView();
        }

        public void loadListView()
        {
            int categoryCounter = 0, itemCounter = 0;
            var queryCategory = conn.Table<Category>();
            foreach (var category in queryCategory)
            {
                categoryCounter++;
                ListViewItem categoryListView = new ListViewItem();
                categoryListView.Name = category.Name;
                categoryListView.Content = categoryCounter + " " + category.Name;
                categoryListView.Margin = new Thickness(40, 5, 40, 0);
                categoryListView.Background = new SolidColorBrush(Windows.UI.Colors.LightGray);
                ItemListView.Items.Add(categoryListView);

                var queryItem = (from I in conn.Table<Item>()
                                 where I.CategoryName == category.Name
                                 select I).ToList();
                itemCounter = 0;
                foreach (var item in queryItem)
                {
                    itemCounter++;
                    ListViewItem itemListView = new ListViewItem();
                    itemListView.Name = item.Name;
                    itemListView.Content = categoryCounter + "." + itemCounter + " " + item.Name;
                    itemListView.Margin = new Thickness(60, 5, 60, 0);
                    itemListView.Background = new SolidColorBrush(Windows.UI.Colors.WhiteSmoke);
                    ItemListView.Items.Add(itemListView);
                }
            }
        }
    }
}

/*
    Button myButton = new Button();
    myButton.Name = "ClickMeButton";
    myButton.Content = "Click Me";
    myButton.Width = 200;
    myButton.Height = 100;
    myButton.Margin = new Thickness(20, 20, 0, 0);
    myButton.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
    myButton.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;

    myButton.Background = new SolidColorBrush(Windows.UI.Colors.Red);
    myButton.Click += ClickMeButton_Click;

    LayoutGrid.Children.Add(myButton);
*/
