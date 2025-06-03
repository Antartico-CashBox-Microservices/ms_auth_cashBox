using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.Entities
{
    public class CashBox
    {
        public Guid Id { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }

        //FK, required relationship
        public Guid TerminalId { get; set; }
        //Navigation property
        public Terminal Terminal { get; set; }
    }
}
