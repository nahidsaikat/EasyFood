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
    public sealed partial class AddItem : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public List<string> categoryList;
        public string categoryName = "";
        public AddItem()
        {
            this.InitializeComponent();
            categoryList = CategoryManager.comboCategory();
            CategoryComboBox.ItemsSource = categoryList;

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Item>();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            If category name or item name or prize not provided give warning message.
            Then if prize value is not decimal give warning message.
            Else add in database.
            
            */
            if (categoryName == "")
            {
                WarningTextBlock.Text = "Please select a category.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if(AddItemTextBox.Text == "")
            {
                WarningTextBlock.Text = "Please give a name.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if(PrizeTextBox.Text == "")
            {
                WarningTextBlock.Text = "Please enter the prize.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                int categoryId = (from category in conn.Table<Category>()
                                  where category.Name == categoryName
                                  select category.Id).FirstOrDefault();
                string itemName = AddItemTextBox.Text;
                string description = DescriptionTextBox.Text;
                string size = SizeTextBox.Text;
                int available = 0;
                if ((bool)AvailableCheckBox.IsChecked)
                {
                    available = 1;
                }
                else
                {
                    available = 0;
                }

                bool flag = false;
                double prize = 0.0;
                try
                {
                    prize = Convert.ToDouble(PrizeTextBox.Text);
                    flag = true;
                }
                catch(Exception ex)
                {
                    
                }
                if (flag)
                {
                    // Checking duplicate item name
                    bool same = false;
                    var queryItem = conn.Table<Item>();
                    foreach (var item in queryItem)
                    {
                        if (item.Name == itemName && item.CategoryName == categoryName)
                        {
                            same = true;
                            break;
                        }
                    }
                    if (!same)
                    {
                        var add = conn.Insert(new Item()
                        {
                            CategoryId = categoryId,
                            CategoryName = categoryName,
                            Name = itemName,
                            Description = description,
                            Size = size,
                            Prize = prize,
                            Available = available,
                            Deleted = 0
                        });
                        Debug.WriteLine(path);

                        AddItemTextBox.Text = "";
                        PrizeTextBox.Text = "";
                        DescriptionTextBox.Text = "";
                        SizeTextBox.Text = "";
                        PrizeTextBox.Text = "";

                        WarningTextBlock.Text = "Item added successfully";
                        WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                    }
                    else
                    {
                        WarningTextBlock.Text = "This item is already exist.";
                        WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    }
                }
                else
                {
                    WarningTextBlock.Text = "Prize should be numeric.";
                    WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                

            }
            Debug.WriteLine(path);
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            categoryName = CategoryComboBox.SelectedValue.ToString();
        }
    }
}
