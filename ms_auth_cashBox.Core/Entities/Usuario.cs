using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public bool isEnable { get; set; }

        public ICollection<UserRole> userRoles { get; set; }
        public ICollection<TerminalSession> terminalSessions { get; set; }
    }
}
