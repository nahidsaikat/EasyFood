using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EasyFood.Controller
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Manager : Page
    {
        public Manager()
        {
            this.InitializeComponent();
            
            //ordersList.IsSelected = true;
            SplitViewListBox.SelectedItem = ordersList;
            titleTextBox.Text = "Orders";
            managerFrame.Navigate(typeof(OrdersList));
            listButton.Visibility = Visibility.Visible;
            listButton.Content = "All Orders";
            EditComboBox.Visibility = Visibility.Collapsed;
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            managerSplitView.IsPaneOpen = !managerSplitView.IsPaneOpen;
        }
        
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ordersList.IsSelected)
            {
                managerFrame.Navigate(typeof(OrdersList));
                titleTextBox.Text = "Orders";
                listButton.Visibility = Visibility.Visible;
                listButton.Content = "All Orders";
                EditComboBox.Visibility = Visibility.Collapsed;
            }
            else if (todayList.IsSelected)
            {
                managerFrame.Navigate(typeof(TodayList));
                titleTextBox.Text = "Today List";
                listButton.Visibility = Visibility.Collapsed;
                EditComboBox.Visibility = Visibility.Collapsed;
            }
            else if (addcategory.IsSelected)
            {
                managerFrame.Navigate(typeof(AddCategory));
                titleTextBox.Text = "Add Category";
                listButton.Visibility = Visibility.Visible;
                listButton.Content = "Category List";
                EditComboBox.Visibility = Visibility.Collapsed;
            }
            else if (addItem.IsSelected)
            {
                managerFrame.Navigate(typeof(AddItem));
                titleTextBox.Text = "Add Item";
                listButton.Visibility = Visibility.Visible;
                listButton.Content = "Item List";
                EditComboBox.Visibility = Visibility.Collapsed;
            }
            else if (addUser.IsSelected)
            {
                managerFrame.Navigate(typeof(AddUser));
                titleTextBox.Text = "Add User";
                listButton.Visibility = Visibility.Visible;
                listButton.Content = "User List";
                EditComboBox.Visibility = Visibility.Collapsed;
            }
            else if (edit.IsSelected)
            {
                listButton.Visibility = Visibility.Collapsed;
                EditComboBox.Visibility = Visibility.Visible;
                EditComboBox.SelectedIndex = 0;
                managerFrame.Navigate(typeof(EditUser));
                titleTextBox.Text = "Edit User";
            }
        }

        private void listButton_Click(object sender, RoutedEventArgs e)
        {
            if (ordersList.IsSelected)
            {
                managerFrame.Navigate(typeof(AllOrders));
                titleTextBox.Text = "All Order List";
                SplitViewListBox.SelectedItem = null;
            }
            else if (addcategory.IsSelected)
            {
                managerFrame.Navigate(typeof(CategoryList));
                titleTextBox.Text = "Category List";
                SplitViewListBox.SelectedItem = null;
            }
            else if (addItem.IsSelected)
            {
                managerFrame.Navigate(typeof(ItemList));
                titleTextBox.Text = "Item List";
                SplitViewListBox.SelectedItem = null;
            }
            else if (addUser.IsSelected)
            {
                managerFrame.Navigate(typeof(UserList));
                titleTextBox.Text = "User List";
                SplitViewListBox.SelectedItem = null;
            }
        }

        private void EditComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine((((sender as ComboBox).SelectedValue) as ComboBoxItem).Content);
            string selected = (((sender as ComboBox).SelectedValue) as ComboBoxItem).Content.ToString();
            if (selected == "User")
            {
                managerFrame.Navigate(typeof(EditUser));
                titleTextBox.Text = "Edit User";
            }
            else if (selected == "Category")
            {
                managerFrame.Navigate(typeof(EditCategory));
                titleTextBox.Text = "Edit Category";
            }
            else if (selected == "Item")
            {
                managerFrame.Navigate(typeof(EditItem));
                titleTextBox.Text = "Edit Item";
            }
        }

        private void ShowComment_Click(object sender, RoutedEventArgs e)
        {
            managerFrame.Navigate(typeof(ShowComments));
            titleTextBox.Text = "Comments";
            listButton.Visibility = Visibility.Collapsed;
            EditComboBox.Visibility = Visibility.Collapsed;
            SplitViewListBox.SelectedItem = null;
        }
    }
}
