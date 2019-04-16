using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.Services
{
    public class CustomerServicesException : Exception
    {
        public CustomerServicesException(string message)
            : base(message)
        {
        }
    }
}