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

            // PropertyImage mappings
            CreateMap<PropertyImage, PropertyImageDto>();
            CreateMap<AddPropertyImageDto, PropertyImage>()
                .ForMember(dest => dest.File, opt => opt.Ignore())
                .ForMember(dest => dest.IdProperty, opt => opt.Ignore());
            CreateMap<PropertyImage, AddPropertyImageResponseDto>()
                .ForMember(dest => dest.FileUrl, opt => opt.Ignore());

            // Property paged response mapping
            CreateMap<(IEnumerable<Property> Items, int TotalCount), PropertyPagedResponseDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
                .ForMember(dest => dest.PageNumber, opt => opt.Ignore())
                .ForMember(dest => dest.PageSize, opt => opt.Ignore());
        }
    }
}
