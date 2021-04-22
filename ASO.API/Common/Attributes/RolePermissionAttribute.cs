using System;
using ASO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace ASO.API.Common.Attributes
{
    /// <summary>
    ///     Хитрая магия - проверяем, может ли текущий пользователь манипулировать пользователями с ролью из роута
    /// </summary>
    public class RolePermissionAttribute : Attribute, IActionFilter
    {
        public RolePermissionAttribute(bool isMultipleRoles = false)
        {
            IsMultipleRoles = isMultipleRoles;
        }

        /// <summary>
        /// Придет ли в запросе  несколько ролей
        /// </summary>
        public bool IsMultipleRoles { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var roleService = context.HttpContext.RequestServices.GetService<IRoleService>();

            var roleActionArgKey = IsMultipleRoles ? "roles" : "role";
            var requestedRoles = (string) context.ActionArguments[roleActionArgKey];

            var checkPermissionsResult = roleService.CheckUserHasPermissionsToRoles(requestedRoles);

            if (!checkPermissionsResult)
                context.Result = new ForbidResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}