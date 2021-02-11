using ASO.Models.DTO.Results;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    public abstract class AsoBaseController : ControllerBase
    {
        protected IActionResult BadResultRequest(BaseResultDto resultDto) =>
            BadRequest(new {Data = new {Error = resultDto.ErrorMessage}});
    }
}