using AutoMapper;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentDto, Department>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.department_id))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.country_id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ReverseMap();
        }
    }
}
