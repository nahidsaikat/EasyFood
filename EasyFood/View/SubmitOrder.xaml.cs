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
using EasyFood.View;
using EasyFood.Model;
using System.Diagnostics;
using EasyFood.Controller;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.View
{
    public sealed partial class SubmitOrder : Page
    {
        // orderDict is a dictionary with key = itemId_Price and value = quantity
        private Dictionary<string, int> orderDict = new Dictionary<string, int>();
        int rowCount = 0;
        double total = 0.0;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public SubmitOrder()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            //rowCount = conn.Table<Order>().Count() + 1;
            try
            {
                rowCount = (from order in conn.Table<Order>()
                            orderby order.Id descending
                            select order).First<Order>().Id + 1;
            }
            catch(Exception ex)
            {
                rowCount = 1;
            }

            CopyDict();
            LoadOrder();
        }

        private void CopyDict()
        {
            foreach(var dictItem in Foods.submitOrderDict)
            {
                orderDict[dictItem.Key] = dictItem.Value;
            }
            Foods.submitOrderDict.Clear();
        }

        private void LoadOrder()
        {
            // This method creates the list view of order items.
            OrderNoTextBlock.Text = "Order - " + (1000 + rowCount).ToString();
            int counter = 1;
            foreach (var order in orderDict)
            {
                string[] itemId_Price = order.Key.Split(' ');
                int itemId = Convert.ToInt32(itemId_Price[0]);
                Item item = (from I in conn.Table<Item>()
                             where I.Id == itemId
                             select I).ToList<Item>().First<Item>();

                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;

                TextBlock textblock = new TextBlock();
                textblock.Text = counter + ".  " + item.Name + "   " + item.Prize + "tk.   Quantity: " + order.Value +
                    "   " + (item.Prize * order.Value) + "tk.";
                counter++;
                textblock.HorizontalAlignment = HorizontalAlignment.Center;
                textblock.VerticalAlignment = VerticalAlignment.Center;
                stackpanel.Children.Add(textblock);

                total += (item.Prize * order.Value);

                OrderItemsListView.Items.Add(stackpanel);
            }
            TotalTextBlock.Text = "Total Amount:  " + total.ToString() + "tk.";
            Debug.WriteLine(orderDict.Count());
        }
        
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime d = DateTime.Now;
            string day = d.Day < 10 ? "0" + d.Day.ToString() : d.Day.ToString();
            string month = d.Month < 10 ? "0" + d.Month.ToString() : d.Month.ToString();
            string year = d.Year.ToString();
            string date = day + "-" + month + "-" + year;

            string hour = d.Hour < 10 ? "0" + d.Hour.ToString() : d.Hour.ToString();
            string minute = d.Minute < 10 ? "0" + d.Minute.ToString() : d.Minute.ToString();
            string second = d.Second < 10 ? "0" + d.Minute.ToString() : d.Minute.ToString();
            string time = hour + ":" + minute + ":" + second;
                        
            string orderNo = "Order - " + (rowCount + 1000).ToString();
            
            if (orderDict.Count > 0)
            {
                // insert in order table
                conn.CreateTable<Order>();
                conn.Insert(new Order() {
                    Id = rowCount,
                    UserId = Login.userId,
                    OrderDate = date,
                    OrderTime = time,
                    TotalAmount = total,
                    Acknowledged = 0,
                    Deliveried = 0,
                    ProcessTime = "",
                    Canceled = 0,
                    OrderNo = orderNo
                });

                // insert in order item table
                foreach (var order in orderDict)
                {
                    string[] itemId_Price = order.Key.Split(' ');
                    int itemId = Convert.ToInt32(itemId_Price[0]);
                    double price = Convert.ToDouble(itemId_Price[1]);
                    int quantity = Convert.ToInt32(order.Value);

                    conn.CreateTable<OrderItem>();
                    conn.Insert(new OrderItem() {
                        OrderId = rowCount,
                        UserId = Login.userId,
                        Date = date,
                        Time = time,
                        ItemId = itemId,
                        Price = price,
                        Quantity = quantity,
                        Total = price * quantity
                    });
                }
                await new MessageDialog("Order is submitted successfully.\nTotal Amount: " + total).ShowAsync();
                SubmittedOrder.showOrderDict = orderDict;
                SubmittedOrder.title = orderNo;
                this.Frame.Navigate(typeof(SubmittedOrder));
            }
        }
    }
}
