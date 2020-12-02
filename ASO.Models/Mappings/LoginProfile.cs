using ASO.Models.DTO;
using ASO.Models.Requests;
using ASO.Models.Responses;
using AutoMapper;

namespace ASO.Models.Mappings
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginRequest, UserLoginDto>();
            CreateMap<UserLoginDto, LoginResponse>();
        }
    }
}