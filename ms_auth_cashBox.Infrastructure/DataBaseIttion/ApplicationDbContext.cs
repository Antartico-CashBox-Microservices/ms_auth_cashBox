using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ms_auth_cashBox.Core.Entities;

namespace ms_auth_cashBox.Infrastructure.DataBaseIttion
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Sucursal> sucursal { get; set; }
        public DbSet<Terminal> terminal { get; set; }
        public DbSet<CashBox> cashBox { get; set; }
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Role> role { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<TerminalSession> terminalSession { get; set; }
    }
}
