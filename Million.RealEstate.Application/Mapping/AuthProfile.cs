using AutoMapper;
using Million.RealEstate.Application.DTOs;
using Million.RealEstate.Domain.Models.Auth;

namespace Million.RealEstate.Application.Mapping
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginResponse, LoginResponseDto>();
        }
    }
}