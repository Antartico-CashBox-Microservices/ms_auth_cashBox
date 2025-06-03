using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ms_auth_cashBox.Core.Interfaces.Repository;

namespace ms_auth_cashBox.Core.Interfaces
{
    /// <summary>
    ///     El patrón Unit of Work (UoW) coordina el trabajo de múltiples repositorios bajo una misma
    ///     transacción y contexto de base de datos. Su función principal es:
    ///         Agrupar cambios en entidades y asegurarse de que se apliquen todos juntos(o ninguno), 
    ///         a través de una única llamada a SaveChangesAsync().
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        //We have only get because we don't want to set the repository.
        //setting the repository will be done in the UnitOfWork class
        IAuthRepository AuthRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICashBoxRepository CashBoxRepository { get; }
        ITerminalRepository TerminalRepository { get; }
        ISucursalRepository SucursalRepository { get; }
        ITerminalSessionRepository TerminalSessionRepository { get; }

        Task SaveChangesAsync();
        Task<int> CompleteAsync();
    }
}
