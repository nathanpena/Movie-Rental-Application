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
            var customers = new List<Customer>();
            customers.Add(new Customer {Id = 1, Name = "John Orlando"});
            customers.Add(new Customer { Id = 2, Name = "Hailey Bieber" });
            customers.Add(new Customer { Id = 3, Name = "Justin Bieber" });

            var viewModel = new IndexCustomerViewModel {Customers = customers};
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customers = new List<Customer>
            {
                new Customer {Id = 1, Name = "John Orlando"},
                new Customer {Id = 2, Name = "Hailey Bieber"},
                new Customer { Id = 3, Name = "Justin Bieber" }
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