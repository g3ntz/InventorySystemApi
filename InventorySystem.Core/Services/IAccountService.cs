using InventorySystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Core.Services
{
    public interface IAccountService
    {
        User Register(User user);
        User Login(User user);
    }
}
