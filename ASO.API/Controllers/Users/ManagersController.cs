using ASO.API.Common.Constants;
using ASO.Models.Constants;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = AuthorizeConstants.ManagersControllerRoles)]
    public class ManagersController : BaseUsersController
    {
        public ManagersController(IUsersService usersService, IRegisterService registerService) : base(usersService,
            registerService)
        {
        }

        protected override long RoleId => RolesConstants.ManagerId;
    }
}