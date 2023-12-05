using System.ComponentModel.DataAnnotations;

namespace Vidly.Models.VM
{
    public class LoginVm
    {
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
