using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            var customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "John Pena"},
                new Customer {Id = 2, Name = "Greg Solis"}
            };

            var viewModel = new IndexCustomerViewModel {Customers = customers};
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customer = new Customer {Id = id, Name = "Nathan Pena"};
            return View(customer);
        }
    }
}