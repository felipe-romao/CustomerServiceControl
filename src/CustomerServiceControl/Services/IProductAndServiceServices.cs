using CustomerServiceControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerServiceControl.Services
{
    public interface IProductAndServiceServices
    {
        void Add(ProductAndService productAndService);

        ProductAndService GetById(int? productAndServiceId);

        ICollection<ProductAndService> GetAll();

        void Update(ProductAndService productAndService);

        void Delete(int? productAndServiceId);
    }
}