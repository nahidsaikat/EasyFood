using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace EasyFood.Model
{
    public class Order
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public double TotalAmount { get; set; }
        public int Acknowledged { get; set; }
        public int Deliveried { get; set; }
        public string ProcessTime { get; set; }
        public int Canceled { get; set; }
        public string OrderNo { get; set; }
    }
}
