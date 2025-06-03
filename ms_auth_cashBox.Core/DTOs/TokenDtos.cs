using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ms_auth_cashBox.Core.DTOs
{
    public class TokenRespDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public class LoginRequestDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
