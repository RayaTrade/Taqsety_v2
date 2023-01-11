using System;
using Raya.Taqsety.Core.Models;

namespace Raya.Taqsety.Service.UserService
{
    public interface IUserService
    {

        User? GetUserById(int id);
        IEnumerable<User> GetUsersByIds(List<int> userIds);
        Task<User> CreateNewUser(string userNameToCreate, int roleId, int HrId);
        User? GetUserByUserName(string userName);
        Task<bool> ResetPassword(string userName);
        Task<bool> ChangePassword(User user, string oldPassword, string newPassword);
        Task<bool> CheckUserNameAndPassword(User user, string password);
        Task<IEnumerable<Role>> GetRoles();
        Task<IEnumerable<string>> GetUserRoles(User userToLogin);
        Task<List<User>> GetAllUsers();
        Task<bool> ChangeUserRoleTo(string userName, int newRoleId);
        Task<int> DeleteUser(int userId);
        public void SetUserId(int userId);
        public int GetUserId();
        void ClearSession();
    }
}

