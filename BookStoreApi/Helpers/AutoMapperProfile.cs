using AutoMapper;
using BookStoreApi.DTOs;
using BookStoreApi.Entities;
using BookStoreApi.ViewModels;

namespace BookStoreApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Book, Book>(); // ou Book -> BookViewModel se tiver ViewModel
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Book, BookViewModel>();
            CreateMap<Author, AuthorViewModel>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Author, AuthorDto>();
        }
    }
}
