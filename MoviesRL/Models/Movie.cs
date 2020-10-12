using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MoviesRL.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required] // sada kolona Name u bazi nije nullable (a po defaultu jeste) 
        [StringLength(255)] // sada kolona Name u bazi nije tipa varchar(MAX) (a po defaultu jeste) 
        public string Name { get; set; }

        // [Required] // ako ovdje stavimo required, baci se Entity Validation izuzetak pa zato premjestimo 'Required' na GenreId
        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }
    }
}