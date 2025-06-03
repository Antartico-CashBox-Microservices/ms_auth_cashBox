using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Core.Interfaces.Repository
{
    public interface ISucursalRepository : IGenericRepository<Sucursal>
    {
        Task<Sucursal> GetSucursalByNroTerminal(string nrTerminal);
        Task<List<Terminal>> GetTerminalesByNroSucursal(string NroSucursal);
        Task<Sucursal> GetSucursalDetails(string NroSucursal);
    }
}
