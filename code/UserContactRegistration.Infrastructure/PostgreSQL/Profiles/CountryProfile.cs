using AutoMapper;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDto, Country>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.country_id))
                .ForMember(dest => dest.IsoCode, opt => opt.MapFrom(src => src.iso_code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ReverseMap();
        }
    }
}
