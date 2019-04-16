using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("name=SqlServerDBConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static DBContext Create()
        {
            return new DBContext();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductAndService> ProductsAndServices { get; set; }
    }
}