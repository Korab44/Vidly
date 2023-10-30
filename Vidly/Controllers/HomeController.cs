using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.Models.VM;
using Vidly.Migrations;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        protected override void Dispose(bool disposing)
        {
            _appDbContext.Dispose();
        }

        public IActionResult Index()
        {
            var customer = _appDbContext.Customers.Include(m => m.MembershipType).ToList();
            return View(customer);
        }

        public IActionResult Details(int id)
        {
            var customer = _appDbContext.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefault(c => c.Id == id);

            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Movies()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}