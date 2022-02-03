using InventorySystem.Core;
using InventorySystem.Core.Models;
using InventorySystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Login(User user)
        {
            return _unitOfWork.Users.IncludeAll().FirstOrDefault(u => u.Username == user.Username);
        }

        public User Register(User user)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();

            return _unitOfWork.Users.Find(u => u.Username == user.Username).FirstOrDefault();
        }
    }
}
