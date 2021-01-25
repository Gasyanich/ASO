using ASO.API.Common.Constants;
using ASO.Models.Constants;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = AuthorizeConstants.StudentsControllerRoles)]
    public class StudentsController : BaseUsersController
    {
        public StudentsController(IUsersService usersService) : base(usersService)
        {
        }

        protected override string Role => RolesConstants.Student;
    }
}