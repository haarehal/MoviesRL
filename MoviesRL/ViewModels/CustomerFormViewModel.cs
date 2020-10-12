using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesRL.Models;

namespace MoviesRL.ViewModels
{
    public class CustomerFormViewModel
    {
        // Sa view modelom enkapsuliramo podatke koji su potrebni za view

        //public List<MembershipType> MembershipTypes { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; } // u view-u necemo koristit edit funkcionalnosti za MembershipType objekte, nego samo iterirat kroz listu pa zato koristimo IEnumerable
        public Customer Customer { get; set; }
        public string Title
        {
            get
            {
                if(Customer == null || Customer.Id == 0)
                {
                    return "New Customer";
                }

                return "Edit Customer";
            }
        }
    }
}