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
using System.Diagnostics;
using EasyFood.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditItem : Page
    {
        public List<string> categoryList;
        public List<string> itemList;
        string comboCategory = string.Empty;
        public string categoryName = string.Empty;
        int ID;
        int categoryId;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public EditItem()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            categoryList = CategoryManager.comboCategory();
            CategoryComboBox.ItemsSource = categoryList;

            itemList = ItemManager.getItem(string.Empty);
            ItemComboBox.ItemsSource = itemList;

            CategoryItemComboBox.ItemsSource = categoryList;
        }

        private void ItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboCategory == string.Empty)
            {
                WarningTextBlock.Text = "Please select a category";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                string comboItem = string.Empty;
                WarningTextBlock.Text = "";
                //Debug.WriteLine((sender as ComboBox).SelectedValue);
                try
                {
                    comboItem = (sender as ComboBox).SelectedValue.ToString();
                    if (comboItem != string.Empty)
                    {
                        var itemList = (from item in conn.Table<Item>()
                                        where item.Name == comboItem && item.CategoryName == comboCategory
                                        select item).ToList<Item>();

                        categoryId = itemList[0].CategoryId;
                        ID = itemList[0].Id;
                        CategoryItemComboBox.SelectedValue = itemList[0].CategoryName;
                        AddItemTextBox.Text = itemList[0].Name;
                        DescriptionTextBox.Text = itemList[0].Description;
                        SizeTextBox.Text = itemList[0].Size.ToString();
                        PrizeTextBox.Text = itemList[0].Prize.ToString();
                        AvailableCheckBox.IsChecked = (itemList[0].Available == 1) ? true : false;

                        categoryName = itemList[0].CategoryName;
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Debug.WriteLine((sender as ComboBox).SelectedValue);

                itemList = ItemManager.getItem((sender as ComboBox).SelectedValue.ToString());
                ItemComboBox.ItemsSource = itemList;
                ItemComboBox.SelectedItem = null;

                comboCategory = CategoryComboBox.SelectedValue.ToString();

                CategoryItemComboBox.SelectedValue = string.Empty;
                AddItemTextBox.Text = string.Empty;
                DescriptionTextBox.Text = string.Empty;
                SizeTextBox.Text = string.Empty;
                PrizeTextBox.Text = string.Empty;
                AvailableCheckBox.IsChecked = false;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

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
            else if (AddItemTextBox.Text == "")
            {
                WarningTextBlock.Text = "Please give a name.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if (PrizeTextBox.Text == "")
            {
                WarningTextBlock.Text = "Please enter the prize.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                string itemName = AddItemTextBox.Text;
                string description = DescriptionTextBox.Text;
                string size = SizeTextBox.Text;
                int available = ((bool)AvailableCheckBox.IsChecked) ? 1 : 0;
                //if ((bool)AvailableCheckBox.IsChecked)
                //{
                //    available = 1;
                //}
                //else
                //{
                //    available = 0;
                //}

                bool flag = false;
                double prize = 0.0;
                try
                {
                    prize = Convert.ToDouble(PrizeTextBox.Text);
                    flag = true;
                }
                catch (Exception ex)
                {

                }
                if (flag)
                {
                    // Checking duplicate item name
                    bool same = false;
                    var queryItem = conn.Table<Item>();
                    foreach (var item in queryItem)
                    {
                        if (item.Name == itemName && item.CategoryName == categoryName && item.Id != ID)
                        {
                            same = true;
                            break;
                        }
                    }
                    if (!same)
                    {
                        var add = conn.Update(new Item()
                        {
                            Id = ID,
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
                        AvailableCheckBox.IsChecked = false;
                        CategoryComboBox.SelectedItem = null;
                        ItemComboBox.SelectedItem = null;
                        CategoryItemComboBox.SelectedItem = null;

                        WarningTextBlock.Text = "Item edited successfully";
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

        private void CategoryItemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                categoryName = CategoryItemComboBox.SelectedValue.ToString();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
