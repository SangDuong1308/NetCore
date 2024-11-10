using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using src.DTOs.Book;
using src.Models;

namespace src.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            BookMap();
        }
        private void BookMap()
        {
            CreateMap<Book, BookResponseDto>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<UpdateBookDto, Book>().ReverseMap();
        }
    }
}