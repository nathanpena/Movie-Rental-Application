using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
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
            var customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "John Pena"},
                new Customer {Id = 2, Name = "Greg Solis"}
            };

            var customer = customers.Find(c => c.Id == id);

            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                return HttpNotFound();
            }
            
        }
    }
}