using CustomerServiceControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServiceControl.Services
{
    public interface ICustomerServices
    {
        void Add(Customer customer);

        Customer GetById(int? customerId);

        ICollection<Customer> GetByFilter(int fieldType, string fiedlValue);

        ICollection<Customer> GetAll();

        ICollection<Customer> GetAllOrderByName();

        void Update(Customer customer);

        void Delete(int? customerId);
    }
}
