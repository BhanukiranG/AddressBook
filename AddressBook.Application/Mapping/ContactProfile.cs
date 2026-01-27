using AutoMapper;
using AddressBook.Application.DTOs.Contact;
using AddressBook.Domain.Entities;

namespace AddressBook.Application.Mapping
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<CreateContactDto, Contact>();
            CreateMap<UpdateContactDto, Contact>();
            CreateMap<Contact, ContactResponseDto>();
        }
    }
}