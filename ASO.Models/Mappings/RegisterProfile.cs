using ASO.DataAccess.Entities;
using ASO.Models.DTO;
using ASO.Models.Requests;
using AutoMapper;

namespace ASO.Models.Mappings
{
    public class RegisterProfile : Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterRequest, UserRegisterDto>()
                .ForMember(dto => dto.UserName,
                    opt => opt.MapFrom(request => request.Email));

            CreateMap<UserRegisterDto, User>();
        }
    }
}