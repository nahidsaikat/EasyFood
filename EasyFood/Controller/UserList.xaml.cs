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
    public sealed partial class UserList : Page
    {
        public List<User> userList;
        public UserList()
        {
            this.InitializeComponent();
            userList = UserManager.getUsers();
            UserListView.ItemsSource = userList;
        }

        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine((sender as Button).Content);
        }
    }
}
