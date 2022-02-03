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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User Create(User user)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.Commit();

            var newUser = _unitOfWork.Users.GetById(user.Id);
            return newUser;
        }

        public void Delete(User user)
        {
            _unitOfWork.Users.Remove(user);
            _unitOfWork.Commit();
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.Users.IncludeAll().GetAll();
        }

        public User GetById(int id)
        {
            return _unitOfWork.Users.IncludeAll().GetById(id);
        }

        public void Update(int id, User user)
        {
            var existingUser = _unitOfWork.Users.GetById(id);
            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.RoleId = user.RoleId;
            existingUser.UserStatusId = user.UserStatusId;
            _unitOfWork.Commit();
        }
    }
}
