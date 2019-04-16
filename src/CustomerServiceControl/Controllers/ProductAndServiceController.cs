using CustomerServiceControl.Models;
using CustomerServiceControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerServiceControl.Controllers
{
    public class ProductAndServiceController : Controller
    {
        private readonly IProductAndServiceServices services;

        public ProductAndServiceController()
        {
            this.services = new ProductAndServiceServices();
        }

        public ActionResult Index()
        {
            return View(this.services.GetAll());
        }

        public ActionResult Create()
        {
            return View("ProductAndService", new ProductAndService());
        }

        [HttpPost]
        public ActionResult Create(ProductAndService productAndService)
        {
            if (ModelState.IsValid)
            {
                this.services.Add(productAndService);
                return RedirectToAction("Index");
            }
            return View(productAndService);
        }

        public ActionResult Edit(int? id)
        {
            var productAndService = this.services.GetById(id);
            return View("ProductAndService", productAndService);
        }

        [HttpPost]
        public ActionResult Edit(ProductAndService productAndService)
        {
            if (ModelState.IsValid)
            {
                this.services.Update(productAndService);
                return RedirectToAction("Index");
            }
            return View(productAndService);
        }

        public ActionResult Delete(int? id)
        {
            return View("ConfirmDelete", this.services.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            this.services.Delete(id);
            return RedirectToAction("Index");
        }
    }
}