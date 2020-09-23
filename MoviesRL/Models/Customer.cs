using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MoviesRL.Models
{
    public class Customer
    {
        public int Id { get; set; }

        // Data annotations sluze po defaultu za server-side validaciju, ali eksplicitno mozemo naznaciti da bude i za client-side

        [Required(ErrorMessage = "Please enter customer's name")] // sada kolona Name u bazi nije nullable (a po defaultu jeste) 
        [StringLength(255)] // sada kolona Name u bazi nije tipa varchar(MAX) (a po defaultu jeste) 
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } // ovo polje je automatski 'required' zbog tipa byte

        [Display(Name = "Date of Birth")] // Ukoliko zelimo da se na formi /customers/new prikaze naziv 'Date of Birth' umjesto Birthday
        [Min18YearsIfAMember] // custom annotation
        public DateTime? Birthdate { get; set; } // znak ? oznacava da moze biti i nullable

        // Imamo i druge data annotations za validaciju: 
        // [Range(1,10)], [Compare("OtherProperty")], [Phone], [EmailAddress], [Url], [RegularExpression("...")]
    }
}