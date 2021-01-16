using ASO.DataAccess.Entities;
using ASO.Models.DTO;
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