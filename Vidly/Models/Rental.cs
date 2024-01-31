using Microsoft.AspNetCore.Mvc;

namespace Vidly.Models
{
    public class Rental { 
        public int Id { get; set; }
        public DateTime RentalDate { get; set; } = DateTime.Now;
        public DateTime? RentalReturnDate { get; set; }
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }

    }
}
