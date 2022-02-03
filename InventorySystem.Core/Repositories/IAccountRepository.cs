using InventorySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Repositories
{
    public interface IAccountRepository : IRepository<User>
    {
        User Register(User user);
        User Login(User user);
    }
}
