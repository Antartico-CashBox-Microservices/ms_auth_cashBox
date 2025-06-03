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
    public class SucursalRepository : GenericRepository<Sucursal>, ISucursalRepository
    {
        public SucursalRepository(ApplicationDbContext dBContext, ILogger logger) : base(dBContext, logger)
        {
        }

        public async Task<List<Terminal>> GetTerminalesByNroSucursal(string NroSucursal)
        {
            try
            {
                List<Terminal> terminals = new List<Terminal>();

                terminals = await _dBcontext.terminal
                    .Include(T => T.Sucursal)
                    .Where(T => T.Sucursal.NroSucursal == NroSucursal)
                    .ToListAsync();

                //terminals = await _dBContext.sucursal.Where(S => S.NroSucursal == NroSucursal)
                //                   .Include(S => S.Terminals)
                //                   .Select(S => S.Terminals);

                //.ToListAsync();
                return terminals;

                //_dBContext.sucursal.Include(S => S.Terminals).Where(S => S.NroSucursal == NroSucursal).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting Terminales by NroSucursal:{NroSucursal}: {ex.Message}");
                throw new Exception("Error: GetTerminalesByNroSucursal()");
            }
        }

        public async Task<Sucursal> GetSucursalByNroTerminal(string nrTerminal)
        {
            try
            {
                var suc = await _dBcontext.terminal.Where(T => T.NroTerminal == nrTerminal)
                    .Include(T => T.Sucursal)
                    .Select(T => T.Sucursal)
                    .FirstOrDefaultAsync();

                return suc;

                //_dBContext.sucursal.Include(S => S.Terminals)
                //    ..Where(S => S..Terminals).Where(S => S.Terminals.).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting Sucursal by Terminal:{nrTerminal}: {ex.Message}");
                throw new Exception("Error: GetSucursalByNroTerminal()");
            }
        }

        public async Task<Sucursal> GetSucursalDetails(string NroSucursal)
        {

            try
            {
                return await _dBcontext.sucursal
                .Include(S => S.Terminals)
                .FirstOrDefaultAsync(s => s.NroSucursal == NroSucursal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting Sucursal by:{NroSucursal}: {ex.Message}");
                throw new Exception("Error: GetSucursalDetails()");
            }
        }
    }
}
