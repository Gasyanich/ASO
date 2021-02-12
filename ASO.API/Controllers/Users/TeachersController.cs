using ASO.API.Common.Constants;
using ASO.Models.Constants;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = AuthorizeConstants.TeachersControllerRoles)]
    public class TeachersController : BaseUsersController
    {
        public TeachersController(IUsersService usersService, IRegisterService registerService) : base(usersService,
            registerService)
        {
        }

        protected override long RoleId => RolesConstants.TeacherId;
    }
}