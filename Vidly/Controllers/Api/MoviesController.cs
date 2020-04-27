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
    public class MoviesController : ApiController
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


        //GET /api/Movies
        public IHttpActionResult GetMovies(string query = null)
        {
            // return _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);
            var moviesQuery = _context.Movies
                .Include(c => c.Genre)
              .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            var moviesDtos= moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesDtos);
        }


        //GET /api/Movies/1
        public IHttpActionResult GetMovies(int id)
        {
            var Movies = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (Movies == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(Movies));
        }


        //POST   /api/Movies/
        [HttpPost]
        public IHttpActionResult CreateMovies(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var movies = Mapper.Map<MovieDto, Movie>(MovieDto);

            _context.Movies.Add(movies);
            _context.SaveChanges();

            MovieDto.Id = movies.Id;

            return Created(new Uri(Request.RequestUri + "/" + movies.Id), MovieDto);
        }

        //PUT /api/Movies/1
        [HttpPut]
        public void UpdateCustomer(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var MoviesInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (MoviesInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDto, MoviesInDb);

            _context.SaveChanges();
        }

        //DELETE /api/Movies/
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var MoviesInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (MoviesInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(MoviesInDb);
            _context.SaveChanges();
        }
    }
}
