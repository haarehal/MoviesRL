using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

// CUSTOM VALIDATION ZA POLJE BIRTHDATE KOD CUSTOMERA

namespace MoviesRL.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            // 0 i 1 predstavljaju MAGIC NUMBERS jer narusavaju maintainability aplikacije 
            // (mi znamo sta predstavlja 0 i 1 u ovom slucaju, ali ako neko drugi bude radio na ovom istom kodu - nece znati)
            /*
            if(customer.MembershipTypeId == 0 || customer.MembershipTypeId == 1)
            {
                return ValidationResult.Success;
            }*/

            // Koristimo drugi pristup
            if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}