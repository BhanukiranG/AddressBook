using AutoMapper;
using AddressBook.Application.DTOs.Contact;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Mapping
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<CreateContactDto, Contacts>();
            CreateMap<UpdateContactDto, Contacts>();
            CreateMap<Contacts, ContactResponseDto>();
        }
    }
}