using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustmerViewModel
            {
                MembershipType = membershipTypes
            };
            return View(viewModel);
        }
            
        // GET: Customers
        public ActionResult Index()
        {
            //var customers = GetCustomers();
            var customers = _context.Customer.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
        public ActionResult Details(int id)
        {
            //var customers = GetCustomers().SingleOrDefault(c => c.Id == id);
            var customers = _context.Customer.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }

        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1 , Name ="John Smith"},
        //        new Customer { Id = 2 , Name ="Mary Williams"}
        //    };
        //}
    }
}