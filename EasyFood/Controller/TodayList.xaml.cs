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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodayList : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        List<string> AvailableItem;
        public TodayList()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Category>();
            loadListView();
        }

        public void loadListView()
        {
            /*
                This function loads ItemListView.
                Each category and item will be added as a item of ItemListView.
                If any item is already available it will be inserted in AvailableItem.
                Submit button will update AvailableItem list item.
            */

            AvailableItem = new List<string>();
            int categoryCounter = 0, itemCounter = 0;
            var queryCategory = conn.Table<Category>();
            foreach (var category in queryCategory)
            {
                categoryCounter++;

                // Prepare category as ItemListView item
                ListViewItem categoryListView = new ListViewItem();
                categoryListView.Name = category.Name;
                categoryListView.Content = categoryCounter + " " + category.Name;
                categoryListView.Margin = new Thickness(0, 5, 0, 0);
                ItemListView.Items.Add(categoryListView);

                var queryItem = (from I in conn.Table<Item>()
                                 where I.CategoryName == category.Name
                                 select I).ToList();

                // Prepare items as ItemListView item
                itemCounter = 0;
                foreach (var item in queryItem)
                {
                    itemCounter++;

                    TextBlock textblock = new TextBlock();
                    textblock.Text = categoryCounter + "." + itemCounter + " " + item.Name;
                    textblock.Margin = new Thickness(0, 5, 0, 0);

                    CheckBox checkbox = new CheckBox();
                    checkbox.Name = category.Id + "_" + item.Id;
                    checkbox.IsChecked = item.Available == 1 ? true : false;
                    checkbox.Margin = new Thickness(10,0,0,0);
                    checkbox.Checked += Checkbox_Checked;
                    checkbox.Unchecked += Checkbox_Unchecked;

                    StackPanel stackpanel = new StackPanel();
                    stackpanel.Orientation = Orientation.Horizontal;
                    stackpanel.Margin = new Thickness(20, 5, 20, 0);
                    stackpanel.Children.Add(textblock);
                    stackpanel.Children.Add(checkbox);

                    ItemListView.Items.Add(stackpanel);
                    
                    if(item.Available == 1)
                    {
                        AvailableItem.Add(category.Id + "_" + item.Id);
                    }
                }
            }
        }
        
        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            // When item is checked it will be added in AvailableItem
            Debug.WriteLine((sender as CheckBox).Name);
            string checkBoxName = (sender as CheckBox).Name;
            if (!AvailableItem.Contains(checkBoxName))
            {
                AvailableItem.Add(checkBoxName);
            }
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            // When item is unchecked it will be removed from AvailableItem
            Debug.WriteLine((sender as CheckBox).Name);
            string checkBoxName = (sender as CheckBox).Name;
            if (AvailableItem.Contains(checkBoxName))
            {
                AvailableItem.Remove(checkBoxName);
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            /*
                IDS will contain category id, item id and 1/0. IDS[0] = category Id. IDS[1] = item id. IDS[2] = 1/0.
            */
            conn.Execute("UPDATE Item SET Available = 0");
            string[] IDS;
            foreach(var item in AvailableItem)
            {
                IDS = item.Split('_');
                conn.Execute("UPDATE Item SET Available = 1 WHERE CategoryId = " + IDS[0] + " AND Id = " + IDS[1]);
                Debug.WriteLine(IDS[0] + " " + IDS[1]);
            }
        }
    }
}
