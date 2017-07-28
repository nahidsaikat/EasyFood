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
    public sealed partial class EditCategory : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public List<string> categoryList;
        public EditCategory()
        {
            this.InitializeComponent();
            categoryList = CategoryManager.comboCategory();
            CategoryComboBox.ItemsSource = categoryList;

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string categoryName = CategoryComboBox.SelectedValue.ToString();
            Debug.WriteLine(categoryName);

            EditCategoryTextBox.Text = categoryName;
        }

        private void EditCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = CategoryComboBox.SelectedValue.ToString();
            string newCategory = EditCategoryTextBox.Text;

            if(newCategory == string.Empty)
            {
                WarningTextBlock.Text = "Please provide a category name.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                WarningTextBlock.Text = "";

                // Cahecking duplicate value. 
                bool same = false;
                var queryCategory = conn.Table<Category>();
                foreach (var category in queryCategory)
                {
                    if (category.Name == newCategory && category.Name != categoryName)
                    {
                        same = true;
                        break;
                    }
                }

                // If duplicate value prompt user else enter in database.
                if (!same)
                {
                    var cat = (from c in conn.Table<Category>()
                                    where c.Name == categoryName
                                    select c).ToList<Category>();

                    var add = conn.Update(new Category()
                    {
                        Id = cat[0].Id,
                        Name = newCategory,
                        Deleted = 0
                    });
                    Debug.WriteLine(path);
                    EditCategoryTextBox.Text = string.Empty;

                    WarningTextBlock.Text = "Category edited successfully.";
                    WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);

                }
                else
                {
                    WarningTextBlock.Text = "This category name is already in used.";
                    WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
            }
        }
    }
}
