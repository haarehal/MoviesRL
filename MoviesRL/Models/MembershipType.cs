using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesRL.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        // eksplicitno definisemo specificne Membership tipove koje koristimo umjesto MAGIC brojeva (u Min18YearsIfAMember klasi) da bi odrzali maintainability koda
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}