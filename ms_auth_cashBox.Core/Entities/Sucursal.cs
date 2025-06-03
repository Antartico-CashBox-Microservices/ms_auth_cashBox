using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.Entities
{
    public class Sucursal
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string NroSucursal { get; set; }

        //Navigation propierties
        public ICollection<Terminal> Terminals { get; set; } = new List<Terminal>();
        //public List<Terminal> Terminals { get; set; } = new List<Terminal>();
    }
}
