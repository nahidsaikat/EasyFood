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
    public sealed partial class ShowComments : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        public ShowComments()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            LoadCommentList();
        }

        private void LoadCommentList()
        {
            CommentsListView.Items.Clear();
            var commentList = conn.Table<Comment>();

            int counter = 1;
            foreach (var comment in commentList)
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Name = comment.Id.ToString();
                stackpanel.Orientation = Orientation.Horizontal;
                stackpanel.Margin = new Thickness(5, 0, 0, 0);
                
                TextBlock textblock = new TextBlock();
                textblock.Name = comment.Id.ToString();
                textblock.Text = counter++ + ".  " + comment.Comments;
                textblock.FontSize = 18;
                stackpanel.Children.Add(textblock);

                CommentsListView.Items.Add(stackpanel);
            }
        }

        private void CommentsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel stackpanel = ((sender as ListView).SelectedItem) as StackPanel;
            Debug.WriteLine(stackpanel.Name);

            // ShowCommentDetails.orderId is ShowCommentDetails page's variable
            ShowCommentDetails.commentId = Convert.ToInt32(stackpanel.Name);
            CommentDetailsFrame.Navigate(typeof(ShowCommentDetails));
        }
    }
}
