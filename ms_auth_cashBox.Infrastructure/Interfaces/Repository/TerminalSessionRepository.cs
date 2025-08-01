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
    public class TerminalSessionRepository : GenericRepository<TerminalSession>, ITerminalSessionRepository
    {
        public TerminalSessionRepository(ApplicationDbContext dbContext, ILogger<GenericRepository<TerminalSession>> logger) : base(dbContext, logger)
        {
        }

        public async Task<bool> AddSessionAsync(TerminalSession terminalSession)
        {
            try
            {
                var result = await _dBcontext.terminalSession.AddAsync(terminalSession);
                if (result != null)
                {//return ter?.Enabled ?? false;
                    _dBcontext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
                //return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AddAsync(): Error:{ex.Message}");
                throw new Exception("Exception in AddSessionAsync()");
            }
        }

        public async Task<TerminalSession> IsSessionActiveAsync(string NroTerminal)
        {
            try
            {
                var result = await _dBcontext.terminalSession
                //.Include(T => T.Terminal.NroTerminal == NroTerminal)
                
                .Where(T => T.LogoutTime == null && T.Terminal.NroTerminal == NroTerminal)
                .Include(T => T.Terminal)
                .FirstOrDefaultAsync();
                //AsNoTracking().FirstOrDefaultAsync<>
                //throw new NotImplementedException();
                return result;
            }catch(Exception ex){
                _logger.LogError(ex, $"IsSessionActiveAsync(): Error:{ex.Message}");
                throw new Exception("Exception in IsSessionActiveAsync()");
            }
        }

        public Task<bool> UpdateSessionAsync(TerminalSession terminalSession)
        {
            throw new NotImplementedException();
        }
    }
}
