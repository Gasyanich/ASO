using ASO.Models.DTO.Results;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    public abstract class AsoBaseController : ControllerBase
    {
        protected virtual IActionResult BadResultRequest(BaseResultDto resultDto) =>
            BadRequestError(resultDto.ErrorMessage);

        protected IActionResult BadRequestWrongId(long id, string entityName = "Пользователь") =>
            BadRequestError($"{entityName} с id {id} не найден");

        protected IActionResult BadRequestError(string error) =>
            BadRequest(new {Error = error});
    }
}