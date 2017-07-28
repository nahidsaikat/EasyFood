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
    public sealed partial class AddCategory : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public AddCategory()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Category>();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AddCategoryTextBox.Text;
            if(name == string.Empty)
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
                    if (category.Name == name)
                    {
                        same = true;
                        break;
                    }
                }

                // If duplicate value prompt user else enter in database.
                if (!same)
                {
                    var add = conn.Insert(new Category()
                    {
                        Name = name,
                        Deleted = 0
                    });
                    Debug.WriteLine(path);
                    AddCategoryTextBox.Text = string.Empty;
                    
                    WarningTextBlock.Text = "Category added successfully.";
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
