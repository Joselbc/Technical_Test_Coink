using AutoMapper;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Profiles
{
    public class MunicipalityProfile : Profile
    {
        public MunicipalityProfile()
        {
            CreateMap<MunicipalityDto, Municipality>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.municipality_id))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.department_id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ReverseMap();
        }
    }
}
