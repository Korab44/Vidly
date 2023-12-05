using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Vidly.Models.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Microsoft.AspNetCore.Builder;
using Vidly.Models.VM;
using System.Threading.Tasks;


namespace Vidly.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
            });


        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();
            }
        }

                public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
                {
                    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                    {

                        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        if (!await roleManager.RoleExistsAsync(Vidly.Models.Roles.UserRoles.Admin))
                            await roleManager.CreateAsync(new IdentityRole(Vidly.Models.Roles.UserRoles.Admin));
                        if (!await roleManager.RoleExistsAsync(Vidly.Models.Roles.UserRoles.User))
                            await roleManager.CreateAsync(new IdentityRole(Vidly.Models.Roles.UserRoles.User));

                        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                        string adminUserEmail = "admin@etickets.com";
                        var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                        if (adminUser == null)
                        {
                            var newAdminUser = new ApplicationUser()
                            {
                                FullName = "Admin User",
                                UserName = "admin",
                                Email = adminUserEmail,
                                EmailConfirmed = true
                            };
                            await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                            await userManager.AddToRoleAsync(newAdminUser, Vidly.Models.Roles.UserRoles.Admin);
                        }


                        string userEmaill = "user@etickets.com";
                        var user = await userManager.FindByEmailAsync(userEmaill);
                        if (user == null)
                        {
                            var newUser = new ApplicationUser()
                            {
                                FullName = "Application User",
                                UserName = "app-user",
                                Email = userEmaill,
                                EmailConfirmed = true
                            };
                            await userManager.CreateAsync(newUser, "Coding@123?");
                            await userManager.AddToRoleAsync(newUser, Vidly.Models.Roles.UserRoles.User);
                        }

                    }
                }
            }
            }

            
        