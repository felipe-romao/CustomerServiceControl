using CustomerServiceControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.ViewModels
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            this.ProductAndServiceItems = new List<ProductAndServiceItem>();
        }

        public Customer Customer { get; set; }
        public IList<ProductAndServiceItem> ProductAndServiceItems { get; set; }
    }
}