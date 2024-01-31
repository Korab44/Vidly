using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Dtos;
using Vidly.Migrations;
using Vidly.Models;
using Vidly.Models.VM;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRental : ControllerBase
    {
        private readonly AppDbContext _context;

        public NewRental(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("CreateNewRentals")]

        public IActionResult CreateNewRentals([FromForm] NewRentalDto newRental)
        {
      
            var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);
            var movies = _context.Movies.Where(m => newRental.MoviesIds.Contains(m.Id));
           
            foreach (var movie in movies)
            {
                movie.NumberAvailable--;
                
                    var rental = new Rental
                    {
                        Customer = customer,
                        Movie = movie,
                        RentalDate = DateTime.Now

                    };
                   
                    _context.Rental.Add(rental);
                

            }
            _context.SaveChanges();
            return Ok();
            
        }
    }
}
