using System;
using System.Data;
using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using NuGet.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace easyNetAPI.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<IdentityUser> userManager, ILogger<UserController> logger, AppDbContext db, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _db = db;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost("Follow"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<ActionResult<string>> FollowUserAsync(string userName)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
            {
                return BadRequest("User not found");
            }

            var managedUser = _db.Users.Where(u => u.Id == userId).FirstOrDefault();
            var userToFollow = _db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (userToFollow is null)
            {
                return BadRequest("User not found");
            }

            var managedUserBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(managedUser.Id);
            if (managedUserBehavior is null)
            {
                return BadRequest("User not found");
            }

            var followedUserBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userToFollow.Id);
            if (followedUserBehavior is null)
            {
                return BadRequest("User not found");
            }

            managedUserBehavior.FollowedUsers.Add(userToFollow.Id);
            await _unitOfWork.UserBehavior.UpdateOneAsync(managedUserBehavior.UserId, managedUserBehavior);

            followedUserBehavior.FollowersList.Add(managedUser.Id);
            await _unitOfWork.UserBehavior.UpdateOneAsync(followedUserBehavior.UserId, followedUserBehavior);
            return Ok("User followed successfully");    
        }

        [HttpPost("Unfollow")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<ActionResult<string>> UnfollowUserAsync(string userName)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
            {
                return BadRequest("User not found");
            }

            var managedUser = _db.Users.Where(u => u.Id == userId).FirstOrDefault();
            var userToFollow = _db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (userToFollow is null)
            {
                return BadRequest("User not found");
            }

            var managedUserBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(managedUser.Id);
            if (managedUserBehavior is null)
            {
                return BadRequest("User not found");
            }

            var followedUserBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userToFollow.Id);
            if (followedUserBehavior is null)
            {
                return BadRequest("User not found");
            }

            if (managedUserBehavior.FollowedUsers.Count() == 0)
            {
                if (!managedUserBehavior.FollowedUsers.Contains(userToFollow.Id))
                {
                    return BadRequest("Cannot unfollow user");
                }
                return BadRequest("Cannot unfollow user");
            }

            if (followedUserBehavior.FollowersList.Count() == 0)
            {
                if (!followedUserBehavior.FollowersList.Contains(managedUser.Id))
                {
                    return BadRequest("Cannot unfollow user");
                }
                return BadRequest("Cannot unfollow user");
            }

            managedUserBehavior.FollowedUsers.Remove(userToFollow.Id);
            await _unitOfWork.UserBehavior.UpdateOneAsync(managedUserBehavior.UserId, managedUserBehavior);

            followedUserBehavior.FollowersList.Remove(managedUser.Id);
            await _unitOfWork.UserBehavior.UpdateOneAsync(followedUserBehavior.UserId, followedUserBehavior);

            return Ok("User unfollowed successfully");
        }

        //prende i follower di un utente specificato
        [HttpGet("GetUserFollowers")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
        public async Task<List<string>?> GetUserFollowers(string userName)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
            {
                return null;
            }

            var userToSearch = _db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (userToSearch is null)
            {
                return null;
            }

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userToSearch.Id);

            var returnList = new List<string>(); 

            if (userBehavior.FollowersList.Count() != 0)
            {
                foreach (var user in userBehavior.FollowersList)
                {
                    returnList.Add(
                        _db.Users.Where(u=> u.Id == user).Select(u => u.UserName).FirstOrDefault()
                        );
                }
            }

            return returnList;
        }

        //prende i follower dell'utente che ha fatto la richiesta
        [HttpGet("GetFollowers")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<List<string>?> GetUserFollowers()
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
            {
                return null;
            }

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);

            var returnList = new List<string>();

            if (userBehavior.FollowersList.Count() != 0)
            {
                foreach (var user in userBehavior.FollowersList)
                {
                    returnList.Add(
                        _db.Users.Where(u => u.Id == user).Select(u => u.UserName).FirstOrDefault()
                        );
                }
            }

            return returnList;
        }


        //prende i follower di un utente specificato
        [HttpGet("GetUserFollowedList")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
        public async Task<List<string>?> GetUserFollowedList(string userName)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
            {
                return null;
            }

            var userToSearch = _db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            if (userToSearch is null)
            {
                return null;
            }

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userToSearch.Id);

            var returnList = new List<string>();

            if (userBehavior.FollowedUsers.Count() != 0)
            {
                foreach (var user in userBehavior.FollowedUsers)
                {
                    returnList.Add(
                        _db.Users.Where(u => u.Id == user).Select(u => u.UserName).FirstOrDefault()
                        );
                }
            }

            return returnList;
        }

        //prende i followed dell'utente che ha fatto la richiesta
        [HttpGet("GetFollowed"), Authorize(Roles = SD.ROLE_USER)]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<List<string>?> GetUserFollowed()
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
            {
                return null;
            }

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);

            var returnList = new List<string>();

            if (userBehavior.FollowedUsers.Count() != 0)
            {
                foreach (var user in userBehavior.FollowedUsers)
                {
                    returnList.Add(
                        _db.Users.Where(u => u.Id == user).Select(u => u.UserName).FirstOrDefault()
                        );
                }
            }

            return returnList;
        }

        [HttpPost("ConvertToCompanyAdmin_AuthModerator"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> ConvertToAdmin(string username, int companyId)
        {
            if (username.IsNullOrEmpty())
                return BadRequest("Insert username");
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId is null || userId.Equals(string.Empty))
                return BadRequest("UserId not found");

            var user = _db.Users.Find(userId);
            if (user is null)
                return BadRequest("User not found");
            
            if (await _userManager.IsInRoleAsync(user, SD.ROLE_MODERATOR))
                return BadRequest("User is moderator");

            if (await _userManager.IsInRoleAsync(user, SD.ROLE_EMPLOYEE))
                return BadRequest("User is employee");

            if (await _userManager.IsInRoleAsync(user, SD.ROLE_COMPANY_ADMIN))
                return BadRequest("User is already Company Admin");

            var company = await _unitOfWork.Company.GetFirstOrDefault(companyId);
            if (company is null)
                return BadRequest("Company does not exist");

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
            if (userBehavior is null)
                return BadRequest("user not found");

            userBehavior.Company = company;
            var resultCompany = await _unitOfWork.UserBehavior.UpdateOneAsync(userId, userBehavior);
            if (!resultCompany)
                return BadRequest("Could not add company to user");

            var result = await _userManager.AddToRoleAsync(user, SD.ROLE_COMPANY_ADMIN);
            if (result.Succeeded)
                return Ok("User is now admin");

            return BadRequest("Could not make user admin see exception: " + result.Errors);
        }

        [HttpPost("ConvertToModerator_AuthModerator"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> ConvertToModerator(string username)
        {
            if (username.IsNullOrEmpty())
                return BadRequest("Insert username");
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId is null || userId.Equals(string.Empty))
                return BadRequest("UserId not found");

            var user = _db.Users.Find(userId);
            if (user is null)
                return BadRequest("User not found");

            if (await _userManager.IsInRoleAsync(user, SD.ROLE_MODERATOR))
                return BadRequest("User is already moderator");

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            var result = await _userManager.AddToRoleAsync(user, SD.ROLE_MODERATOR);
            if (result.Succeeded)
                return Ok("User is now moderator");

            return BadRequest("Could not make user moderator see exception: " + result.Errors);
        }

        [HttpPost("ConvertToEmployee_AuthCompanyAdmin"), Authorize(Roles = SD.ROLE_COMPANY_ADMIN)]
        public async Task<ActionResult<string>> ConvertToEmployee(string username)
        {
            if (username.IsNullOrEmpty())
                return BadRequest("Insert username");

            var token = Request.Headers["Authorization"].ToString();
            var adminId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (adminId.IsNullOrEmpty())
                return BadRequest("AdminId not found");
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId.IsNullOrEmpty())
                return BadRequest("UserId not found");

            var user = _db.Users.Find(userId);
            if (user is null)
                return BadRequest("User not found");

            var adminBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(adminId);
            if (adminBehavior is null)
                return BadRequest("admin not found");
            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
            if (userBehavior is null)
                return BadRequest("user not found");

            if (await _userManager.IsInRoleAsync(user, SD.ROLE_MODERATOR))
                return BadRequest("User is moderator");

            if (await _userManager.IsInRoleAsync(user, SD.ROLE_COMPANY_ADMIN))
                return BadRequest("User is company admin");

            if (await _userManager.IsInRoleAsync(user, SD.ROLE_EMPLOYEE))
                return BadRequest("User is already Employee");

            userBehavior.Company = adminBehavior.Company;
            var resultCompany = await _unitOfWork.UserBehavior.UpdateOneAsync(userId, userBehavior);
            if(!resultCompany)
                return BadRequest("could not update user company");
            var result = await _userManager.AddToRoleAsync(user, SD.ROLE_EMPLOYEE);
            if (result.Succeeded)
                return Ok("User is now employee");

            return BadRequest("Could not make user employee see exception: " + result.Errors);
        }

        [HttpPost("RemoveCompanyAdmin_AuthModerator"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> RemoveFromAdmin(string username)
        {
            if (username.IsNullOrEmpty())
                return BadRequest("Insert username");
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId is null || userId.Equals(string.Empty))
                return BadRequest("UserId not found");

            var user = _db.Users.Find(userId);
            if (user is null)
                return BadRequest("User not found");
            if (!_userManager.GetUsersInRoleAsync(SD.ROLE_COMPANY_ADMIN).Result.Contains(user))
                return BadRequest("User is not admin");

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
            if (userBehavior is null)
                return BadRequest("user not found");


            userBehavior.Company = new Company() { CompanyId = 0, CompanyName = "" };
            var resultCompany = await _unitOfWork.UserBehavior.UpdateOneAsync(userId, userBehavior);
            if (!resultCompany)
                return BadRequest("Could not remove company from user");


            var result = await _userManager.RemoveFromRoleAsync(user, SD.ROLE_COMPANY_ADMIN);
            if (result.Succeeded)
                return Ok("User has been removed from role admin");

            return BadRequest("Could not remove user from role admin see exception: " + result.Errors);
        }

        [HttpPost("RemoveFromModerator_AuthModerator"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> RemoveFromModerator(string username)
        {
            if (username.IsNullOrEmpty())
                return BadRequest("Insert username");
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId is null || userId.Equals(string.Empty))
                return BadRequest("UserId not found");

            var user = _db.Users.Find(userId);
            if (user is null)
                return BadRequest("User not found");
            if (!_userManager.GetUsersInRoleAsync(SD.ROLE_MODERATOR).Result.Contains(user))
                return BadRequest("User is not moderator");
            var result = await _userManager.RemoveFromRoleAsync(user, SD.ROLE_MODERATOR);
            if (!result.Succeeded)
                return BadRequest("Could not remove user from role moderator see exception: " + result.Errors);
            result = await _userManager.AddToRoleAsync(user, SD.ROLE_USER);
            if(result.Succeeded)
                return Ok("User has been removed from role moderator and added to role user");
            return BadRequest("Could not add user to role user see exception: " + result.Errors);
        }

        [HttpPost("RemoveFromEmployee_AuthCompanyAdmin"), Authorize(Roles = SD.ROLE_COMPANY_ADMIN)]
        public async Task<ActionResult<string>> RemoveFromEmployee(string username)
        {
            var token = Request.Headers["Authorization"].ToString();
            var adminId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (adminId.IsNullOrEmpty())
                return BadRequest("AdminId not found");

            if (username.IsNullOrEmpty())
                return BadRequest("Insert username");
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId is null || userId.Equals(string.Empty))
                return BadRequest("UserId not found");

            var user = _db.Users.Find(userId);
            if (user is null)
                return BadRequest("User not found");

            if (!_userManager.GetUsersInRoleAsync(SD.ROLE_EMPLOYEE).Result.Contains(user))
                return BadRequest("User is not employee");

            var adminBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(adminId);
            if (adminBehavior is null)
                return BadRequest("admin not found");

            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
            if (userBehavior is null)
                return BadRequest("user not found");
            if (userBehavior.Company is null)
                userBehavior.Company = new Company() { CompanyId = 0, CompanyName = "" };
            if (adminBehavior.Company is null)
                return BadRequest("Admin doesn't have a company");
            if (adminBehavior.Company.CompanyId != userBehavior.Company.CompanyId)
                return BadRequest("admin cannot remove user from role employee");


            userBehavior.Company = new Company() { CompanyId = 0, CompanyName = "" };
            var resultCompany = await _unitOfWork.UserBehavior.UpdateOneAsync(userId, userBehavior);
            if (!resultCompany)
                return BadRequest("Could not remove company from user");


            var result = await _userManager.RemoveFromRoleAsync(user, SD.ROLE_EMPLOYEE);

            if (result.Succeeded)
                return Ok("User has been removed from role employee");

            return BadRequest("Could not remove user from role employee see exception: " + result.Errors);
        }
        [HttpGet("GetAllUsernameList")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
        public async Task<List<string>> GetAllUsernameAsync()
        {
            try
            {
                var allUsers = await _unitOfWork.UserBehavior.GetAllAsync();
                var usernames = new List<string>();
                foreach (var item in allUsers)
                {
                    var username_of_id = await _db.Users.FindAsync(item.UserId);
                    if (username_of_id == null)
                        continue;
                    usernames.Add(username_of_id.UserName);
                }
                return usernames;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet("GetUserData")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
        public async Task<UserBehavior> GetUserDataAsync(string username)
        {
            try
            {
                var id = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
                if (id == null)
                {
                    return null;
                }
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(id);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}