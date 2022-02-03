
using InventorySystem.Core.Models;
using InventorySystem.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private InventoryDbContext InventoryDbContext
        {
            get { return Context as InventoryDbContext; }
        }

        public UserRepository(InventoryDbContext context)
            : base(context)
        { }

        public override IRepository<User> IncludeAll()
        {
            return base.Include(x => x.Role, x => x.UserStatus);
        }
    }
}
