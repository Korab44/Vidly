using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{

    public class Genres
    {
        internal static object ToList()
        {
            throw new NotImplementedException();
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
