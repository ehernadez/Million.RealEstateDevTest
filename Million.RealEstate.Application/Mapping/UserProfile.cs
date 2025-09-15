using AutoMapper;
using Million.RealEstate.Application.DTOs.Users;
using Million.RealEstate.Domain.Entities;

namespace Million.RealEstate.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}