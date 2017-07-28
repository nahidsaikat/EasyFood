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
    public sealed partial class OrdersList : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public OrdersList()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            LoadOrders();
        }
        private void LoadOrders()
        {
            OrdersListView.Items.Clear();

            var orderList = (from order in conn.Table<Order>()
                             where order.Deliveried == 0 && order.Canceled == 0
                             select order).ToList<Order>();
            int counter = 1;
            foreach(var order in orderList)
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Name = order.Id.ToString();
                stackpanel.Orientation = Orientation.Horizontal;
                stackpanel.Margin = new Thickness(5, 0, 0, 0);
                
                User user = (from oneUser in conn.Table<User>()
                            where oneUser.Id == order.UserId
                            select oneUser).ToList<User>()[0];

                string orderNo = order.OrderNo;
                string userName = user.UserName.ToString();
                string date = order.OrderDate.ToString();
                string time = order.OrderTime.ToString();

                TextBlock textblock = new TextBlock();
                textblock.Text = counter++ + ".  " + orderNo + "  (" + userName + ")  ";
                textblock.FontSize = 18;
                stackpanel.Children.Add(textblock);

                CheckBox acknowledge = new CheckBox();
                acknowledge.IsChecked = Convert.ToBoolean(order.Acknowledged);
                acknowledge.Content = "Acknowledged";
                acknowledge.IsEnabled = false;
                acknowledge.Margin = new Thickness(10, 0, 0, 0);
                acknowledge.VerticalAlignment = VerticalAlignment.Top;
                stackpanel.Children.Add(acknowledge);
                
                OrdersListView.Items.Add(stackpanel);
            }
        }
        
        private void OrdersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel stackpanel = ((sender as ListView).SelectedItem) as StackPanel;
            Debug.WriteLine(stackpanel.Name);

            // OrderDetails.orderId is OrderDetails page's variable
            OrderDetails.orderId = Convert.ToInt32(stackpanel.Name);
            OrderDetailsFrame.Navigate(typeof(OrderDetails));
        }
    }
}
