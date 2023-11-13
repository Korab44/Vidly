using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Vidly.Models;
using Vidly.Models.VM;
using Vidly.Migrations;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        protected override void Dispose(bool disposing)
        {
            _appDbContext.Dispose();
        }

        public ActionResult Index()
        {
            var customer = _appDbContext.Customers.Include(m => m.MembershipType).ToList();
            return View(customer);
        }
        public ActionResult New()
        {
            var membereshipType = _appDbContext.MembershipTypes.ToList();
            var viewModel = new CustomerFormVM
            {
                Customers = new Customer(),
                MembershipTypes = membereshipType
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CustomerFormVM model)
        {
            var customer = model.Customers; // Retrieve the customer object from the model
            try
            {   

                if (customer.Id == 0)
                {
                        _appDbContext.Customers.Add(customer);
                }
                else
                {
                    var customerInDb = _appDbContext.Customers.Single(c => c.Id == customer.Id);
                    customerInDb.Name = customer.Name;

                    customerInDb.MembershipTypeId = customer.MembershipTypeId;
                    customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                    customerInDb.Birthday = customer.Birthday;

                    _appDbContext.Customers.Update(customerInDb);
                }
                
                _appDbContext.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                var modelVM = new CustomerFormVM
                {
                    Customers = model.Customers,
                    MembershipTypes = _appDbContext.MembershipTypes.ToList()
                };


                return View("CustomerForm", modelVM);
                
            }
        }

        public ActionResult Edit(int id)
        {
            var customer = _appDbContext.Customers
            .FirstOrDefault(c => c.Id == id);

            var viewModel = new CustomerFormVM()
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
        public ActionResult Movies()
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