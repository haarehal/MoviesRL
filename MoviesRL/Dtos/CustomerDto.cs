using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoviesRL.Models;

namespace MoviesRL.Dtos
{
    public class CustomerDto
    {
        // Polja iz modela Customer

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //public MembershipType MembershipType { get; set; } // ovo nam ne treba jer je ovo domain klasa i ovo polje kreira dependency od DTO prema domain modelu
        // Ovdje koristimo samo primitivne tipove (string, int, byte, ...) ili custom Dtos
        // Ako zelimo vratiti high-archical strukturu, kreiramo zasebnu klasu MembershipTypeDto
        // Na ovaj nacin je Dto potpuno razdvojen od domain objekta (inace sa MembershipType poljem bi ovaj Dto bio povezan domain objektom)
    
        public MembershipTypeDto MembershipType { get; set; } // ovako vracamo high-archical podatke iz APIja

        public byte MembershipTypeId { get; set; }

        //[Min18YearsIfAMember] - sada ovo izbacujemo jer ce se baciti izuzetak zbog toga sto Min18YearsIfAMember primjenjujemo na Customer model a ne na CustomerDto
        public DateTime? Birthdate { get; set; }

    }
}