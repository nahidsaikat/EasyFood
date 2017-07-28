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
    public sealed partial class EditUser : Page
    {
        public List<string> userName;

        string path;
        SQLite.Net.SQLiteConnection conn;
        int ID;
        public EditUser()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            userName = UserManager.getUserName();
            UserComboBox.ItemsSource = userName;
        }

        private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string username = (sender as ComboBox).SelectedValue.ToString();
            Debug.WriteLine(username);

            var user = (from u in conn.Table<User>()
                        where u.UserName == username
                        select u).ToList<User>();

            ID = user[0].Id;
            FirstNameTextBox.Text = user[0].FirstName;
            LastNameTextBox.Text = user[0].LastName;
            UserNameTextBox.Text = user[0].UserName;
            PasswordTextBox.Password = user[0].Password;
            EmailTextBox.Text = user[0].Email;
            ContactNoTextBox.Text = user[0].ContactNo;
            UserTypeComboBox.SelectedIndex = (user[0].Type == "Admin") ? 0 : 1;
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

            if (UserComboBox.SelectedValue.ToString() == string.Empty)
            {
                WarningTextBlock.Visibility = Visibility.Visible;
                WarningTextBlock.Text = "You haven't select any user.";
                WarningTextBlock.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            // Checking empty value
            else if (firstname == string.Empty || lastName == string.Empty || userName == string.Empty || password == string.Empty ||
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
                    if (user.UserName == userName && user.Id != ID)
                    {
                        same = true;
                        break;
                    }
                }

                if (!same)
                {
                    // Insert in database as it is not already in database
                    var add = conn.Update(new User()
                    {
                        Id = ID,
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
                    UserTypeComboBox.SelectedItem = null;
                    UserComboBox.SelectedItem = null;

                    WarningTextBlock.Visibility = Visibility.Visible;
                    WarningTextBlock.Text = "User edited successfully.";
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
