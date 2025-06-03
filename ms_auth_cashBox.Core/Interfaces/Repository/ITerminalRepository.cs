using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Core.Interfaces.Repository
{
    public interface ITerminalRepository : IGenericRepository<Terminal>
    {
        Task<Terminal> GetTerminalByNro(string nroTerminal);
        Task<bool> IsTerminalEnable(string nroTerminal);
    }
}
//ya hay una sesión activa en esa Terminal

//    Luego de verificar, si se permite el login, se crea la nueva sesión

//    ✅ ¿Qué sigue?
//Podemos:

//Validar el modelo ([Required] en los DTOs)

//Agregar endpoints para registro de usuarios o logout

//Proteger endpoints con [Authorize(Roles = "Supervisor")]

//¿Avanzamos con el endpoint de registro, logout, o configuración JWT en Program.cs