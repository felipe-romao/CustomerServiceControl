using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CustomerServiceControl.Models;

namespace CustomerServiceControl.Services
{
    public class ProductAndServiceServices : IProductAndServiceServices
    {
        public void Add(ProductAndService productAndService)
        {
            try
            {
                using (var context = new DBContext())
                {
                    context.ProductsAndServices.Add(productAndService);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar adicionar este item: {ex.Message}.");
            }
        }

        public void Delete(int? productAndServiceId)
        {
            try
            {
                if (productAndServiceId == null)
                    throw new CustomerServicesException("Informe um Id válido.");

                using (var context = new DBContext())
                {
                    var productAndService = context.ProductsAndServices.Find(productAndServiceId);
                    context.ProductsAndServices.Remove(productAndService);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar excluir o id '{productAndServiceId}': {ex.Message}.");
            }
        }

        public ICollection<ProductAndService> GetAll()
        {
            try
            {
                using (var context = new DBContext())
                {
                    return context.ProductsAndServices.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar obter todos os items: {ex.Message}.");
            }
        }

        public ProductAndService GetById(int? productAndServiceId)
        {
            try
            {
                if (productAndServiceId == null)
                    throw new CustomerServicesException("Informe um Id válido.");

                using (var context = new DBContext())
                {
                    return context.ProductsAndServices.Find(productAndServiceId);
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar encontrar o item id '{productAndServiceId}': {ex.Message}.");
            }
        }

        public void Update(ProductAndService productAndService)
        {
            try
            {
                if (productAndService.Id == null)
                    throw new CustomerServicesException("Item selecionado não possui Id.");

                using (var context = new DBContext())
                {
                    context.Entry(productAndService).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new CustomerServicesException($"Erro ao tentar atualizar um item: {ex.Message}.");
            }
        }
    }
}