

using AutoMapper;
using MT.PersonService.API.Dtos;
using MT.PersonService.Core.Entity;

namespace MT.PersonService.API.Mapping
{
    public class MapProfile : Profile
    {

        public MapProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
            CreateMap<Person, PersonReadContactInfoDto>().ReverseMap();
        }
    }
}
