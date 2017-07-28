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
using Windows.UI.Notifications;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Foods : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public List<Category> categoryList;
        public List<string> itemList;
        public List<Item> itemFullList;
        public static List<string> orderList = new List<string>();
        private Dictionary<string, int> orderDict = new Dictionary<string, int>();
        public static Dictionary<string, int> submitOrderDict = new Dictionary<string, int>();
        public Foods()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            categoryList = CategoryManager.getCategory();
            CategoryListView.ItemsSource = categoryList;
            CategoryListView.SelectedIndex = 0;

            //loadItemListView(categoryList.First<Category>().Name);
        }

        private void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string category = ((sender as ListView).SelectedItem as Category).Name;
            Debug.WriteLine(category);
            loadItemListView(category);
        }

        private void loadItemListView(string category)
        {
            ItemListView.Items.Clear();

            itemFullList = ItemManager.getFullItem(category);
            foreach(Item item in itemFullList)
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;

                string text = item.Name + "  -  ";
                if (item.Size != string.Empty)
                {
                    text += item.Size + "  -  ";
                }
                text += item.Prize + "tk.";

                TextBlock textblock = new TextBlock();
                textblock.Text = text;
                textblock.HorizontalAlignment = HorizontalAlignment.Center;
                textblock.VerticalAlignment = VerticalAlignment.Center;

                stackpanel.Children.Add(textblock);

                Button myButton = new Button();
                myButton.Name = item.Id.ToString();
                myButton.Content = "Order";
                myButton.Margin = new Thickness(20, 0, 0, 0);
                myButton.HorizontalAlignment = HorizontalAlignment.Left;
                myButton.VerticalAlignment = VerticalAlignment.Top;
                myButton.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                myButton.BorderThickness = new Thickness(2, 2, 2, 2);
                myButton.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                myButton.Click += MyButton_Click;

                stackpanel.Children.Add(myButton);

                ItemListView.Items.Add(stackpanel);
            }
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine((sender as Button).Name);

            string itemID = (sender as Button).Name.ToString();
            orderList.Add(itemID);

            if (orderDict.ContainsKey(itemID))
            {
                orderDict[itemID]++;
            }
            else
            {
                orderDict.Add(itemID, 1);
            }
            OrderListTextBlock.Visibility = Visibility.Visible;
            LoadOrderListView(orderDict);

            WarningTextBlock.Visibility = Visibility.Collapsed;
        }

        private void LoadOrderListView(Dictionary<string, int> dict)
        {
            OrderListView.Items.Clear();

            int counter = 1;
            foreach (var order in dict)
            {
                int itemId = Convert.ToInt32(order.Key);
                Item item = (from I in conn.Table<Item>()
                             where I.Id == itemId
                             select I).ToList<Item>().First<Item>();

                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;

                TextBlock textblock = new TextBlock();
                textblock.Text = counter + ".  " + item.Name;
                counter++;
                textblock.HorizontalAlignment = HorizontalAlignment.Center;
                textblock.VerticalAlignment = VerticalAlignment.Center;
                stackpanel.Children.Add(textblock);

                TextBox textbox = new TextBox();
                textbox.Width = 10;
                textbox.Text = order.Value.ToString();
                textbox.Margin = new Thickness(10, 0, 0, 0);
                textbox.Name = itemId + " " + item.Prize;
                stackpanel.Children.Add(textbox);

                Button cancelButton = new Button();
                cancelButton.Name = order.Key;
                cancelButton.Content = "Cancel";
                cancelButton.Margin = new Thickness(10, 0, 0, 0);
                cancelButton.HorizontalAlignment = HorizontalAlignment.Left;
                cancelButton.VerticalAlignment = VerticalAlignment.Top;
                cancelButton.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                cancelButton.BorderThickness = new Thickness(2, 2, 2, 2);
                cancelButton.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                cancelButton.Click += CancelButton_Click;
                stackpanel.Children.Add(cancelButton);

                OrderListView.Items.Add(stackpanel);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            string itemID = (sender as Button).Name.ToString();
            orderDict.Remove(itemID);
            OrderListView.Items.Clear();
            LoadOrderListView(orderDict);
        }

        private void SubmitCommentButton_Click(object sender, RoutedEventArgs e)
        {
            // If safe = false then quantiry textbox has problem
            bool safe = true;
            foreach(var item in OrderListView.Items)
            {
                TextBox QuantityTextBox = ((item as StackPanel).Children as UIElementCollection)[1] as TextBox;
                string itemId_Price = QuantityTextBox.Name.ToString();
                int quantity = 0;
                try
                {
                    quantity = Convert.ToInt32(QuantityTextBox.Text.ToString());
                    submitOrderDict.Add(itemId_Price, quantity);
                }
                catch(Exception ex)
                {
                    safe = false;
                    WarningTextBlock.Text = "Quantity should be numeric.";
                    WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
            }

            if(OrderListView.Items.Count() < 1)
            {
                WarningTextBlock.Visibility = Visibility.Visible;
            }
            else if (safe)
            {
                orderDict.Clear();
                OrderListView.Items.Clear();
                // SubmitOrder page will take submitOrderDict and sibmit the order in DB.
                this.Frame.Navigate(typeof(SubmitOrder));
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
