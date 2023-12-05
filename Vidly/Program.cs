using Vidly.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Vidly.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Vidly.Models;
using Optivem.Framework.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        // Add services to the container.
        Mapper.Initialize(c => c.AddProfile<MappingProfile>());


        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("Lidhja")
        ));



        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultUI()
       .AddDefaultTokenProviders();
        builder.Services.AddScoped<UserManager<ApplicationUser>>();
        builder.Services.AddScoped<SignInManager<ApplicationUser>>();
        builder.Services.AddScoped<AppDbContext>();



        // Existing code...


        builder.Host.ConfigureServices((hostContext, services) =>
        {

            //services.AddControllers(options =>
            //{
            //    options.Filters.Add<Vidly.Helpers.CustomAuthorizeFilter>();
            //});


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/LogIn"; // Specify the login path here
                    options.AccessDeniedPath = "/"; // Specify the home page path here
                });

            // Add other services to the container
            // ...
        });

        // configure SSL
        builder.WebHost.ConfigureKestrel((context, options) =>
        {
            options.ListenAnyIP(443, listenOptions =>
            {
                listenOptions.UseHttps();
            });
        });



        builder.Services.AddMemoryCache();


        var app = builder.Build();

        AppDbContext.Seed(app);
        AppDbContext.SeedUsersAndRolesAsync(app).Wait();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();


        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthentication();
        app.UseAuthorization();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.MapControllerRoute(
            name: "default",
            pattern: "api/{controller}/{action}/{id?}",
            defaults: new { controller = "Homes", action = "GetCustomers" }
        );


        app.MapControllers();





        app.Run();
    }
}