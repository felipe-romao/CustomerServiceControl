using CustomerServiceControl.Models;
using CustomerServiceControl.Services;
using CustomerServiceControl.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerServiceControl.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerServices customerServices;
        private readonly IProductAndServiceServices productAndServiceServices;

        public CustomerController()
        {
            this.customerServices = new CustomerServices();
            this.productAndServiceServices = new ProductAndServiceServices();
        }

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel();
            try
            {
                viewModel.Customer = new Customer();
                viewModel.ProductAndServiceItems = this.CreateProductAndServiceItems(this.productAndServiceServices.GetAll().ToList(), new List<ProductAndService>());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao tentar criar um cliente: {ex.Message}");
            }
            return View("Customer", viewModel);
        }

        [HttpPost]
        public ActionResult Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = viewModel.Customer;
                    customer.ProductsAndServices = this.GetProductAndServiveListFromItemSelected(viewModel.ProductAndServiceItems);
                    this.customerServices.Add(customer);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao tentar criar um cliente: {ex.Message}");
                }
            }
            return View("Customer", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            try
            {
                var customerEdited = this.customerServices.GetById(id);
                var viewModel = new CustomerViewModel
                {
                    Customer = customerEdited,
                    ProductAndServiceItems = this.CreateProductAndServiceItems(this.productAndServiceServices.GetAll().ToList(), customerEdited.ProductsAndServices.ToList())
                };
                return View("Customer", viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao tentar alterar um cliente: {ex.Message}");
            }
            return View("Customer", new CustomerViewModel());
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customerEdited = viewModel.Customer;
                    customerEdited.ProductsAndServices = this.GetProductAndServiveListFromItemSelected(viewModel.ProductAndServiceItems);
                    this.customerServices.Update(customerEdited);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erro ao tentar alterar um cliente: {ex.Message}");
                }
            }
            return View("Customer", viewModel);
        }

        public ActionResult Delete(int? id)
        {
            var viewModel = new CustomerViewModel();
            try
            {
                viewModel.Customer = this.customerServices.GetById(id);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao tentar excluir um cliente: {ex.Message}");
            }
            return View("ConfirmDelete", viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            this.customerServices.Delete(id);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Report()
        {
            try
            {
                return View(this.customerServices.GetAllOrderByName().ToList());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao tentar gerar relatório: {ex.Message}");
            }
            return View();
        }

        private List<ProductAndServiceItem> CreateProductAndServiceItems(List<ProductAndService> productAndServiceList, List<ProductAndService> productAndServiceSelected)
        {
            var productAndServiceItems = new List<ProductAndServiceItem>();
            foreach (var productAndService in productAndServiceList)
            {
                productAndServiceItems.Add(new ProductAndServiceItem
                {
                    Id = productAndService.Id,
                    Display = productAndService.Description,
                    IsChecked = productAndServiceSelected.Where(x => x.Id == productAndService.Id).Any()
                });
            }
            return productAndServiceItems;
        }

        private List<ProductAndService> GetProductAndServiveListFromItemSelected(IList<ProductAndServiceItem> productAndServiceItems)
        {
            var list = new List<ProductAndService>();
            foreach(var item in productAndServiceItems)
            {
                if (item.IsChecked)
                    list.Add(this.productAndServiceServices.GetById(item.Id));
            }
            return list;
        }
    }
}