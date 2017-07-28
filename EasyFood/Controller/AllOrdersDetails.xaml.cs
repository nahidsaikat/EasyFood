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
using System.Diagnostics;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllOrdersDetails : Page
    {
        // This variable is assigned in OrderList.xaml.cs file
        public static int orderId = -1;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public AllOrdersDetails()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            LoadDetails();
        }

        private void LoadDetails()
        {
            Order order = (from O in conn.Table<Order>()
                           where O.Id == orderId
                           select O).First<Order>();

            OrderNoTextBlock.Text = order.OrderNo.ToString();
            OrderDateTextBlock.Text = "Order date: " + order.OrderDate;
            OrderTimeTextBlock.Text = "Order Time: " + order.OrderTime;
            OrderByTextBlock.Text = "Order By: " + (from user in conn.Table<User>()
                                     where user.Id == order.UserId
                                     select user).First<User>().UserName;
            TotalCostTextBlock.Text = "Total Cost: " + order.TotalAmount;
            ProcessTimeTextBlock.Text = "Processing Time: " + order.ProcessTime;
            AcknowledgeCheckBox.IsChecked = order.Acknowledged == 1 ? true : false;
            DeliveryCheckBox.IsChecked = order.Deliveried == 1 ? true : false;
            CancelCheckBox.IsChecked = order.Canceled == 1 ? true : false;

            var orderItemList = (from orderItem in conn.Table<OrderItem>()
                                 where orderItem.OrderId == order.Id
                                 select orderItem).ToList<OrderItem>();
            int counter = 1;
            foreach(var orderItem in orderItemList)
            {
                string itemName = (from item in conn.Table<Item>()
                                   where item.Id == orderItem.ItemId
                                   select item).First<Item>().Name;
                TextBlock textblock = new TextBlock();
                textblock.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
                textblock.Text = counter++ + ". " + itemName + " " + orderItem.Price + "tk. Quantity: " + orderItem.Quantity + " Total: " + orderItem.Total;
                OrderItemsListView.Items.Add(textblock);
            }
        }
    }
}
