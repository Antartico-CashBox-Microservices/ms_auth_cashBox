using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using ms_auth_cashBox.Core.Entities;
using ms_auth_cashBox.Core.Interfaces.Repository;
using ms_auth_cashBox.Infrastructure.DataBaseIttion;

namespace ms_auth_cashBox.Infrastructure.Interfaces.Repository
{
    public class RoleReposotory : GenericRepository<Role>, IRoleRepository
    {
        private readonly ILogger _logger;

        public RoleReposotory(ApplicationDbContext dbContext, ILogger logger) : base(dbContext, logger)
        {
            _logger = logger;
        }
        
        public Task<Role> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
