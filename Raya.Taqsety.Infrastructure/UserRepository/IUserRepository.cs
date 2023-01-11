using System;
using Raya.Taqsety.Core.Models;

namespace Raya.Taqsety.Infrastructure.UserRepository
{
    public interface IUserRepository
    {
        User? GetById(int userId);
        IEnumerable<User> GetUsersWithIds(List<int> usersIds);
        User? GetUserByUserName(string userName);
        Task<List<User>> GetAllUsers();
        Task<int> DeleteUser(int Id);
        Task<int> MakeUserAsOldUser(User user);


    }
}

