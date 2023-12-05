using System.ComponentModel.DataAnnotations;

namespace Vidly.Models.VM
{
    public class RegisterVm
    {
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        [Display(Name = "Driving License")]
        public string DrivingLicense { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

  
    }
}
