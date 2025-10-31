using AutoMapper;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Profiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<PhoneDto, Phone>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.phone_id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.user_id))
                .ForMember(dest => dest.PhoneType, opt => opt.MapFrom(src => src.phone_type))
                .ForMember(dest => dest.PhoneValue, opt => opt.MapFrom(src => src.phone_value))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ReverseMap();
        }
    }
}
