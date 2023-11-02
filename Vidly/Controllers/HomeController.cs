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
        public IActionResult New() 
        {
            var membereshipType = _appDbContext.MembershipTypes.ToList();
            var viewModel = new CustomerFormVM
            {
                MembershipTypes = membereshipType
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        public IActionResult Save(CustomerFormVM model) 
            {
            
            if (model.Customers.Id == 0) { 
            _appDbContext.Customers.Add(model.Customers);
            } else
            {
                var customer = _appDbContext.Customers.Single(c => c.Id == model.Customers.Id);
                customer.Name = model.Customers.Name;
                customer.Birthday = model.Customers.Birthday;
                customer.MembershipTypeId = model.Customers.MembershipTypeId;
                customer.IsSubscribedToNewsLetter = model.Customers.IsSubscribedToNewsLetter;
                _appDbContext.Customers.Update(customer);
            }
            _appDbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Edit(int id)
        {
            var customer = _appDbContext.Customers
            .FirstOrDefault(c => c.Id == id);

            var viewModel= new CustomerFormVM()
            {
                Customers = customer,
                MembershipTypes = _appDbContext.MembershipTypes.ToList()
            };

            if (customer != null)
            {
                return View("CustomerForm", viewModel);
            }
            else
            {
                return NotFound();
            }
        }
        //[HttpPost]
        //public IActionResult Create(CustomerMovieVM model)
        //{

        //    _appDbContext.Customers.Add(model.Customers);
        //    _appDbContext.SaveChanges();
        //    return RedirectToAction("Index", "Home");
        //}
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