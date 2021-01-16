using ASO.DataAccess.Entities;
using ASO.Models.DTO.Users;
using AutoMapper;

namespace ASO.Models.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // регистрация юзера -> доменный юзер
            CreateMap<UserRegisterDto, User>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(userDto => userDto.Email));

            // обновление инфы по юзеру -> доменный юзер
            CreateMap<UserUpdateDto, User>()
                .ForMember(dto => dto.UserName, opt => opt.MapFrom(userDto => userDto.Email));

            // доменный юзер <-> dto юзера
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}