using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre MovieGenre { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDateTime { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int StockNumber { get; set; }

     

        public enum Genre
        {
            Action,
            Comedy,
            Thriller,
            Drama,
            Documentary,
            Horror,
            Cartoon
        }

    }
}