using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqlite
{
    [Table("customer")]
    public class Customer
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("cust_name")]
        public string CustomerName { get; set; }

        [Column("cust_mobile")]
        public string Mobile { get; set; }

        [Column("cust_email")]
        public string Email { get; set; }
    }
}
