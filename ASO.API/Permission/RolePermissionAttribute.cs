using System;
using System.Security.Claims;
using ASO.Services.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASO.API.Permission
{
    /// <summary>
    ///     Хитрая магия - проверяем, может ли текущий пользователь манипулировать пользователями с ролью из роута
    /// </summary>
    public class RolePermissionAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var currentUserRole = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            var requestedRole = (string) context.ActionArguments["role"];

            if (!RolePermissionHelper.HasPermissionToRole(currentUserRole, requestedRole))
                context.Result = new BadRequestObjectResult(new RolePermissionResult(requestedRole));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

    public class RolePermissionResult
    {
        public RolePermissionResult(string requestedRole)
        {
            Error = $"Не хватает прав для осуществления операций с пользователями группы {requestedRole}";
        }

        public string Error { get; set; }
    }
}