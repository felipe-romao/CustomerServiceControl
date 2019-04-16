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
            var viewModel = new RootViewModel();
            try
            {
                viewModel.Customers = this.customerServices.GetAll();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao carregar página inicial: {ex.Message}");
            }

            return View(viewModel);
        }

        public ActionResult CustomerSearch(RootViewModel viewModel)
        {
            try
            {
                var customersFromSearch = this.customerServices.GetByFilter(viewModel.SearchType, viewModel.SearchValue);
                viewModel.Customers = customersFromSearch;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao tentar fazer uma busca: {ex.Message}");
            }
            return View("Index", viewModel);
        }
    }
}