using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Core.Interfaces.Repository
{
    public interface ITerminalSessionRepository : IGenericRepository<TerminalSession>
    {
        Task<TerminalSession> IsSessionActiveAsync(string NroTerminal);
        Task <bool>AddSessionAsync(TerminalSession terminalSession);
        Task<bool> UpdateSessionAsync(TerminalSession terminalSession);
    }
}
