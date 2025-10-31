using AutoMapper;
using UserContactRegistration.Domain.Entities;
using UserContactRegistration.Infrastructure.PostgreSQL.Entities;

namespace UserContactRegistration.Infrastructure.PostgreSQL.Profiles
{
    public class DocumentTypeProfile : Profile
    {
        public DocumentTypeProfile()
        {
            CreateMap<DocumentTypeDto, DocumentType>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.document_type_id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.code))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.created_at))
                .ReverseMap();
        }
    }
}
