using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms_auth_cashBox.Core.Entities
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Usuario User { get; set; } = default!;

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
    }
}
