using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesRL.Dtos;
using MoviesRL.Models;

// Umjesto da recimo preko rute (tj. akcije iz kontrolera) vracamo klijentu View kojeg dalje parsira Razor View mehanizam, te putem kojeg vracamo HTML (tabela sa kupcima), 
// bolje je kreirati API koji vraca samo listu kupaca (GET zahtjev) kao raw data te prepustamo da se HTML kreira na klijentskoj strani umjesto na serverskoj strani
// Benefiti ovog pristupa: manje resursa se trosi na serveru, manji bandwith, podrzava razlicite klijente (web app, mobile app, tablet, ...)

namespace MoviesRL.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // API ne bi nikad trebao primati/vracati domain model objekte (kao sto je Customer) - to bi bio sigurnosni propust, te zbog toga sto se domain model moze 
        // cesto mijenjati kako se aplikacija razvija - takve promjene mogu uticati na postojece klijente koji ovise od domain model objekta
        // Zato trebamo koristiti drugi objekat: DTO (Data Transfer Object) - vrsi transfer objekata od klijenta do servera i obrnuto
        // Na taj nacin sprjecavamo bugove u API-u kad budemo vrsili refaktoring domain modela
        // Auto mapper koristimo da povezemo domain model objekat sa DTO objektom

        /*
        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/1
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return customer;
        }

        // POST /api/customers
        [HttpPost] // da smo koristili public Customer PostCustomer(Customer customer), tada [HttpPost] anotacija nije potrebna, ali ovo nije preporuceni nacin
        public Customer CreateCustomer(Customer customer) // prema konvenciji, kad kreiramo novi resurs, vracamo taj resurs klijentu (taj resurs ce vrlo vjerovatno imati id koji je server generisao)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer) // povratni tip moze biti ili Customer ili void, radi i jedan i drugi nacin
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;


            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();
        }
        */

        // Auto Mapper i Dto

        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        /*
        // GET /api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Customer, CustomerDto>(customer);
        }
        */
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        /*
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto); // customer ne saljemo kao drugi parametar jer to nije vec postojeci objekat, nego novi koji se kreira

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return customerDto;
        }
        */
        // Prema RESTful konvenciji, kada kreiramo POST zahtjev, status kod (za uspjesan zahtjev) treba biti 201 za 'created' (a ne defaultni 200); zbog toga koristimo IHttpActionResult povratni tip i helper metode
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(); // helper metoda koja vraca BadRequest rezultat - to je klasa iz inerfejsa IHttpActionResult
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto); // customer ne saljemo kao drugi parametar jer to nije vec postojeci objekat, nego novi koji se kreira

            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto); // helper metoda koja vraca klijentu URI tek kreiranog resursa ('/api/customers/id') i objekat koji je tek kreiran
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto) // povratni tip moze biti ili Customer ili void; radi i jedan i drugi nacin
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb); // customerInDb saljemo kao drugi argument jer je to vec postojeci objekat (koji je vezan za DBContext, te na ovaj nacin omogucavamo da DBContext direktno prati promjene nad objektom)
            // moze se pisat i Mapper.Map(customerDto, customerInDb) jer kompajler iz objekata zakljuci koji su source i target tipovi

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Customers.Remove(customerInDb);

            _context.SaveChanges();
        }
    }
}
