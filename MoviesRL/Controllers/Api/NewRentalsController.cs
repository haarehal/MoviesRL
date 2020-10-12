using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesRL.Dtos;
using MoviesRL.Models;

namespace MoviesRL.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        // Dva pristupa kod rjesavanja edge caseova: Deffensive i Optimistic (oba su ispravna; stvar licnog odabira)

        // Defensive approach 
        // dobar pristup ukoliko koristimo public API kojeg koriste razlicite aplikacije i timovi
        // za 4 edge casea cemo imati i 4 ispitivanja (if bloka) - radimo dosta validacije i eksplicitno vracamo errore
        // medjutim, to stvara dosta zbrke u kodu za nekog ko ce se prvi put susresti sa citavim kodom

        // Optimistic approach
        // u nasem slucaju koristimo privatni API (samo za internu upotrebu) koji ce se pozivati sa frontenda

        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRentalDto)
        {
            // Defensive approach - trenutna implementacija vec obuhvata ovaj specijalni slucaj 
            // if(newRentalDto.MovieIds.Count == 0) return BadRequest("No movie IDs have been given.");

            // Defensive approach - trenutna implementacija vec obuhvata ovaj specijalni slucaj 
            // var customer = _context.Customers.SingleOrDefault(c => c.Id == newRentalDto.CustomerId);
            // if (customer == null) return BadRequest("Customer ID is not valid!");

            var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId); // sa Single (umjesto SingleOrDefault) ocekujemo da ce ID biti validan

            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id)).ToList(); // SELECT * FROM Movies WHERE Id IN (1, 2, 3)

            // Defensive approach - trenutna implementacija vec obuhvata ovaj specijalni slucaj 
            // if(movies.Count != newRentalDto.MovieIds.Count) return BadRequest("One or more movie IDs are invalid!");

            foreach (var movie in movies)
            {
                // Deffensive approach - potrebna dodatna provjera zbog sigurnosti i odbrane od nezeljenih korisnika 
                if(movie.NumberAvailable == 0) return BadRequest("Movie is not available!");

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                movie.NumberAvailable--;

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
        
    }
}
