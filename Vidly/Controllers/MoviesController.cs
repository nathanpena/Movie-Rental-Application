using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Vidly.Models;
using Vidly.ViewModels;

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

        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer One"},
                new Customer {Name = "Customer Two"}
            };
            
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {

            return View(GetMovie(id));
        }


        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Create(Movie movie)
        {
            movie.DateAdded = DateTime.Today;
            _context.Movies.Add(movie);
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {

            var viewModel = new MovieFormViewModel
            {
                Genres = GetGenres(),
                Movie = GetMovie(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Update(Movie movie)
        {
            var dbMovie = GetMovie(movie.Id);
            Map(dbMovie, movie);
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByRelaseDate(int year, int month)
        {
            return Content($"year={year}&month={month}");
        }

        private IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        private Movie GetMovie(int id)
        {
            return _context.Movies.Include(m => m.Genre).SingleOrDefault(movie => movie.Id == id);
        }

        private void Map(Movie dbMovie, Movie movie)
        {
            dbMovie.Name = movie.Name;
            dbMovie.ReleaseDate = movie.ReleaseDate;
            dbMovie.GenreId = movie.GenreId;
            dbMovie.NumberInStock = movie.NumberInStock;
        }
    }
}