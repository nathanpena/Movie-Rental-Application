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
       
       public ActionResult Index()
        {
            var customers = GetCustomers();

            var viewModel = new IndexCustomerViewModel {Customers = customers};
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {                
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
            
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Id = 1, Name = "John Orlando"},
                new Customer {Id = 2, Name = "Hailey Bieber"},
                new Customer { Id = 3, Name = "Justin Bieber" }
            };
        }
    }
}
