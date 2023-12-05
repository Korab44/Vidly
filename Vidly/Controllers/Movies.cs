using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.Models.Roles;
using Vidly.Models.VM;


namespace Vidly.Controllers
{
    //[Authorize(AuthenticationSchemes = "Cookies")]
    
    public class Movies : Controller
    {
        private readonly AppDbContext _appDbContext;

        public Movies(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public IActionResult Index()
        {
            if (User.IsInRole(UserRoles.Admin))
                return View("List");
             return View("ReadOnlyList");
        }
        [Authorize(Roles = UserRoles.Admin, AuthenticationSchemes = "Cookies")]
        // the commented method here in New method code is another method with boolean value to show different titles in the beggining of page 
        public IActionResult New(/*RandomMovieVM movie*/)
        {
            //movie.isNewMovie = true;

            ViewBag.Message = "New Movie";
            return View("CustomFormMovie"/*, movie*/);
        }
        public IActionResult Edit(int id)
        {
            var movie = _appDbContext.Movies.FirstOrDefault(c => c.Id == id);

            if (movie != null)
            {
                var viewModel = new RandomMovieVM
                {
                    Movies = movie,

                };
                ViewBag.Messages = "Edit Movie";
                return View("CustomFormMovie", viewModel);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(RandomMovieVM movie)

        {
            try
            {
                if (movie.Movies.Id == 0)
                {
                    _appDbContext.Movies.Add(movie.Movies);
                }
                else
                {

                    var existingMovie = _appDbContext.Movies.Single(x => x.Id == movie.Movies.Id);

                    existingMovie.Name = movie.Movies.Name;
                    existingMovie.MovieGenre = movie.Movies.MovieGenre;
                    existingMovie.ReleaseDateTime = movie.Movies.ReleaseDateTime;
                    existingMovie.StockNumber = movie.Movies.StockNumber;
                    _appDbContext.Movies.Update(existingMovie);
                }
                _appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        //public IActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };
        //    var customer = new List<Customer>
        //    {
        //        new Customer() { Name = "Customer 1" },
        //        new Customer() { Name = "Customer 2" }
        //    };
        //    var viewModel = new RandomMovieVM
        //    {
        //        Movies = movie,
        //        Customers = customer
        //    };

        //    return View(viewModel);
        //}
        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}
