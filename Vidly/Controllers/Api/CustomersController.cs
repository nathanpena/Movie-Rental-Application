using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dto;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }

        public IHttpActionResult GetCustomer(int id)
        {
            var customer = GetCustomerHelper(id);
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));


        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
             
            var dbCustomer = GetCustomerHelper(id);
            if (dbCustomer == null)
                return NotFound();

            Mapper.Map(customerDto, dbCustomer);
            
            _context.SaveChanges();
            
            return Ok(Mapper.Map<Customer, CustomerDto>(dbCustomer));
        }

        [HttpDelete]
        public HttpStatusCode DeleteCustomer(int id)
        {
            var customer = GetCustomerHelper(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return HttpStatusCode.OK;
        }

        private Customer GetCustomerHelper(int id)
        {
            return _context.Customers.SingleOrDefault(c => c.Id == id);

        }

    }
}
