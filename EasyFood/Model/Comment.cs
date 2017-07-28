using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyFood.Model
{
    public class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Comments { get; set; }
    }
}
