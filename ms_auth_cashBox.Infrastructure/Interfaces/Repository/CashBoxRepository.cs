using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ms_auth_cashBox.Core.Entities;
using ms_auth_cashBox.Core.Interfaces.Repository;
using ms_auth_cashBox.Infrastructure.DataBaseIttion;

namespace ms_auth_cashBox.Infrastructure.Interfaces.Repository
{
    public class CashBoxRepository : GenericRepository<CashBox>, ICashBoxRepository
    {
        public CashBoxRepository(ApplicationDbContext dBContext, ILogger<GenericRepository<CashBox>> logger) : base(dBContext, logger)
        {
        }

        public Task<bool> AddCashBox(CashBox cashBox)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CashBox>> GetAllCashBox()
        {
            //throw new NotImplementedException();
            List<CashBox> result = new List<CashBox>();
            result = await _dBcontext.Set<CashBox>().ToListAsync();
            return result;
        }

        public async Task<CashBox> GetCashBoxByIp(string Ip)
        {
            try
            {
                return await _dBcontext.cashBox.AsNoTracking().FirstOrDefaultAsync(c => c.Ip == Ip);
                //return await _dBContext.Set<CashBox>().FirstOrDefaultAsync(c => c.Ip == Ip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                $"Error getting CashBox by Ip:{Ip}: {ex.Message}");
                throw new Exception($"Error: GetCashBoxById()");
            }
        }

        public async Task<List<CashBox>> GetCashBoxByTerminalNro(string nroTerminal)
        {
            try
            {
                return await _dBcontext.terminal.Where(T => T.NroTerminal == nroTerminal)
                                    .Include(T => T.CashBox)
                                    .Select(T => T.CashBox)
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                $"Error getting CashBox by TerminalNro:{nroTerminal}: {ex.Message}");
                throw new Exception($"Error: GetCashBoxByTerminalNro()");
            }
        }
    }
}
