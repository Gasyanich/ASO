using System.Threading.Tasks;
using ASO.DataAccess.Entities;
using ASO.Models.DTO;
using ASO.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using NETCore.MailKit.Core;

namespace ASO.Services
{
    public class UsersService : IUsersService
    {
        private readonly ActionContext _actionContext;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;
        private readonly UserManager<User> _userManager;

        public UsersService(
            IMapper mapper,
            IEmailService emailService,
            UserManager<User> userManager,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _actionContext = actionContextAccessor.ActionContext;

            _mapper = mapper;
            _emailService = emailService;
            _urlHelper = urlHelperFactory.GetUrlHelper(_actionContext);
            _userManager = userManager;
        }
        public async Task<IdentityResult> RegisterUserAsync(UserRegisterDto userRegister)
        {
            return await CreateUserAsync(userRegister);
        }

        private async Task<IdentityResult> CreateUserAsync(UserRegisterDto userRegister)
        {
            var user = _mapper.Map<User>(userRegister);
            var password = "Simple123";

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                await SendConfirmEmailAsync(user.Id, emailConfirmationToken, user.Email, password);
            }

            await _userManager.AddToRoleAsync(user, userRegister.Role);

            return result;
        }
        
        private async Task SendConfirmEmailAsync(long userId, string token, string email, string userPassword)
        {
            var request = _actionContext.HttpContext.Request;
            var confirmationLink = _urlHelper.Action("ConfirmEmail", "Account",
                new {usid = userId, tkn = token}, request.Scheme, request.Host.ToString());

            var message = GetEmailBodyMessage(userPassword, confirmationLink);

            await _emailService.SendAsync(email, "Подтверждение регистрации на сайте Автошкола онлайн", message);
        }

        private string GetEmailBodyMessage(string userPassword, string confirmationLink)
        {
            return "Здравствуйте!" +
                   "\n\nВы зарегестрировались на сайте Автошкола онлайн." +
                   $"\n\nВаш пароль:{userPassword}" +
                   $"\nДля подтверждения регистрации пройдите по следующей ссылке - {confirmationLink}" +
                   "\nЭто письмо отправлено автоматически, не отвечайте на него. " +
                   "Если Вы считаете, что письмо пришло к вам по ошибке, просто удалите его.";
        }
       
    }
}