using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Core.Interfaces.Repository
{
    public interface ICashBoxRepository : IGenericRepository<CashBox>
    {
        Task<List<CashBox>> GetAllCashBox();
        Task<List<CashBox>> GetCashBoxByTerminalNro(string nroTerminal);
        Task<CashBox> GetCashBoxByIp(string Ip);
        Task<bool> AddCashBox(CashBox cashBox);

    }
}
