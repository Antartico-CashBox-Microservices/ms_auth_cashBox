using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Core.Interfaces.Repository
{
    public interface IAuthRepository:IGenericRepository<Usuario>
    {
        Task<bool> IsEnabled(Usuario request);
        Task<Usuario?> GetByEmailAsync(string email);
        Task AddAsync(Usuario user);
    }
}