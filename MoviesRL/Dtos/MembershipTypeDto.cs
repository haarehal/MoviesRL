using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesRL.Dtos
{
    public class MembershipTypeDto
    {
        // Ne moramo dodavati sva polja iz MembershypType modela
        // Ako klijent hoce da zna detalje za specificni MembershipType, moze koristiti Id da bi poslao zahtjev na potencijalni endpoint za MembershipTypes  
        public byte Id { get; set; }

        public string Name { get; set; }
    }
}