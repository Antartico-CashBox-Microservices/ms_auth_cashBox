using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ms_auth_cashBox.Core.Entities;
using ms_auth_cashBox.Core.Interfaces;
using ms_auth_cashBox.Core.Interfaces.Repository;
using ms_auth_cashBox.Infrastructure.DataBaseIttion;
using ms_auth_cashBox.Infrastructure.Interfaces.Repository;

namespace ms_auth_cashBox.Infrastructure.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        #region -- Constructor --
        //private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ApplicationDbContext _dbContext;
        private readonly ICashBoxRepository _cashBoxRepository;
        private readonly ITerminalRepository _terminalRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ITerminalSessionRepository _terminalSessionRepository;

        public UnitOfWork(ApplicationDbContext dbContext, ILoggerFactory logger)
        {
            _dbContext = dbContext;
            //_logger = logger.CreateLogger("Logs");
            _loggerFactory = logger;
        }
        #endregion

        public ICashBoxRepository CashBoxRepository
        {
            get
            {
                if (_cashBoxRepository != null)
                {
                    return _cashBoxRepository;
                }
                else { return new CashBoxRepository(_dbContext, _loggerFactory.CreateLogger<GenericRepository<CashBox>>()); }
            }
        }

        public ITerminalRepository TerminalRepository
        {
            get
            {
                if (_terminalRepository != null) { return _terminalRepository; }
                else { return new TerminalRepository(_dbContext, _loggerFactory.CreateLogger<GenericRepository<Terminal>>()); }
            }
        }

        public ISucursalRepository SucursalRepository
        {
            get
            {
                if (_sucursalRepository != null) { return _sucursalRepository; }
                else { return new SucursalRepository(_dbContext, _loggerFactory.CreateLogger<GenericRepository<Sucursal>>()); }
            }
        }

        public IAuthRepository AuthRepository
        {
            get
            {
                if (_authRepository != null) { return _authRepository; }
                else { return new AuthRepository(_dbContext, _loggerFactory.CreateLogger<GenericRepository<Usuario>>()); }
            }
        }
        
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository != null) { return _roleRepository; }
                else { return new RoleReposotory(_dbContext, _loggerFactory.CreateLogger<GenericRepository<Role>>()); }
            }
        }

        public ITerminalSessionRepository TerminalSessionRepository
        {
            get
            {
                if (_terminalSessionRepository != null) { return _terminalSessionRepository; }
                else { return new TerminalSessionRepository(_dbContext, _loggerFactory.CreateLogger<GenericRepository<TerminalSession>>()); }
            }
        }

        #region -- IDisposable --
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
        #endregion
        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
