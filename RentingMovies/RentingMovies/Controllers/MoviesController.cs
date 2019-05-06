using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentingMovies.Models;
using RentingMovies.ViewModels;

namespace RentingMovies.Controllers
{
    public class MoviesController : Controller
    {

        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) // dbcontext is disposable context
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var generals = _context.Generals.ToList();

            var viewModels = new MovieFormViewModel
            {
                Generals = generals
            };
            return View("MovieForm",viewModels);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GeneralId = movie.GeneralId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Generals = _context.Generals.ToList()
            };
            return View("MovieForm", viewModel);
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.General).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.General).SingleOrDefault(m => m.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }


       // private IEnumerable<Movie> GetMovies()
        //{
          //  return new List<Movie>
            //{
              //  new Movie{Id = 1, Name = "Captain Marvel"},
                //new Movie{Id = 2, Name = "Avengers"}
            //};
       // }

        //public ActionResult Random()
        //{
        //  var movie = new Movie() { Name = "Sheela!!"};

        //var customers = new List<Customer>
        //{
        //  new Customer { Name = "Customer1" },
        //new Customer { Name = "Customer2" }
        //};

        //var viewModel = new RandomMovieViewModel
        //{
        //  Movie = movie,
        //Customers = customers
        //From here 'Customers' come: public List<Customer> Customers { get; set; }
        //};
        //return View(viewModel);
        //}


        ////////////////////////////////////////////////////////
        // GET: Movies/Random
        //public ActionResult Random()
        //{ 
        // Create INstance of MOvie Model
        //  var movie = new Movie() { Name = "Ramleela!!" };
        //return Content("Helloaaaaaaw World");
        //return new EmptyResult();
        //return RedirectToAction("Index", "Home", new { page = 1, sortBy= "name"});
        //return View(movie); //put Movie model inside the view so that we can render it.
        //Used movie variable.
        //}

        //////////////////////////////////////////////////////////

        //public ActionResult Edit(int id) //id is a paramter
        //{

        //     return Content("Hello World" + id);
        //}

        //
        ////////////////////////////////////////////////////////
        //movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        // {
        //   if (pageIndex.HasValue)
        //     pageIndex = 1;

        //if (String.IsNullOrWhiteSpace(sortBy))
        //  sortBy = "Name";

        //return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}

        ///////////////////////////////////////////////////////


        //this is the example of paramter(id) embedded in URL
        //Request: http://localhost:49836/Movies/edit/7
        //MVC: compare map Request data i.e. 7 with Action value i.e. id.
        //Action: id
        //so, request>> mvc >> action
        //result will be Helloworld7

        ///////////////////////////////////////////////////////


        //this is Attribute Routing. 
        //This called one method in RouteConfig file "routes.MapMvcAttributeRoutes();"
        //Then pass the url with constraints over inside "[Route()]"
        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{

        //  return Content(year + "/" + month);
        //}

    }
}