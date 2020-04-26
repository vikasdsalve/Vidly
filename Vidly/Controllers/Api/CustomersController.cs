using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using AutoMapper;

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
        public IHttpActionResult GetCustomers()
        {
            //return _context.Customer.ToList().Select(Mapper.Map<Customer,CustomerDto>);

            var customerDtos = _context.Customer
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //GET /api/Customers/1
        public IHttpActionResult GetCustomers(int id)
        {
            var Customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (Customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(Customer));
        }

        //POST   /api/Customers/
        [HttpPost]
        public IHttpActionResult CreateCustomers(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customer.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return  Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/Customer/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var CustomerInDb = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (CustomerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, CustomerInDb);

            //CustomerInDb.Name = customerDto.Name;
            //CustomerInDb.Birthdate = customerDto.Birthdate;
            //CustomerInDb.IsSubscribedtoNewsletter = customerDto.IsSubscribedtoNewsletter;
            //CustomerInDb.MembershipTypeId = customerDto.MembershipTypeId;

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
