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
    public sealed partial class ShowCommentDetails : Page
    {
        // This variable will be assigned in ShowComments.xaml.cs file
        public static int commentId = 0;

        string path;
        SQLite.Net.SQLiteConnection conn;
        public ShowCommentDetails()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            LoadComment();
        }

        private void LoadComment()
        {
            var comment = (from comm in conn.Table<Comment>()
                           where comm.Id == commentId
                           select comm).First<Comment>();
            FromTextBlock.Text += comment.UserName;
            CategoryNameTextBlock.Text += comment.CategoryName;
            ItemNameTextBlock.Text += comment.ItemName;
            CommentTextBlock.Text = comment.Comments;
        }
    }
}
