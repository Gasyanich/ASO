using ASO.Models.DTO.Results;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    public abstract class AsoBaseController : ControllerBase
    {
        protected virtual IActionResult BadResultRequest(BaseResultDto resultDto) =>
            BadRequest(new {Error = resultDto.ErrorMessage});

        protected IActionResult BadRequestWrongId(long id, string entityName = "Пользователь") =>
            BadRequest(new {Error = $"{entityName} с id {id} не найден"});
    }
}