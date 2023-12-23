using Address_Book.Core.Entities;
using Address_book_BE.Dtos;
using AutoMapper;
using System.Runtime.InteropServices;

namespace Address_book_BE.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Entry, EntryToReturnDto>().ReverseMap();

        }
    }
}

