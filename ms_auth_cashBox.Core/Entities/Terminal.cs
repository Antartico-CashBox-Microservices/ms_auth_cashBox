using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.Entities
{
    public class Terminal
    {
        public Guid Id { get; set; }
        public string NroTerminal { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        // Requerided foreing key propierty
        public Guid SucursalId { get; set; }
        public ICollection<TerminalSession> Sessions { get; set; }

        //Navigation propierties
        public Sucursal Sucursal { get; set; }
        public CashBox CashBox { get; set; }
    }
}
