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
    public class AuthRepository : GenericRepository<Usuario>, IAuthRepository
    {
        //private readonly DbContext _dbContext;

        public AuthRepository(ApplicationDbContext dbContext, ILogger<GenericRepository<Usuario>> logger) : base(dbContext, logger)
        {
            //_dbContext = dbContext;

        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            try
            {
                _logger.LogInformation($"HOLA {email}");
                return await _dBcontext.usuario.Include(u => u.userRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync(u => u.Email == email);
                //return await _dBcontext.usuario.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                $"Error getting Usuario by email:{email}: {ex.Message}");
                throw new Exception($"Error: GetByEmailAsync()");
            }
        }

        public async Task<bool> IsEnabled(Usuario request)
        {
            try
            {
                var result = await _dBcontext.usuario.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Email == request.Email);
                return result.isEnable;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                $"Error getting Usuario: {ex.Message}");
                throw new Exception($"Error: IsEnabled()");
            }
        }

        public async Task AddAsync(Usuario user)
        {
            await _dBcontext.usuario.AddAsync(user);
        }
    }
}
