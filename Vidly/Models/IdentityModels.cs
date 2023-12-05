using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Phone { get; set; }
        public string DrivingLicense {  get; set; }
        public string FullName { get; set; }

        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    var result = await manager.CreateAsync(this);

        //    if (result.Succeeded)
        //    {
        //        var userIdentity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
        //        userIdentity.AddClaim(new Claim(ClaimTypes.Email, Email));
        //        // Add custom claims to the userIdentity if needed.
        //        return userIdentity;
        //    }

        //    return null;
        //}
        //public class AppDbContext : IdentityDbContext<ApplicationUser>
        //{
        //    public DbSet<Customer> Customers { get; set; }
        //    public DbSet<Movie> Movies { get; set; }
        //    public DbSet<MembershipType> MembershipTypes { get; set; }
        //}
    }
}
