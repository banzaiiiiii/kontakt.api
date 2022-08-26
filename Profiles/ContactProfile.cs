using AutoMapper;
using Kontakt.API.Dtos;
using Kontakt.API.Models;

namespace Kontakt.API.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            //source --> target

            CreateMap<Contact, ContactReadDto>();
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
            CreateMap<Contact, ContactUpdateDto>();
        }
    }
}
