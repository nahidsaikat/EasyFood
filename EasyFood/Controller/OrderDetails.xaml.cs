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
    public sealed partial class OrderDetails : Page
    {
        // This variable is assigned in OrderList.xaml.cs file
        public static int orderId = -1;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public OrderDetails()
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

            var orderitemList = (from item in conn.Table<OrderItem>()
                                         where item.OrderId == order.Id
                                         select item).ToList<OrderItem>();
            OrderItemsListView.Items.Clear();
            int counter = 1;
            foreach (var orderItem in orderitemList)
            {
                string itemName = (from item in conn.Table<Item>()
                                   where item.Id == orderItem.ItemId
                                   select item).First<Item>().Name;

                TextBlock textblock = new TextBlock();
                textblock.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
                textblock.Text = counter++ + ". " + itemName + " " + orderItem.Price + "tk. Quantity: " + orderItem.Quantity + " Total: " + orderItem.Total;
                OrderItemsListView.Items.Add(textblock);
            }

            if(orderitemList.Count > 4)
            {
                OrderItemsListView.Height = 180;
            }

            TotalAmountTextBlock.Text = "Total Cost: " + order.TotalAmount.ToString();
            
            if(order.Acknowledged == 1)
            {
                ProcessTimeTextBox.Text = order.ProcessTime;
                ProcessTimeTextBox.IsEnabled = false;
                AcknowledgeButton.Visibility = Visibility.Collapsed;
            }
        }

        private async void AcknowledgeButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessTimeTextBox.Text == string.Empty)
            {
                await new MessageDialog("Please provide processing time for this order.").ShowAsync();
            }
            else
            {
                Order order = (from O in conn.Table<Order>()
                               where O.Id == orderId
                               select O).ToList<Order>()[0];
                order.Acknowledged = 1;
                order.ProcessTime = ProcessTimeTextBox.Text;
                conn.Update(order);
                LoadDetails();
            }
        }

        private async void DeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            string amount = AmountTextBox.Text;
            if (amount != string.Empty)
            {
                Order order = (from O in conn.Table<Order>()
                               where O.Id == orderId
                               select O).ToList<Order>()[0];

                order.Deliveried = 1;
                conn.Update(order);
                await new MessageDialog("This order has been deliveried.").ShowAsync();

                DeliveryButton.IsEnabled = false;
                CancelButton.IsEnabled = false;
            }
            else
            {
                await new MessageDialog("Please enter amount of the order.").ShowAsync();
            }
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = (from O in conn.Table<Order>()
                           where O.Id == orderId
                           select O).ToList<Order>()[0];
            
            order.Canceled = 1;
            conn.Update(order);
            await new MessageDialog("This order has been canceled.").ShowAsync();

            DeliveryButton.IsEnabled = false;
            CancelButton.IsEnabled = false;
        }
    }
}
