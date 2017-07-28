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

namespace EasyFood.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SubmittedOrder : Page
    {
        public static string title = "";
        public static Dictionary<string, int> showOrderDict = new Dictionary<string, int>();

        string path;
        SQLite.Net.SQLiteConnection conn;
        public SubmittedOrder()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            // title = orderNo. Which is assigned from SubmitOrder cs page.
            TitleTextBlock.Text = title;
            onLoad();
        }

        void onLoad()
        {
            double total = 0.0;
            int counter = 1;
            foreach (var order in showOrderDict)
            {
                string[] itemId_Price = order.Key.Split(' ');
                int itemId = Convert.ToInt32(itemId_Price[0]);
                Item item = (from I in conn.Table<Item>()
                             where I.Id == itemId
                             select I).ToList<Item>().First<Item>();

                TextBlock textblock = new TextBlock();
                textblock.Text = counter + ".  " + item.Name + "   " + item.Prize + "tk.   Quantity: " + order.Value +
                    "   " + (item.Prize * order.Value) + "tk.";
                counter++;
                textblock.HorizontalAlignment = HorizontalAlignment.Center;
                textblock.VerticalAlignment = VerticalAlignment.Center;
                MainStackPanel.Children.Add(textblock);

                total += (item.Prize * order.Value);
            }
            TotalTextBlock.Text = "Total Cost : " + total.ToString();
        }
    }
}
