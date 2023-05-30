using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Data;
using System.Security.Claims;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly ILogger<TagController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public readonly AppDbContext _db;

        public TagController(ILogger<TagController> logger, IUnitOfWork unitOfWork, AppDbContext db)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
        }

        [HttpPost("UpsertTag")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> PostTagAsync(int postId, List<string> usernames)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                {
                    return BadRequest("Not Logged in");
                }
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                if (post == null)
                {
                    return BadRequest("Post doesn't exist");
                }
                if (post.UserId != userId)
                {
                    return Forbid("Cannot add tags");
                }
                foreach (var item in usernames)
                {
                    //check if user exists
                    var user = _db.Users.Where(u => u.UserName == item).FirstOrDefault();
                    if (user is not null)
                    {
                        if (post.Tags is null)
                            post.Tags = new List<string>();
                        if(!post.Tags.Contains(userId))
                            post.Tags.Add(user.Id);
                        //aggiorna i mentioned posts dell'utente menzionato
                        var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(user.Id);
                        if (userBehavior is not null)
                        {
                            if (userBehavior.MentionedPost is null)
                                userBehavior.MentionedPost = new List<int>();
                            if (!userBehavior.MentionedPost.Contains(postId))
                            {
                                userBehavior.MentionedPost.Add(post.PostId);
                                var result = await _unitOfWork.UserBehavior.UpdateOneAsync(user.Id, userBehavior);
                                if (!result)
                                {
                                    return BadRequest("Someting went wrong");
                                }
                            }
                        }
                    }
                }
                var postResult = await _unitOfWork.Post.UpdateOneAsync(post);
                if (postResult)
                {
                    return Ok("Tags added succesfully");
                }
                return BadRequest("Someting went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetTagsOfPost")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> GetTagsOfPostAsync(int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                {
                    return BadRequest("Not Logged in");
                }
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                var userNames = new List<string>();
                post.Tags.ForEach(i => userNames.Add(_db.Users.Where(u => u.Id == i).Select(u => u.UserName).FirstOrDefault()));
                return Ok(userNames);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetPostsWhereTagged")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<IActionResult> GetPostWhereTaggedAsync(string userName)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = _db.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstOrDefault();
                if (userId == null)
                {
                    return BadRequest("Not Logged in");
                }
                var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (userBehavior is null)
                {
                    return BadRequest("User not found");
                }
                var postsList = new List<Post>();
                if (userBehavior.MentionedPost.Count() != 0)
                {
                    foreach (var postId in userBehavior.MentionedPost)
                    {
                        var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                        if (post != null)
                        {
                            postsList.Add(post);
                        }
                    }
                    return Ok(postsList);
                }
                return Ok("User not tagged");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpDelete("DeleteTag"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<ActionResult<string>> DeleteTagAsync(List<string> usersList, int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                if (post is null)
                {
                    return BadRequest("Post not found");
                }
                if (post.UserId != userId)
                {
                    return Forbid("Can't delete tags");
                }
                if (post.Tags.Count() == 0)
                {
                    return BadRequest("Tags don't exist");
                }
                foreach (var tag in usersList)
                {
                    string taggedUserId = _db.Users.Where(u => u.UserName == tag).Select(u => u.Id).FirstOrDefault();
                    if (post.Tags.Contains(taggedUserId))
                    {
                        post.Tags.Remove(taggedUserId);

                        var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(taggedUserId);
                        if (userBehavior != null)
                        {
                            userBehavior.MentionedPost.Remove(post.PostId);
                            var userResult = await _unitOfWork.UserBehavior.UpdateOneAsync(taggedUserId, userBehavior);
                            if (!userResult)
                            {
                                return BadRequest("There was an error deleting the tag");
                            }
                        }
                    }
                }
                var postResult = await _unitOfWork.Post.UpdateOneAsync(post);
                if (postResult)
                {
                    return Ok("Tag deleted successfully");
                }
                return BadRequest("Tag not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Unandled exeption: " + ex.Message);
            }
        }
    }
}