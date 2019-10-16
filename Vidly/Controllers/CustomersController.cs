using System;
using System.Data.Entity;
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

        private readonly ApplicationDbContext _context;
          
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = GetMembershipTypes();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
 
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [HttpPost]
        public ActionResult Update(Customer customer)
        {

            var DbCustomer = GetCustomer(customer.Id);
            Map(DbCustomer, customer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = GetCustomer(id);

            if (customer == null)
                return HttpNotFound();

            var membershipTypes = GetMembershipTypes();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = customer
            };

            return View(viewModel);
        }

        public ActionResult Index()
        {
            
            var viewModel = new IndexCustomerViewModel { Customers = _context.Customers.Include(c => c.MembershipType).ToList() };
            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customer = GetCustomer(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }

        }

        private Customer GetCustomer(int id)
        {
            return _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
        }

        private IEnumerable<MembershipType> GetMembershipTypes()
        {
            return _context.MembershipTypes.ToList();
        }

        private void Map(Customer DbCustomer, Customer customer)
        {
            DbCustomer.Name = customer.Name;
            DbCustomer.Birthdate = customer.Birthdate;
            DbCustomer.MembershipTypeId = customer.MembershipTypeId;
            DbCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
        }

    }
}
