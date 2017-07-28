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
    public sealed partial class AddUser : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public AddUser()
        {
            this.InitializeComponent();
            WarningTextBlock.Visibility = Visibility.Collapsed;
            UserTypeComboBox.SelectedIndex = 1;

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();
        }

        private void UserAddButton_Click(object sender, RoutedEventArgs e)
        {
            string firstname = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Password;
            string email = EmailTextBox.Text;
            string contactNo = ContactNoTextBox.Text;
            string type = (UserTypeComboBox.SelectedValue as ComboBoxItem).Content.ToString();

            // Checking empty value
            if (firstname == string.Empty || lastName == string.Empty || userName == string.Empty || password == string.Empty ||
                email == string.Empty || contactNo == string.Empty || type == string.Empty)
            {
                WarningTextBlock.Visibility = Visibility.Visible;
                WarningTextBlock.Text = "Please fill up all the field.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else
            {
                // Checking duplicate value
                bool same = false;
                var queryUser = conn.Table<User>();
                foreach (var user in queryUser)
                {
                    if (user.UserName == userName)
                    {
                        same = true;
                        break;
                    }
                }

                if (!same)
                {
                    // Insert in database as it is not already in database
                    var add = conn.Insert(new User()
                    {
                        FirstName = firstname,
                        LastName = lastName,
                        FullName = firstname + " " + lastName,
                        UserName = userName,
                        Password = password,
                        Email = email,
                        ContactNo = contactNo,
                        Type = type
                    });
                    Debug.WriteLine(path);
                    FirstNameTextBox.Text = string.Empty;
                    LastNameTextBox.Text = string.Empty;
                    UserNameTextBox.Text = string.Empty;
                    PasswordTextBox.Password = string.Empty;
                    EmailTextBox.Text = string.Empty;
                    ContactNoTextBox.Text = string.Empty;

                    WarningTextBlock.Visibility = Visibility.Visible;
                    WarningTextBlock.Text = "User added successfully.";
                    WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);

                }
                else
                {
                    // as duplicate value tell user.
                    WarningTextBlock.Visibility = Visibility.Visible;
                    WarningTextBlock.Text = "This user name is already in used.";
                    WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
            }
        }
    }
}
