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
    public class TerminalRepository : GenericRepository<Terminal>, ITerminalRepository
    {
        public TerminalRepository(ApplicationDbContext dBContext, ILogger logger) : base(dBContext, logger)
        {
        }

        public async Task<Terminal> GetTerminalByNro(string nroTerminal)
        {
            try
            {
                return await _dBcontext.terminal.AsNoTracking().FirstOrDefaultAsync(T => T.NroTerminal == nroTerminal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting Terminal by:{nroTerminal}: {ex.Message}");
                throw new Exception("Error: GetTerminalByNro()");
            }
        }

        public async Task<bool> IsTerminalEnable(string nroTerminal)
        {
            try
            {
                var ter = await _dBcontext.terminal.AsNoTracking().FirstOrDefaultAsync(T => T.NroTerminal == nroTerminal);
                return ter?.Enabled ?? false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting if Terminal is enable:{nroTerminal}: {ex.Message}");
                throw new Exception("Error: GetTerminalByNro()");
            }

        }
    }
}
