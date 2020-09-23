using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesRL.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesRL.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; } // u view-u necemo koristit edit funkcionalnosti za Genre objekte, nego samo iterirat kroz listu pa zato koristimo IEnumerable

        // 1. nacin: umjesto polja Movie (koji predstavlja model), mozemo sve parametre modela Movie drzati u ovom view modelu i koristi prema potrebi
        /*
        public Movie Movie { get; set; }
        
        public string Title
        {
            get
            {
                if(Movie != null && Movie.Id != 0)
                {
                    return "Edit Movie";
                }

                return "New Movie";
            }
        }*/

        // 2. nacin (novodefinisana polja za Movie):
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte? GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        [Required]
        public byte? NumberInStock { get; set; }

        // staro polje Title
        public string Title
        {
            get
            {
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        // konstruktori
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}