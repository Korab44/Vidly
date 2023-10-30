using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Models;
using Vidly.Models.VM;


namespace Vidly.Controllers
{
    public class Movies : Controller
    {
        private readonly AppDbContext _appDbContext;

        public Movies(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var movie = _appDbContext.Movies.ToList();
            return View(movie);
        }
        public IActionResult Details(int id)
        {
            var movie = _appDbContext.Movies.FirstOrDefault(c => c.Id == id);
            if (movie != null)
            {
                return View(movie);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            var customer = new List<Customer>
            {
                new Customer() { Name = "Customer 1" },
                new Customer() { Name = "Customer 2" }
            };
            var viewModel = new RandomMovieVM
            {
                Movies = movie,
                Customers = customer
            };

            return View(viewModel);
        }
        [Route("movies/released/{year}/{month}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}
