using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using Vidly.Data;
using Vidly.Dtos;
using Vidly.Migrations;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]

    public class HomesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/homes
        [HttpGet("GetCustomers")]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return customers;
        }

        // GET /api/homes/{id}

        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Mapper.Map<Customer, CustomerDto>(customer);
        }


        // POST /api/homes Postman: Post and Body - Form data 
        [HttpPost("CreateCustomer")] 

        public IActionResult CreateCustomer([FromForm] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.MembershipType = GetMembershipTypeById(customer.MembershipTypeId);

            if (customer.MembershipTypeId == 2 || customer.MembershipTypeId == 3 || customer.MembershipTypeId == 4)
            {
                var today = DateTime.Today;

                if (!customer.Birthday.HasValue)
                {
                    return BadRequest("Birthday is required.");
                }
                else
                {
                    var age = today.Year - customer.Birthday.Value.Year;
                    if (age < 18)
                    {
                        return BadRequest("Customer should be at least 18 years old to go on a membership.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT /api/homes/{id} Postman: Put and Body - Raw data 
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }

            if (customer.MembershipTypeId == 2 || customer.MembershipTypeId == 3 || customer.MembershipTypeId == 4)
            {
                var today = DateTime.Today;

                if (!customer.Birthday.HasValue)
                {
                    return BadRequest("Birthday is required.");
                }
                else
                {
                    var age = today.Year - customer.Birthday.Value.Year;
                    if (age < 18)
                    {
                        return BadRequest("Customer should be at least 18 years old to go on a membership.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            customerInDb.Name = customer.Name;
            customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            customerInDb.MembershipType = customer.MembershipType;
            customerInDb.Birthday = customer.Birthday;

            _context.SaveChanges();

            return Ok(customerInDb);
        }

        // DELETE /api/homes/{id} Postman: Delete and Body - None 
        [HttpDelete("{id}")]
        public ActionResult<Customer> DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
            return customerInDb;
        }


        public MembershipType GetMembershipTypeById(int id)
        {
            var result = _context.MembershipTypes.FirstOrDefault(t => t.Id == id);
            return result;
        }
    }
}
