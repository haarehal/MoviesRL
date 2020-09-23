using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesRL.Models;
using MoviesRL.ViewModels;
using System.Data.Entity;

namespace MoviesRL.Controllers
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

        /*private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }*/

        public ViewResult Index()
        {
            //var movies = GetMovies();
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null) return HttpNotFound();
            return View(movie);
        }

        public ActionResult Random() // /movies/random
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1"},
                new Customer {Name = "Customer 2"}
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);

            // Prvi nacin prosljedjivanja parametra u View
            // return View(movie);
            // Neka imamo: var viewResult = new ViewResult();
            // View metoda se pobrine za to da movie objekat bude dodijeljen polju viewResult.ViewData.Model
            // ViewData nije obicni Dictionary, nego ViewData dictionary; ViewData mozemo koristi kao key-value parove ili sa Model poljem ako hocemo da radimo sa objektom

            // Drugi tipovi ActionResult-a (svi su zapravo pomocna metoda iz naslijedjene klase Control)
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });

            /*
            // Drugi nacin prosljedjivanja parametra u View
            ViewData["Movie"] = movie;
            return View();

            // Treci nacin prosljedjivanja parametra u View
            ViewBag.Movie = movie;
            return View();
            */
        }

        /*public ActionResult Edit(int id)
        {
            return Content(id.ToString());
        }*/

        public ActionResult Index2(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue) pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy)) sortBy = "name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")] // Drugi nacin pravljenja rute (attribute routing)
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _context.Genres.ToList(),
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {   
                    Genres = _context.Genres.ToList()
                    
                };

                return View("MovieForm", viewModel);
            }

            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                // Prvi nacin updateovanja u bazi
                //TryUpdateModel(movieInDb);

                // Drugi nacin updateovanja u bazi
                // Mapper.Map(movie, movieInDb);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.DateAdded = DateTime.Now;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}