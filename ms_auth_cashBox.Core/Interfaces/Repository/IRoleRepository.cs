using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Core.Interfaces.Repository
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetById(int id);
    }
}
