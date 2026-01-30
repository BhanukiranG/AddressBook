using AddressBook.Application.DTOs.Contact;
using AddressBook.Domain.Entities;
using AutoMapper;

namespace AddressBook.Application.Mapping
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<CreateContactDto, Contacts>();
            CreateMap<UpdateContactDto, Contacts>();
            CreateMap<Contacts, ContactResponseDto>();
            CreateMap<UpdateContactDto, Contacts>()
                .ForAllMembers(opt =>
                    opt.Condition((_, _, srcMember) => srcMember != null));
        }
    }
}