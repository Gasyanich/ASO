using ASO.Models.Constants;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : BaseUsersController
    {
        public TeachersController(IUsersService usersService) : base(usersService)
        {
        }

        protected override string Role => RolesConstants.Teacher;
    }
}