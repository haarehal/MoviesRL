using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoviesRL.Dtos;
using MoviesRL.Models;

namespace MoviesRL.App_Start
{
    public class MappingProfile : Profile // Odredjuje kako se objekti razlicitih tipova mogu medjusobno povezati
    {
        // Auto Mapper je convention-based mapping tool
        public MappingProfile() // kad se pozove MappingProfile, Auto Mapper na osnovu naziva polja povezuje ta polja
        {
            Mapper.CreateMap<Customer, CustomerDto>(); // <source, target>
            Mapper.CreateMap<CustomerDto, Customer>();

            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MovieDto, Movie>();
        }
        
    }
}