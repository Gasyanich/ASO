using ASO.DataAccess.Entities;
using ASO.Models.DTO.Users;
using AutoMapper;

namespace ASO.Models.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<UserRole, RoleDto>().ReverseMap();
        }
    }
}