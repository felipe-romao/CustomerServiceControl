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
    public class HomeController : Controller
    {
        private readonly ICustomerServices customerServices;


        public HomeController()
        {
            this.customerServices = new CustomerServices();
        }

        public ActionResult Index()
        {
            var viewModel = new RootViewModel()
            {
                Customers = this.customerServices.GetAll(),
            };

            return View(viewModel);
        }

        public ActionResult CustomerSearch(RootViewModel viewModel)
        {
            var customersFromSearch = this.customerServices.GetByFilter(viewModel.SearchType, viewModel.SearchValue);
            viewModel.Customers = customersFromSearch;

            return View("Index", viewModel);
        }
    }
}