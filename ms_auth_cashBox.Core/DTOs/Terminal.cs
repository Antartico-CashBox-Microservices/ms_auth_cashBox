using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.DTOs
{
    public class TerminalDto
    {
        public Guid Id { get; set; }
        public string NroTerminal { get; set; }
        public bool Enabled { get; set; }

        // Requerided foreing key propierty
        public Guid SucursalId { get; set; }
    }
}
