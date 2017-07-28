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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyOrder : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public MyOrder()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            loadStackPanel();
        }

        void loadStackPanel()
        {
            var orderList = (from order in conn.Table<Order>()
                             where order.Canceled == 0 && order.Deliveried == 0 && order.UserId == Login.userId
                             select order).ToList<Order>();

            foreach (var order in orderList)
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Vertical;
                stackpanel.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock orderNoTextBlock = new TextBlock();
                orderNoTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                orderNoTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
                orderNoTextBlock.Text = order.OrderNo;
                orderNoTextBlock.Margin = new Thickness(0, 10, 0, 10);
                orderNoTextBlock.FontSize = 24;
                stackpanel.Children.Add(orderNoTextBlock);

                TextBlock totalTextBlock = new TextBlock();
                totalTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                totalTextBlock.Text = "Total Cost : " + order.TotalAmount;
                totalTextBlock.Margin = new Thickness(0, 0, 0, 10);
                totalTextBlock.FontSize = 20;
                stackpanel.Children.Add(totalTextBlock);

                TextBlock processTimeTextBlock = new TextBlock();
                processTimeTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                processTimeTextBlock.Text = "Process Time : " + order.ProcessTime;
                processTimeTextBlock.Margin = new Thickness(0, 0, 0, 10);
                processTimeTextBlock.FontSize = 20;
                stackpanel.Children.Add(processTimeTextBlock);

                var orderItemList = (from item in conn.Table<OrderItem>()
                                where item.OrderId == order.Id
                                select item).ToList<OrderItem>();
                int counter = 1;
                foreach (var orderItem in orderItemList)
                {
                    var item = (from I in conn.Table<Item>()
                                       where I.Id == orderItem.ItemId
                                       select I).ToList<Item>()[0];

                    TextBlock itemTextBlock = new TextBlock();
                    itemTextBlock.Text = counter + ".  " + item.Name + "   " + item.Prize + "tk.   Quantity: " + orderItem.Quantity +
                        "   Total: " + (item.Prize * orderItem.Quantity) + "tk.";
                    counter++;
                    itemTextBlock.Margin = new Thickness(0, 0, 0, 10);
                    itemTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    itemTextBlock.VerticalAlignment = VerticalAlignment.Center;
                    stackpanel.Children.Add(itemTextBlock);
                }

                //ListViewItem myorderitem = new ListViewItem();
                //myorderitem.HorizontalAlignment = HorizontalAlignment.Center;
                //myorderitem.Width = 500;
                //myorderitem.Content = stackpanel;

                //MyOrderListView.Items.Add(myorderitem);

                TextBlock gapTextBlock = new TextBlock();
                gapTextBlock.Margin = new Thickness(0, 0, 0, 20);
                stackpanel.Children.Add(gapTextBlock);

                MyOrderStackPanel.Children.Add(stackpanel);
            }
        }
    }
}
