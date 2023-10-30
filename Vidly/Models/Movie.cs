namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre MovieGenre { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public DateTime DateAdded { get; set; }
        public int StockNumber { get; set; }

        public Movie()
        {
            DateAdded = DateTime.Now;
        }


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