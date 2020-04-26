using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET /api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customer.ToList();
        }

        //GET /api/Customers/1
        public Customer GetCustomers(int id)
        {
            var Customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (Customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Customer;
        }

        //POST   /api/Customers/
        [HttpPost]
        public Customer CreateCustomers(Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Customer.Add(customer);
            _context.SaveChanges();
            return customer;
        }

        //PUT /api/Customer/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var CustomerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            CustomerInDb.Name = customer.Name;
            CustomerInDb.Birthdate = customer.Birthdate;
            CustomerInDb.IsSubscribedtoNewsletter = customer.IsSubscribedtoNewsletter;
            CustomerInDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        //DELETE /api/customer/
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var CustomerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customer.Remove(CustomerInDb);
            _context.SaveChanges();
        }
    }

}
