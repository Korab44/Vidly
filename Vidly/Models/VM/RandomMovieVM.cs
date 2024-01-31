using static Vidly.Models.Movie;

namespace Vidly.Models.VM
{
    public class RandomMovieVM
    {
        public Movie Movies { get; set; }
        public List<Customer> Customers { get; set; }

        public List<Genres> Genres { get; set; }
        public bool isNewMovie { get; set; }

        public RandomMovieVM()
        {
            Movies = new Movie(); // Initialize the Movies property
            Customers = new List<Customer>(); // Initialize the Customers property
            Genres = new List<Genres>(); // Initialize the Genres property
            isNewMovie = false; // Set the default value for isNewMovie
        }
    }
}
