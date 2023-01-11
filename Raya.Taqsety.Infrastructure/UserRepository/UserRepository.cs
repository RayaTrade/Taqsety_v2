using System;
using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;

namespace Raya.Taqsety.Infrastructure.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> DeleteUser(int Id)
        {
            var toDeleteUser = _applicationDbContext.Users.FirstOrDefault(x => x.Id == Id);
            if (toDeleteUser == null)
                return 0;
            else
            {
                toDeleteUser.IsDeleted = true;
                _applicationDbContext.Users.Update(toDeleteUser);
                var result = await _applicationDbContext.SaveChangesAsync();
                return result;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _applicationDbContext.Users.Where(x => x.IsDeleted != true).ToListAsync();
        }

        public User? GetById(int userId)
        {
            User? user = new();
            try
            {
                user = _applicationDbContext.Users.FirstOrDefault(x => x.Id == userId && x.IsDeleted == false);
            }
            catch (Exception ex)
            {

                throw;
            }
            return user;
        }

        public User? GetUserByUserName(string userName)
        {
            return _applicationDbContext.Users.FirstOrDefault(x => x.UserName == userName && !x.IsDeleted);
        }

        public IEnumerable<User> GetUsersWithIds(List<int> usersIds)
        {
            return _applicationDbContext.Users.Where(x => usersIds.Contains(x.Id));
        }

        public async Task<int> MakeUserAsOldUser(User user)
        {
            user.IsNew = false;
            _applicationDbContext.Users.Update(user);
            var result = await _applicationDbContext.SaveChangesAsync();
            return result;
        }
    }
}

