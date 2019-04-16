using CustomerServiceControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.ViewModels
{
    public class RootViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public int SearchType { get; set; }
        public string SearchValue { get; set; }
    }
}