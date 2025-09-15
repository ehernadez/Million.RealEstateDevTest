using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Entities;

namespace Million.RealEstate.Application.Mapping
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            // Property mappings
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner));

            CreateMap<CreatePropertyDto, Property>();

            CreateMap<UpdatePropertyDto, Property>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Owner mapping
            CreateMap<Owner, OwnerDto>();

            // PropertyImage mapping
            CreateMap<PropertyImage, PropertyImageDto>();

            // Property paged response mapping
            CreateMap<(IEnumerable<Property> Items, int TotalCount), PropertyPagedResponseDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());

            // PropertyTrace mappings
            CreateMap<PropertyTrace, PropertyTraceDto>();
            
            CreateMap<CreatePropertyTraceDto, PropertyTrace>()
                .ForMember(dest => dest.IdPropertyTrace, opt => opt.Ignore())
                .ForMember(dest => dest.IdProperty, opt => opt.Ignore())
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore());

            CreateMap<UpdatePropertyTraceDto, PropertyTrace>()
                .ForMember(dest => dest.IdPropertyTrace, opt => opt.Ignore())
                .ForMember(dest => dest.IdProperty, opt => opt.Ignore())
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore());
        }
    }
}
