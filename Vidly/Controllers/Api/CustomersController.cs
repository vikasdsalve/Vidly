using System;
using System.Collections.Generic;
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
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customer.ToList().Select(Mapper.Map<Customer,CustomerDto>);
        }

        //GET /api/Customers/1
        public CustomerDto GetCustomers(int id)
        {
            var Customer = _context.Customer.SingleOrDefault(c => c.Id == id);

            if (Customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<Customer,CustomerDto>(Customer);
        }

        //POST   /api/Customers/
        [HttpPost]
        public CustomerDto CreateCustomers(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customer.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
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
