using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vidly.Dtos
{
    public class NewRentalDto : Controller
    {
    public int CustomerId { get; set; }
    public List<int> MoviesIds { get; set; }

    }
}
