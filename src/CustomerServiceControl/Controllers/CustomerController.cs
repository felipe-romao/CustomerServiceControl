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
            return View(this.customerServices.GetAll());
        }

        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel
            {
                Customer = new Customer(),
                ProductAndServiceItems = this.CreateProductAndServiceItems(this.productAndServiceServices.GetAll().ToList(), new List<ProductAndService>())
            };
            return View("Customer", viewModel);
        }

        [HttpPost]
        public ActionResult Create(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = viewModel.Customer;
                customer.ProductsAndServices = this.GetProductAndServiveListFromItemSelected(viewModel.ProductAndServiceItems);
                this.customerServices.Add(customer);
                return RedirectToAction("Index", "Home");
            }
            return View("Customer", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            var customerEdited = this.customerServices.GetById(id);
            var viewModel = new CustomerViewModel
            {
                Customer = customerEdited,
                ProductAndServiceItems = this.CreateProductAndServiceItems(this.productAndServiceServices.GetAll().ToList(), customerEdited.ProductsAndServices.ToList())
            };
            return View("Customer", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var customerEdited = viewModel.Customer;
                customerEdited.ProductsAndServices = this.GetProductAndServiveListFromItemSelected(viewModel.ProductAndServiceItems);
                this.customerServices.Update(customerEdited);
                return RedirectToAction("Index", "Home");
            }
            return View("Customer", viewModel);
        }

        public ActionResult Delete(int? id)
        {
            var viewModel = new CustomerViewModel
            {
                Customer = this.customerServices.GetById(id),
            };
            return View("ConfirmDelete", viewModel);
        }

        public ActionResult Report()
        {
            return View(this.customerServices.GetAllOrderByName().ToList());
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