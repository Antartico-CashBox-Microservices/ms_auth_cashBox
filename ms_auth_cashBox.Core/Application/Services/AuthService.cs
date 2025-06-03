using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.DTOs;
using ms_auth_cashBox.Core.Interfaces.Services;

namespace ms_auth_cashBox.Core.Application.Services
{
    public class AuthService
    {
        private readonly IHttpService _httpService;

        public AuthService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        async Task<TokenRespDto> AuthenticateAsync()
        {
            var url = ConfigurationManager.AppSettings["ApiUrl"];
            var email = ConfigurationManager.AppSettings["AuthEmail"];
            var password = ConfigurationManager.AppSettings["AuthPassword"];

            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw new ConfigurationErrorsException("Faltan claves en App.config: ApiUrl, AuthEmail o AuthPassword.");

            var queryString = $"email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}";

            return await _httpService.PostAsync<TokenRespDto>(url, queryString);
        }
    }
}
