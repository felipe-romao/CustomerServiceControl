using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CustomerServiceControl.Models;

namespace CustomerServiceControl.Services
{
    public class CustomerServices : ICustomerServices
    {
        public void Add(Customer customer)
        {
            if (customer == null)
                throw new CustomerServicesException("Cliente não pode ser nulo.");
            try
            {
                using (var context = new DBContext())
                {
                    customer.SetDateTimeNowFirstAttendance();
                    customer.SetDateTimeNowLastAttendance();
                    var productAndServicesEntity = new List<ProductAndService>();
                    foreach(var item in customer.ProductsAndServices)
                    {
                        productAndServicesEntity.Add(context.ProductsAndServices.Find(item.Id));
                    }
                    customer.ProductsAndServices = productAndServicesEntity;

                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar adicionar o cliente: {ex.Message}.");
            }
        }

        public void Delete(int? customerId)
        {
            if (customerId == null)
                throw new CustomerServicesException("Informe um Id válido.");

            try
            {
                using (var context = new DBContext())
                {
                    var customer = context.Customers.Find(customerId);
                    context.Customers.Remove(customer);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar remover o Id '{customerId}': {ex.Message}.");
            }
        }

        public ICollection<Customer> GetAll()
        {
            try
            {
                using (var context = new DBContext())
                {
                    return context.Customers.Include("ProductsAndServices")
                        .OrderBy(c => c.DateTimeFirstAttendance).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar obter todos os clientes: {ex.Message}.");
            }
        }

        public ICollection<Customer> GetAllOrderByName()
        {
            try
            {
                using (var context = new DBContext())
                {
                    return context.Customers.Include("ProductsAndServices")
                        .OrderBy(c => c.Name).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar obter todos os clientes: {ex.Message}.");
            }
        }

        public Customer GetById(int? customerId)
        {
            if (customerId == null)
                throw new CustomerServicesException("Informe um Id válido.");

            try
            {
                using (var context = new DBContext())
                {
                    return context.Customers.Include("ProductsAndServices")
                        .FirstOrDefault(c => c.Id == customerId);
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar obter o Id '{customerId}': {ex.Message}.");
            }
        }

        public ICollection<Customer> GetByFilter(int fieldType, string fiedlValue)
        {
            if (fiedlValue == null)
                return this.GetAll();

            var value = fiedlValue.Trim();
            if (String.IsNullOrEmpty(value))
                return this.GetAll();

            try
            {
                using (var context = new DBContext())
                {
                    switch (fieldType)
                    {
                        case 0:
                            return context.Customers.Include("ProductsAndServices").Where(c => c.Name.Contains(value)).ToList();
                        case 1:
                            return context.Customers.Include("ProductsAndServices").Where(c => c.CompanyName.Contains(value)).ToList();
                    }
                    return new List<Customer>();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar obter o cliente: {ex.Message}.");
            }
        }

        public void Update(Customer customer)
        {
            if (customer == null)
                throw new CustomerServicesException("Cliente não pode ser nulo.");
            try
            {
                using (var context = new DBContext())
                {
                    customer.SetDateTimeNowLastAttendance();

                    var customerDb = context.Customers
                       .Include(x => x.ProductsAndServices)
                       .Single(c => c.Id == customer.Id);

                    context.Entry(customerDb).CurrentValues.SetValues(customer);

                    foreach (var productAndService in customerDb.ProductsAndServices.ToList())
                        if (!customer.ProductsAndServices.Any(s => s.Id == productAndService.Id))
                            customerDb.ProductsAndServices.Remove(productAndService);

                    foreach (var newProductAndService in customer.ProductsAndServices)
                    {
                        var productAndServiceDb = customerDb.ProductsAndServices.SingleOrDefault(s => s.Id == newProductAndService.Id);
                        if (productAndServiceDb != null)
                            context.Entry(productAndServiceDb).CurrentValues.SetValues(newProductAndService);
                        else
                            customerDb.ProductsAndServices.Add(context.ProductsAndServices.Find(newProductAndService.Id));
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar atualizar cliente: {ex.Message}.");
            }
        }
    }
}