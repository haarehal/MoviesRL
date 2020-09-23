using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MoviesRL.Models;
using System.Data.Entity;
using MoviesRL.ViewModels;
using System.Data.Entity.Validation;
using System;

namespace MoviesRL.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        /*private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer {Id = 1, Name = "John Smith"},
                new Customer {Id = 2, Name = "Mary Williams"}
            };
        }*/
        
        public ViewResult Index()
        {
            //var customers = GetCustomers();
            //var customers = _context.Customers.ToList();
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            //var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null) return HttpNotFound();
            return View(customer);
        }
        
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), // ovo inace ne bi morali pisati (po defaultu bi customer bio null), ali je potrebno da bi se Id novog customera postavio na 0 i tako izbjegnemo error poruku za customer ID u validation summary-u
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id) // nova akcija umjesto Details akcije
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // automatska validacija za skriveno polje AntiForgeryToken u formi sa svrhom sprjecavanja hakerskih napada CSRF (Cross-site Request Forgery)
        public ActionResult Save(Customer customer) // Model binding - MVC binda ovaj model (parametar) sa request podatkom POST metode; umjesto CustomerFormViewModel vievModel mozemo koristiti Customer customer
        {
            //System.Diagnostics.Debug.WriteLine(customer.Id + " | " + customer.Name + " | " + customer.IsSubscribedToNewsletter + " | " + customer.Birthdate + " | " + customer.MembershipType.Id + " | " + customer.MembershipType.Name);
            
            if (!ModelState.IsValid) // server-side validacija
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            //System.Diagnostics.Debug.WriteLine("OK!");

            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // Prvi nacin updateovanja u bazi
                //TryUpdateModel(customerInDb);

                // Drugi nacin updateovanja u bazi
                // Mapper.Map(customer, customerInDb); - automatski inicijalizira sva polja, ne moramo pojedinacno svako polje da postavljamo
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");

            /*
            // Try-catch za debugiranje
            try
            {
                // ...
            }
            catch (DbEntityValidationException e) // Hvatanje nastalog izuzetka za potrebe debugiranja
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw e;
            }*/
        }
    }
}