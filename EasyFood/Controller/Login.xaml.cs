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

namespace EasyFood.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public static string userName = string.Empty;
        public static string userPassword = string.Empty;
        public static string userType = string.Empty;
        public static int userId = -1;
        string path;
        SQLite.Net.SQLiteConnection conn;
        public Login()
        {
            this.InitializeComponent();
            WarningtextBlock.Visibility = Visibility.Collapsed;
            UserTypeComboBox.SelectedIndex = 0;

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string name = UserNameTextBox.Text;
            string pass = PasswordTextBox.Password;
            string type = (UserTypeComboBox.SelectedValue as ComboBoxItem).Content.ToString();

            if (name == "" || pass == "" || type == "")
            {
                WarningtextBlock.Text = "Please fill up all the fields!";
                WarningtextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                bool same = false;
                var queryUser = conn.Table<User>();
                foreach (var user in queryUser)
                {
                    if (user.UserName == name && user.Password == pass && user.Type == type)
                    {
                        userId = user.Id;
                        same = true;
                        break;
                    }
                }
                if (same && type == "Admin")
                {
                    this.Frame.Navigate(typeof(Manager));
                }
                else if (same && type == "User")
                {
                    this.Frame.Navigate(typeof(View.User));
                }
                else
                {
                    WarningtextBlock.Text = "User not found!";
                    WarningtextBlock.Visibility = Visibility.Visible;
                }
                userName = name;
                userPassword = pass;
                userType = type;
            }

            //this.Frame.Navigate(typeof(Manager));
            //this.Frame.Navigate(typeof(View.User));
        }
    }
}
