using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
    [NotMapped]
    public class Genres
    {
       
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
