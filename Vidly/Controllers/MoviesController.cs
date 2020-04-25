using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Random()
        {
            //var movie = new Movie() { Name = "Sherk!" };
            //var customers = new List<Customer>
            //{
            //    new Customer {  Name="Customer 1"},
            //    new Customer {  Name="Customer 1"}
            //};

            //var viewModel = new RandomMovieViewModel
            //{
            //    Movie = movie,
            //    Customers = customers

            //};

            //return View(viewModel);

            //ViewData["Movie"] = movie;
            //ViewBag.Movie = movie;
            //return View();
            //return View(movie);
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1 , sortBy ="name" });

            //var movies = GetMovies();
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }


        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";
            return Content(string.Format("pageIndex={0}&sortBy={1}",pageIndex,sortBy));
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:range(1,12)}")]
        public ActionResult ByReleasDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id =1 , Name = "Sherk"},
        //        new Movie { Id =2 , Name = "Wall-e"},
        //    };
        //}


        public ActionResult Details(int id)
        {
            //var customers = GetCustomers().SingleOrDefault(c => c.Id == id);
            var customers = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }
    }
}