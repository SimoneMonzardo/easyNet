using System;
using easyNetAPI.Data;
using easyNetAPI.Utility.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using easyNetAPI.Utility;
using easyNetAPI.Models;
using easyNetAPI.Models.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Security.Cryptography;
using System.Collections.Immutable;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        private readonly TokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly IImmutableSet<string> _forbiddenNames = new HashSet<string>()
        {
            "index","create","settings","saved"
        }.ToImmutableHashSet();

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db, TokenService tokenService, 
            IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Route("CheckToken"), AllowAnonymous]
        public async Task<ActionResult<string>> CheckToken()
        {
            var token = Request.Headers["Authorization"].ToString();
            try
            {
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                return Ok("Token is valid");
            }
            catch (Exception ex)
            {
                return BadRequest("Token is no longer valid");
            }
        }
        [HttpPost]
        [Route("Register"), AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            User applicationUser = new()
            {
                UserName = request.Username,
                Name = request.Name,
                Surname = request.Surname,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                NormalizedEmail = request.Email.ToUpper(),
                Email = request.Email,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state invalid");
            }
            if (_forbiddenNames.Contains(applicationUser.UserName))
            {
                return BadRequest("Username is not valid");
            }
            if (_db.Users.Where(u => u.NormalizedEmail == applicationUser.NormalizedEmail).Count() > 0)
                return BadRequest("Mail already used");
            if (_db.Users.Where(u => u.UserName == request.Username).Count() > 0)
                return BadRequest("Username already used");
            var result = await _userManager.CreateAsync(
               applicationUser, request.Password);
            if (result.Succeeded)
            {
                //crea l'utente in mongoDB
                await _unitOfWork.UserBehavior.AddAsync(new UserBehavior
                {
                    UserId = applicationUser.Id,
                    Administrator = false,
                    Company = new Company
                    {
                        CompanyId = 0,
                        Bot = new Bot(),
                        CompanyName = string.Empty,
                        ProfilePicture = string.Empty,
                        Documents = new List<string>()
                    },
                    Posts = new List<Post>(),
                    FollowedUsers = new List<string>(),
                    FollowersList = new List<string>(),
                    LikedPost = new List<int>(),
                    SavedPost = new List<int>(),
                    MentionedPost = new List<int>(),
                    ReportedPost = new List<int>()
                }) ;

                if (!_roleManager.RoleExistsAsync(SD.ROLE_MODERATOR).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_MODERATOR));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_COMPANY_ADMIN));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_EMPLOYEE));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_USER));
                }

                await _userManager.AddToRoleAsync(applicationUser, SD.ROLE_USER);
                request.Password = "";

                await _emailSender.SendEmailAsync(request.Email, "Email di conferma per Muznet", $"Ciao {request.Username}, <br> <a href=\"https://muznet.vercel.app/mail?userId={applicationUser.Id}\">Clicca qui per confermare la tua mail</a>");

                return Ok(new { Result = "User created successfully", UserId = applicationUser.Id});
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("confirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return BadRequest("Something went wrong can't find your user");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (!result.Succeeded)
                    return BadRequest("Something went wrong confirming your email");
                return Ok("Email confirmed succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        } 
        [HttpPost]
        [Route("RegisterFromModerator")]
        [Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> RegisterFromModerator(RegistrationRequestFromModerator request)
        {
            User applicationUser = new()
            {
                UserName = request.Username,
                Name = request.Name,
                Surname = request.Name,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                NormalizedEmail = request.Email.ToUpper(),
                Email = request.Email,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest("Model state invalid");
            }
            if (_forbiddenNames.Contains(applicationUser.UserName))
            {
                return BadRequest("Username is not valid");
            }
            if (_db.Users.Where(u => u.NormalizedEmail == applicationUser.NormalizedEmail).Count() > 0)
                return BadRequest("Mail already used");
            if (_db.Users.Where(u => u.UserName == request.Username).Count() > 0)
                return BadRequest("Username already used");
            var result = await _userManager.CreateAsync(
               applicationUser, request.Password);
            if (result.Succeeded)
            {
                //crea l'utente in mongoDB
                await _unitOfWork.UserBehavior.AddAsync(new UserBehavior
                {
                    UserId = applicationUser.Id,
                    Administrator = false,
                    Company = new Company(),
                    Posts = new List<Post>(),
                    FollowedUsers = new List<string>(),
                    FollowersList = new List<string>(),
                    LikedPost = new List<int>(),
                    SavedPost = new List<int>(),
                    MentionedPost = new List<int>(),
                    ReportedPost = new List<int>()
                });

                if (!_roleManager.RoleExistsAsync(SD.ROLE_MODERATOR).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_MODERATOR));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_COMPANY_ADMIN));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_EMPLOYEE));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_USER));
                }
                if(request.Role.Equals(SD.ROLE_MODERATOR)|| request.Role.Equals(SD.ROLE_EMPLOYEE) || request.Role.Equals(SD.ROLE_USER) || request.Role.Equals(SD.ROLE_COMPANY_ADMIN))
                    await _userManager.AddToRoleAsync(applicationUser, request.Role);
                else
                    await _userManager.AddToRoleAsync(applicationUser, SD.ROLE_USER);
                request.Password = "";
                return Ok("User created successfully");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("login"), AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var managedUser = await _userManager.FindByNameAsync(request.UserName);
            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }
            var applicationUserInDb = _db.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (applicationUserInDb is null)
            {
                return Unauthorized("User not found");
            }
            var roles = await _userManager.GetRolesAsync(applicationUserInDb);
            var accessToken = _tokenService.CreateToken(applicationUserInDb, roles);
            await _db.SaveChangesAsync();
            return Ok(new AuthResponse
            {
                Username = applicationUserInDb.UserName!,
                Email = applicationUserInDb.Email!,
                Token = accessToken,
                ProfilePicture = applicationUserInDb.ProfilePicture
            });
        }

        [HttpPost]
        [Route("changePassword"), Authorize (Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE}, {SD.ROLE_COMPANY_ADMIN}, {SD.ROLE_MODERATOR}")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var managedUser = await _userManager.FindByIdAsync(userId);
            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.OldPassword);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }
            var passwordChanged = await _userManager.ChangePasswordAsync(managedUser, request.OldPassword, request.NewPassword);
            if (!passwordChanged.Succeeded)
            {
                return BadRequest("Could not change password");
            }
            await _db.SaveChangesAsync();
            await _emailSender.SendEmailAsync(managedUser.Email, "La password è stata modificata" ,$"La password dell'account {managedUser.UserName} è stata cambiata");
            return Ok("Password Changed Successfully");
        }

        [HttpDelete]
        [Route("DeleteUser"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE}, {SD.ROLE_COMPANY_ADMIN}, {SD.ROLE_MODERATOR}")]
        public async Task<ActionResult<string>> DeleteUser()
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var managedUser = await _userManager.FindByIdAsync(userId);
            if (managedUser == null)
            {
                return BadRequest("User not found");
            }
            var result = await _userManager.DeleteAsync(managedUser);
            if (!result.Succeeded)
            {
                return BadRequest("There was a problem deleting the account");
            }
            await _db.SaveChangesAsync();

            //elimina dati da mongodb
            var userFromDb = await _unitOfWork.UserBehavior.GetFirstOrDefault(managedUser.Id);
            await _unitOfWork.UserBehavior.RemoveAsync(managedUser.Id);

            //eliminare tutta l'attività dell'utente dall'intero database
            await _unitOfWork.UserBehavior.RemoveUserActivityAsync(userFromDb, _unitOfWork);

            return Ok("User deleted successfully");
        }

        [HttpDelete]
        [Route("DeleteUserFromModerator")]
        [Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> DeleteUserFromModerator(string username)
        {
            if (username == null)
            {
                return BadRequest("User not found");
            }
            var userId = await AuthControllerUtility.GetUserIdFromUsername(username, _db);
            if (userId is null)
            {
                return BadRequest("User not found");
            }
            var managedUser = await _userManager.FindByIdAsync(userId);
            if (managedUser == null)
            {
                return BadRequest("User not found");
            }
            var result = await _userManager.DeleteAsync(managedUser);
            if (!result.Succeeded)
            {
                return BadRequest("There was a problem deleting the account");
            }
            await _db.SaveChangesAsync();

            //elimina dati da mongodb
            var userFromDb = await _unitOfWork.UserBehavior.GetFirstOrDefault(managedUser.Id);
            await _unitOfWork.UserBehavior.RemoveAsync(managedUser.Id);

            //eliminare tutta l'attività dell'utente dall'intero database
            await _unitOfWork.UserBehavior.RemoveUserActivityAsync(userFromDb, _unitOfWork);

            return Ok("User deleted successfully");
        }

        [HttpPost]
        [Route("editUserData"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE}, {SD.ROLE_COMPANY_ADMIN}, {SD.ROLE_MODERATOR}")]
        public async Task<ActionResult<string>> EditUserData([FromBody] EditUserDataRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var managedUser = await _userManager.FindByIdAsync(userId);
            if (managedUser == null)
            {
                return BadRequest("User not found");
            }
            var applicationUserInDb = _db.Users.FirstOrDefault(u => u.UserName == managedUser.UserName);
            if (applicationUserInDb is null)
            {
                return Unauthorized("User is not Authorized");
            }
            applicationUserInDb.Name = request.Name;
            applicationUserInDb.Surname = request.Surname;
            applicationUserInDb.DateOfBirth = request.DateOfBirth;
            applicationUserInDb.Gender = request.Gender;
            applicationUserInDb.ProfilePicture = request.ProfilePicture;
            await _db.SaveChangesAsync();
            return Ok("User Details updated successfully");
        }

        [HttpGet]
        [Route("GetUserData"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE}, {SD.ROLE_COMPANY_ADMIN}, {SD.ROLE_MODERATOR}")]
        public async Task<ActionResult<GetUserDataResponse>> GetUserData() {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var managedUser = await _userManager.FindByIdAsync(userId);
            if (managedUser == null)
            {
                return BadRequest("User not found");
            }
            var user = _db.Users.ToList().Where(u => u.Id == managedUser.Id).FirstOrDefault();
            return Ok(new GetUserDataResponse
            {
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                Surname = user.Surname,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                ProfilePicture = user.ProfilePicture,
                PhoneNumber = user.PhoneNumber
            });
        }

        [HttpPost("UploadProfilePicture"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<object> PostProfilePicture(IFormFile? file)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    var user =  _db.Users.FirstOrDefault(u=> u.Id == userId);
                    if (user is null)
                        return BadRequest("User not found");
                    if(user.ProfilePicture!= null)
                    {
                        await DeleteProfilePicture();
                    }
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images");
                    var extension = Path.GetExtension(file.FileName);
                    var link = Path.Combine(uploads, fileName + extension);
                    string url = "https://progettoeasynet.azurewebsites.net/images/" + fileName + extension; // da modificare con il link futuro del sito
                    using (var fileStreams = new FileStream(link, FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    //user.ProfilePicture = url;
                    //_db.Users.Update(user);
                    //_db.SaveChanges();
                    return Ok(url);
                }
                return BadRequest("File is null");
            }
            catch (Exception ex)
            {
                return BadRequest("Error " + ex.Message);
            }
        }

        [HttpDelete("DeleteProfilePicture"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<object> DeleteProfilePicture()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if (user is null)
                    return BadRequest("User not found");
                var link = user.ProfilePicture;
                if (link is null)
                    return BadRequest("There is no profile picture");
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string location = (new Uri(link)).PathAndQuery;
                var path = wwwRootPath+ location;
                System.IO.File.Delete(path);
                user.ProfilePicture = "";
                _db.Users.Update(user);
                _db.SaveChanges();
                return Ok("Deleted " + link);
            }
            catch (Exception ex)
            {
                return BadRequest("Error " + ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserIdFromUsernameModeratorCompanyAdmin")]
        [Authorize(Roles = $"{SD.ROLE_MODERATOR},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<ActionResult<string>> GetUserIdFromUsername_AuthModerator(string userName)
        {
            var user = _db.Users.First(u => u.UserName.Equals(userName));
            if (user == null)
                return "User not found";
            return Ok(user.Id);
        }
        [HttpGet]
        [Route("ForgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgottenPasswordAsync(string email)
        {
            try
            {
                var user = _db.Users.First(u => u.Email == email);
                if (user == null)
                    throw new Exception($"Can't find user with this email: {email}");
                var password = "";
                using (RNGCryptoServiceProvider cryptRNG = new RNGCryptoServiceProvider())
                {
                    byte[] tokenBuffer = new byte[8];
                    cryptRNG.GetBytes(tokenBuffer);
                    password =  Convert.ToBase64String(tokenBuffer);
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, password);
                await _emailSender.SendEmailAsync(email, $"Recupero della password per {user.UserName}", $"Ciao {user.UserName}, <br> la nuova password del tuo account è {password} per modificarla vai nelle impostazioni del tuo account");
                return Ok($"Email inviata correttamente alla mail {email}");
            }   
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }

    public static class AuthControllerUtility
    {
        public static async Task<ClaimsPrincipal> DecodeJWTToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://localhost:7260",
                ValidAudience = "https://localhost:7260",
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("!SomethingSecret!")),
            };
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }
        public static async Task<string> GetUserIdFromTokenAsync(string token)
        {
            token = token.Remove(0, 7);
            var principal = await DecodeJWTToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
            return userId;
        }
        public static async Task<string> GetUserIdFromUsername(string userName, AppDbContext _db)
        {
            if (_db.Users.Where(u => u.UserName.Equals(userName)).Count() == 0)
                return null;
            var user = _db.Users.First(u => u.UserName.Equals(userName));
            if (user == null)
                return null;
            return user.Id;
        }
    }
}