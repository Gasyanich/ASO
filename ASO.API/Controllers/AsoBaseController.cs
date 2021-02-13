using ASO.Models.DTO.Results;
using Microsoft.AspNetCore.Mvc;

namespace ASO.API.Controllers
{
    public abstract class AsoBaseController : ControllerBase
    {
        protected virtual IActionResult BadResultRequest(BaseResultDto resultDto)
        {
            return BadRequestError(resultDto.ErrorMessage);
        }

        protected IActionResult BadRequestWrongId(long id, string entityName = "Пользователь")
        {
            return BadRequestError($"{entityName} с id {id} не найден");
        }

        protected IActionResult BadRequestError(string error)
        {
            return BadRequest(new {Error = error});
        }
    }
}