using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.Data;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesApiController(AppDbContext context)
        {
            _context = context;
        }
        // GET /api/moviesapi
        [HttpGet("GetMovies")]
        public IEnumerable<Movie> GetMovies()
        {
            var movies = _context.Movies.ToList();
            return movies;
        }

        // GET /api/moviesapi/{id}

        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovies(int id)
        {
            var movie = _context.Movies.FirstOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }


        // POST /api/moviesapi Postman: Post and Body - Form data 
        [HttpPost("CreateMovie")]

        public IActionResult CreateMovies([FromForm] Movie movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Movies.Add(movies);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetMovies), new { id = movies.Id }, movies);
        }

        // PUT /api/moviesapi/{id} Postman: Put and Body - Raw data 
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, Movie movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var moviesInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (moviesInDb == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            moviesInDb.Name = movies.Name;
            moviesInDb.MovieGenre = movies.MovieGenre;
            moviesInDb.ReleaseDateTime = movies.ReleaseDateTime;
            moviesInDb.DateAdded = movies.DateAdded;
            moviesInDb.StockNumber = movies.StockNumber;

            _context.SaveChanges();

            return Ok(moviesInDb);
        }

            // DELETE /api/moviesapi/{id} Postman: Delete and Body - None
            [HttpDelete("{id}")]
            public ActionResult<Movie> DeleteCustomer(int id)
            {
                var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
                if (movieInDb == null)
                {
                    return NotFound();
                }
                _context.Movies.Remove(movieInDb);
                _context.SaveChanges();
                return movieInDb;
            }
    }
}
