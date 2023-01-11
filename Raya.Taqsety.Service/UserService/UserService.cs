using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Infrastructure;
using Raya.Taqsety.Infrastructure.UserRepository;

namespace Raya.Taqsety.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        private readonly RoleManager<Role> _roleManager;
        private readonly IHttpContextAccessor _session;


        private static int _userId;
        public UserService(IUserRepository userRepository,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IHttpContextAccessor session,
            ApplicationDbContext applicationDbContext
            )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _session = session;
            _context = applicationDbContext;
        }

        public void SetUserId(int userId)
        {
            _userId = userId;
        }
        public int GetUserId()
        {
            return _userId;
        }

        public async Task<User> CreateNewUser(string userNameToCreate, int roleId, int HrId)
        {
            var result = _session.HttpContext.Session.TryGetValue("userId", out byte[] userIdOut);
            var identityResult = await _userManager
                .CreateAsync(new User { UserName = userNameToCreate, Email = userNameToCreate + "@Raya.com", IsNew = true, HRId = HrId }, "Raya@100");


            if (identityResult.Succeeded)
            {
                User userToAssignToRole = GetUserByUserName(userNameToCreate);
                Role roleToAssign =  GetRoles().Result.FirstOrDefault(x => x.Id == roleId, new Role { NormalizedName = "USER" });
                var isRoleAssigned = await _userManager.AddToRoleAsync(userToAssignToRole, roleToAssign.NormalizedName);
                var userCreated = await _context.Users.AsNoTracking().OrderBy(x => x.Id).LastOrDefaultAsync();
                if (isRoleAssigned.Succeeded && userCreated != null)
                    return userCreated;
                else
                    return null;
            }
            else
                return null;
        }
        public User? GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetUsersByIds(List<int> usersIds)
        {
            return _userRepository.GetUsersWithIds(usersIds);
        }

        public User? GetUserByUserName(string userName)
        {
            return _userRepository.GetUserByUserName(userName);
        }

        public async Task<bool> ResetPassword(string userName)
        {
            var UserToResetPassword = _userRepository.GetUserByUserName(userName);
            var removeResult = await _userManager.RemovePasswordAsync(UserToResetPassword);
            if (removeResult.Succeeded)
            {
                UserToResetPassword.IsNew = true;
                var addResult = await _userManager.AddPasswordAsync(UserToResetPassword, "Raya@100");
                if (addResult.Succeeded)
                    return true;
                else
                    return false;

            }
            else
                return false;
        }

        public async Task<bool> ChangePassword(User user, string oldPassword, string newPassword)
        {

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
                await _userRepository.MakeUserAsOldUser(user);//mark user as IsNew == false
            return result.Succeeded;
        }
        public async Task<bool> CheckUserNameAndPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetUserRoles(User userToLogin)
        {
            return await _userManager.GetRolesAsync(userToLogin);
        }

        public async Task<bool> ChangeUserRoleTo(string userName, int newRoleId)
        {
            User userToChangeRole = GetUserByUserName(userName);
            if (userToChangeRole == null)
                return false;

            Role roleToAssign = GetRoles().Result.FirstOrDefault(x => x.Id == newRoleId, new Role { NormalizedName = "USER" });
            var identityResult = await _userManager.RemoveFromRoleAsync(userToChangeRole, roleToAssign.NormalizedName);
            return identityResult.Succeeded;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<int> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public void ClearSession()
        {
            _userId = 0;
        }
    }
}

