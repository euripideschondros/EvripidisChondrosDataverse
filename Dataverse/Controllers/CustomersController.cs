using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dataverse.Models;

namespace Dataverse.Controllers
{
    public class CustomersController : Controller
    {

        private CustomerContext _context;

        public CustomersController()
        {
            _context = new CustomerContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ViewResult Index(string search)
        {
            var searchCustomer = from c in _context.Customers select c;

            if(!String.IsNullOrEmpty(search))
            {
                searchCustomer = searchCustomer.Where(s => s.Lname!.Contains(search));
            }

            var customers = _context.Customers.ToList();
            //return View(customers);
            return View(searchCustomer.ToList());
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return Content("There is no customer, try again");

            return View(customer);
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            ModelState.Remove("Id");
            if(!ModelState.IsValid)
            {
                return View("New", customer);
            }

            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Lname = customer.Lname;
                customerInDb.Fname = customer.Fname;
                customerInDb.Tel = customer.Tel;
                customerInDb.Address = customer.Address;
                customerInDb.Email = customer.Email;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return Content("There is no customer, try again");

            return View("New", customer);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return Content("What happened");
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}
