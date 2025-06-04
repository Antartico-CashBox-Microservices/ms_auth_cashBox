using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ms_auth_cashBox.Core.DTOs;
using ms_auth_cashBox.Core.Entities;
using ms_auth_cashBox.Core.Interfaces;
using ms_auth_cashBox.Infrastructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ms_auth_cashBox.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthController(IUnitOfWork unitOfWork, IConfiguration configuration) {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        // POST: api/<AuthController>
        [HttpPost]
        public async Task<ActionResult<TokenRespDto>> LoginAsync([FromBody] LoginRequestDto request)
        {

            Usuario usuario = await _unitOfWork.AuthRepository.GetByEmailAsync(request.Email);
            if (usuario == null) {
                return NotFound(request);
                //throw new UnauthorizedAccessException("Usuario no encontrado o deshabilitado.");
            }
            if(usuario.Password != request.Password)
            {
                //return Unauthorized(request);
                return Unauthorized(new { message = "Invalid credentials or terminal conflict" });
                //throw new UnauthorizedAccessException("Contraseña incorrecta.");
            }

            //Obtenemos Nro. Terminal por configuracion
            string TermNro = _configuration.GetSection("Terminal").GetValue<string>("Numero");
            //Verificar si terminal esta configurada en tablas          
            if (string.IsNullOrEmpty(TermNro))
            {
                return Unauthorized(new { message = $"Terminal Sin Numero." });
                throw new Exception("Terminal sin numero.");
            }
            //y si ista habilitada..
            bool isEnabled = await _unitOfWork.TerminalRepository.IsTerminalEnable(TermNro);
            if (!isEnabled)
            {
                return Unauthorized(new { message = $"Terminal:{TermNro} No Habilitada." });
            }
            
            //Si tiene una sesion activa ... Advertir y salir sino crear una terminaSesion y seguir
            var isActive = await _unitOfWork.TerminalSessionRepository.IsSessionActiveAsync(TermNro);
            if(isActive == null)
            {
                return Unauthorized(new { messge=$"Sesion activa con Terminal={TermNro}"});
            }

            var terminal = await _unitOfWork.TerminalRepository.GetTerminalByNro(TermNro);
            var session = new TerminalSession
            {
                Id = Guid.NewGuid(),
                TerminalId = terminal.Id,
                UserId = usuario.Id,
                LoginTime = DateTime.UtcNow,
                LogoutTime = null
            };
            
            var isOk = _unitOfWork.TerminalSessionRepository.AddAsync(session);

            // Construir claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new(ClaimTypes.Email,   usuario.Email),
            };
            // Agregar roles como claims
            var roles = usuario.userRoles.Select(ur => ur.Role.Name).ToList();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Clave secreta y token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Respuesta
            TokenRespDto results = new TokenRespDto
            {
                Token = jwt
            };

            return Ok(results);
            //return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
