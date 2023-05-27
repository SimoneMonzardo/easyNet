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
        private readonly UserBehaviorSettings _userBehaviorSettings;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext db, TokenService tokenService, UserBehaviorSettings userBehaviorSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _tokenService = tokenService;
            _userBehaviorSettings = userBehaviorSettings;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            User applicationUser = new()
            {
                UserName = request.Username,
                Name = request.Name,
                Surname = request.Name,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                ProfilePicture = request.ProfilePicture,
                NormalizedEmail = request.Email.ToUpper(),
                Email = request.Email,
            };
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userManager.CreateAsync(
               applicationUser, request.Password);
            if (result.Succeeded)
            {
                //crea l'utente in mongoDB
                await _userBehaviorSettings.AddAsync(new UserBehavior
                {
                    UserId = applicationUser.Id,
                    Administrator = false,
                    Company = new Company(),
                    Posts = Array.Empty<Post>(),
                    FollowedUsers = Array.Empty<string>(),
                    FollowersList = Array.Empty<string>(),
                    LikedPost = Array.Empty<string>(),
                    SavedPost = Array.Empty<string>(),
                    MentionedPost = Array.Empty<string>()
                }) ;

                if (!_roleManager.RoleExistsAsync(SD.ROLE_MODERATOR).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_MODERATOR));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_COMPANY_ADMIN));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_EMPLOYEE));
                    await _roleManager.CreateAsync(new IdentityRole(SD.ROLE_USER));
                }
                //verifico il ruolo

                await _userManager.AddToRoleAsync(applicationUser, SD.ROLE_USER);
                request.Password = "";
                return CreatedAtAction(nameof(Register), new { email = request.Email }, request);
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("login")]
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
                return Unauthorized();
            }
            var roles = await _userManager.GetRolesAsync(applicationUserInDb);
            var accessToken = _tokenService.CreateToken(applicationUserInDb, roles);
            await _db.SaveChangesAsync();
            return Ok(new AuthResponse
            {
                Username = applicationUserInDb.UserName!,
                Email = applicationUserInDb.Email!,
                Token = accessToken,
            });
        }

        [HttpPost]
        [Route("changePassword")]
        public async Task<ActionResult<string>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = Request.Headers["Authorization"].ToString();
            token = token.Remove(0, 7);
            var principal = await AuthControllerUtility.DecodeJWTToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
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
            return Ok("Passoword Changed Successfully");
        }

        [HttpDelete]
        [Route("deleteUser")]
        public async Task<ActionResult<string>> DeleteUser()
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Remove(0, 7);
            var principal = await AuthControllerUtility.DecodeJWTToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
            if (userId == null)
            {
                return BadRequest("User not found");
            }
            var managedUser = await _userManager.FindByIdAsync(userId);
            if (managedUser == null)
            {
                return BadRequest("User not found");
            }
            var result = _userManager.DeleteAsync(managedUser);
            if (!result.IsCompletedSuccessfully)
            {
                return BadRequest("There was a problem deleting the account");
            }
            await _db.SaveChangesAsync();

            //elimina dati da mongodb
            var userFromDb = await _userBehaviorSettings.GetAsync(managedUser.Id);
            await _userBehaviorSettings.RemoveAsync(managedUser.Id);

            //eliminare tutta l'attività dell'utente dall'intero database
            var users = await _userBehaviorSettings.GetAllAsync();
            foreach (var user in users)
            {
                var followersList = user.FollowersList.ToList();
                if (followersList.Count !=0)
                {
                    if (followersList.Contains(managedUser.Id))
                    {
                        followersList.Remove(managedUser.Id);
                        user.FollowersList = followersList.ToArray();
                    }
                }

                var followedUsersList = user.FollowedUsers.ToList();
                if (followedUsersList.Count != 0)
                {
                    if (followedUsersList.Contains(managedUser.Id))
                    {
                        followedUsersList.Remove(managedUser.Id);
                        user.FollowedUsers = followedUsersList.ToArray();
                    }
                }

                //questo si può mettere in un altro metodo nel postrepository
                var postsListToDelete = userFromDb.Posts.ToList();
                foreach (var post in postsListToDelete)
                {
                    var likedPost = user.LikedPost.ToList();
                    if (likedPost.Count() != 0)
                    {
                        if (likedPost.Contains(post.PostId.ToString()))
                        {
                            likedPost.Remove(post.PostId.ToString());
                            user.LikedPost = likedPost.ToArray();
                        }
                    }


                    var savedPosts = user.SavedPost.ToList();
                    if (savedPosts.Count() != 0)
                    {
                        if (savedPosts.Contains(post.PostId.ToString()))
                        {
                            savedPosts.Remove(post.PostId.ToString());
                            user.SavedPost = savedPosts.ToArray();
                        }
                    }

                    var mentionedPosts = user.MentionedPost.ToList();
                    if (mentionedPosts.Count() != 0)
                    {
                        if (mentionedPosts.Contains(post.PostId.ToString()))
                        {
                            mentionedPosts.Remove(post.PostId.ToString());
                            user.MentionedPost = mentionedPosts.ToArray();
                        }
                    }

                }

                var postsList = user.Posts.ToList();
                foreach (var post in postsList)
                {
                    //cancella commenti sui post di altri utenti
                    var commentsList = post.Comments.ToList();
                    if (commentsList.Count() != 0)
                    {
                        foreach (var comment in commentsList)
                        {
                            if (comment.UserId == managedUser.Id)
                            {
                                commentsList.Remove(comment);
                            }
                        }
                        post.Comments = commentsList.ToArray();
                    }

                    //cancella like su post di altri uenti
                    var likesList = post.Likes.ToList();
                    if (likesList.Count != 0)
                    {
                        if (likesList.Contains(managedUser.Id))
                        {
                            likesList.Remove(managedUser.Id);
                        }
                        post.Likes = likesList.ToArray();
                    }
                    

                    //cancella tags su post di altri utenti
                    var tagsList = post.Tags.ToList();
                    if (tagsList.Count() != 0)
                    {
                        if (tagsList.Contains(managedUser.Id))
                        {
                            tagsList.Remove(managedUser.Id);
                        }
                        post.Tags = tagsList.ToArray();
                    }
                }
                user.Posts = postsList.ToArray();

                await _userBehaviorSettings.UpdateAsync(user.UserId, user);
            }

            return Ok("User deleted successfully");
        }

        [HttpPost]
        [Route("editUserData")]
        public async Task<ActionResult<string>> EditUserData([FromBody] EditUserDataRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Remove(0, 7);
            var principal = await AuthControllerUtility.DecodeJWTToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
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
                return Unauthorized();
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
        [Route("getUserData")]
        public async Task<ActionResult<GetUserDataResponse>> GetUserData() {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Remove(0, 7);
            var principal = await AuthControllerUtility.DecodeJWTToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
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
                ProfilePicture = user.ProfilePicture
            });
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
    }
}