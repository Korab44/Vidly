using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Data
{
    public class AppDbContext : IdentityDbContext
    {
        
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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
        
    }
}
