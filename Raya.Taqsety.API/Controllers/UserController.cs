using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Raya.Taqsety.Core.Models;
using Raya.Taqsety.Service.DTOs;
using Raya.Taqsety.Service.UserService;

namespace Raya.Taqsety.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] UserRoleModel userRoleModel)
        {
            
            var user = _userService.GetUserByUserName(userRoleModel.UserName);
            if (user != null)
                return new JsonResult(new { Succeded = false, Message = "This userName Already exists", Content = "" });

            else
            {
                if (userRoleModel.RoleId != 0)
                {
                    var isCreated = await _userService.CreateNewUser(userRoleModel.UserName,(int) userRoleModel.RoleId,(int) userRoleModel.HRID);
                    if (isCreated != null)
                        return new JsonResult(new { Succeded = true, Message = "User Ceated Succefully", Content = isCreated });
                    else
                        return new JsonResult(new { Succeded = false, Message = "somthing went wrong Please Try again", Content = "" });
                }
                else
                    return new JsonResult(new { Succeded = false, Message = "you must specify the assigned role", Content = "" });

            }
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<JsonResult> GetAllUsers()
        {
            IEnumerable<User> users = await _userService.GetAllUsers();
            IEnumerable<UserDTO> allUsers = _mapper.Map<IEnumerable<UserDTO>>(users);
            if (allUsers != null)
                return new JsonResult(new { Succeded = true, Message = "All users got successfully", Content = allUsers });
            else
                return new JsonResult(new { Succeded = false, Message = "somthing went wrong!", Content = allUsers });
        }


        [HttpGet]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string UserNameToResetPassword)
        {
            var user = _userService.GetUserByUserName(UserNameToResetPassword);
            if (user == null)
                return new JsonResult(new { Succeded = false, Message = "This User Does Not Exist", Content = "" });

            else
            {
                var isSucceded = await _userService.ResetPassword(UserNameToResetPassword);
                if (isSucceded)
                    return new JsonResult(new { Succeded = true, Message = "Password Reset Succefullt the Default Password Is Raya@100", Content = "" });
                else
                    return new JsonResult(new { Succeded = false, Message = "Something Went Wrong", Content = "" });
            }
        }
        [HttpPost]
        [Route("ChangePassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel userToChangePassModel)
        {
            if (userToChangePassModel.OldPassword == userToChangePassModel.NewPassword)
                return new JsonResult(new { Succeded = false, Message = "the current password and the new passowrd are the same you must change", Content = "" });

            var userToChangePassWord = _userService.GetUserByUserName(userToChangePassModel.UserName);
            if (userToChangePassWord == null)
                return new JsonResult(new { Succeded = false, Message = "This User Does Not Exists" });

            var isUserToChangePassWordVerified = await _userService.CheckUserNameAndPassword(userToChangePassWord, userToChangePassModel.OldPassword);
            if (!isUserToChangePassWordVerified)
                return new JsonResult(new { Succeded = false, Message = "The Old Password Is Not Correct", Content = "" });
            else
            {
                var isSucceded = await _userService.ChangePassword(userToChangePassWord, userToChangePassModel.OldPassword, userToChangePassModel.NewPassword);
                if (isSucceded)
                    return new JsonResult(new { Succeded = true, Message = "Password Was Changed Succefully", Content = "" });
                else
                    return new JsonResult(new { Succeded = false, Message = "The New Password Must Contain At least 1 Capital Chachter, At Least 1 Small charchter, At Least 1 Number And At least 1 Special Charchter ", Content = "" });
            }
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel userNameToLogin)
        {
            var userToLogin = _userService.GetUserByUserName(userNameToLogin.UserName);
            if (userToLogin == null)
            {
                return new JsonResult(new { Succeded = false, Message = "This User does not Exist" });
            }
            //user does exists
            bool isPasswordCorrect = await _userService.CheckUserNameAndPassword(userToLogin, userNameToLogin.Password);
            if (isPasswordCorrect)//user name and password are correct
            {
                IEnumerable<string> userRoles = await _userService.GetUserRoles(userToLogin);
                var authClaims = new List<Claim> {
                    new Claim("Email", userToLogin.Email),
                    new Claim("UserId", userToLogin.Id.ToString()),

                     //new Claim(JwtRegisteredClaimNames.Sub, userToLogin.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                 var roles = await _userService.GetRoles();
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim("Role", userRole));
                    authClaims.Add(new Claim("RoleId", roles.First(x=>x.Name == userRole).Id.ToString()));
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                HttpContext.Session.SetString("Token", token.ToString());//save token in session

                SetUserDataInSession(userToLogin);
                _userService.SetUserId(userToLogin.Id);
                return new JsonResult(new
                {
                    succeded = true,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Content = userToLogin
                });
            }
            return new JsonResult(new { Succeded = false, Message = "Password is incorect" });
        }
        private void SetUserDataInSession(User loggedInUser)
        {
            HttpContext.Session.SetString("userId", loggedInUser.Id.ToString());
            HttpContext.Session.SetString("userName", loggedInUser.UserName.ToString());
            HttpContext.Session.SetString("HRId", loggedInUser.HRId.ToString());

        }

        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            IEnumerable<RoleDTO> rolesDto = _mapper.Map<IEnumerable<RoleDTO>>(_userService.GetRoles());
            return new JsonResult(rolesDto);
        }

        [HttpPost]
        [Route("ChangeUserRoleTo")]
        public async Task<IActionResult> ChangeUserRoleTo([FromBody] UserRoleModel userRoleModel)
        {
            if (userRoleModel.RoleId > 2)
                return new JsonResult(new { Succeded = false, Message = "This Role Id does not exists" });
            //if user exists 
            User? doesUsersExists = _userService.GetUserByUserName(userRoleModel.UserName);
            if (doesUsersExists == null)
                return new JsonResult(new { Succeded = false, Message = "this user does not exists" });
            else
            {
                bool isChangedSuccefully = await _userService.ChangeUserRoleTo(userRoleModel.UserName,(int) userRoleModel.RoleId);
                if (isChangedSuccefully)
                    return new JsonResult(new { Succeded = true, Message = "Role is Changed Succefully" });
                else
                    return new JsonResult(new { Succeded = false, Message = "Role is Changed Succefully" });

            }
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<JsonResult> DeleteUser(int Id)
        {
            //check if this user does exist
            User? doesUsersExists = _userService.GetUserById(Id);
            if (doesUsersExists == null)
                return new JsonResult(new { Succeded = false, Message = "This user does not Exsit", Content = "" });
            int result = await _userService.DeleteUser(Id);
            if (result == 1)
                return new JsonResult(new { Succeded = true, Message = "user Deleted successfully", Content = "" });
            else
                return new JsonResult(new { Succeded = false, Message = "Somthing went wrong", Content = "" });
        }

        [HttpGet]
        [Route("Logout")]
        [AllowAnonymous]
        public async Task Logout()
        {
            //HttpContext.Session.Clear();
            _userService.ClearSession();

        }
    }
}
