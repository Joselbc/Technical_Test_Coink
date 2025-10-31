using AutoMapper;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.address_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.country_id))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.department_id))
                .ForMember(dest => dest.MunicipalityId, opt => opt.MapFrom(src => src.municipality_id))
                .ForMember(dest => dest.AddressValue, opt => opt.MapFrom(src => src.address))
                .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.complement))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.postal_code))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ReverseMap();
        }
    }
}
