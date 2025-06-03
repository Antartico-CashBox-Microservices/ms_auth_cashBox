using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.Entities
{
    public class TerminalSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TerminalId { get; set; }
        public Terminal Terminal { get; set; } = default!;

        public Guid UserId { get; set; }
        public Usuario User { get; set; } = default!;

        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
        public DateTime? LogoutTime { get; set; }

        public bool IsActive => LogoutTime == null;
    }
}
