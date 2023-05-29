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
                    return BadRequest("Not Logged in");
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                if (post == null)
                    return BadRequest("Post doesn't exist");
                List<string> tags = new List<string>();
                foreach (var item in usernames)
                {
                    tags.Add(item);
                }
                if (tags.Contains("user not found"))
                    return BadRequest("one user does not exists");
                post.Tags = tags;
                _unitOfWork.Post.UpdateOneAsync(post);
                return Ok("Tags added succesfully");
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
                    return BadRequest("Not Logged in");
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                var tags = post.Tags;
                if (tags.IsNullOrEmpty())
                    return Ok("[]");
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetPostWhereTagged")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<IActionResult> GetPostWhereTaggedAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var posts = (await _unitOfWork.Post.GetAllAsync()).Where(p => p.Tags.IsNullOrEmpty()?false:p.Tags.Contains(userId)).ToList();
                if (posts.IsNullOrEmpty())
                    return Ok("[]");
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpDelete("DeleteTag"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
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
                    if (post.Tags.Contains(tag))
                    {
                        post.Tags.Remove(tag);
                        var managedUserId = _db.Users.Where(u => u.UserName == tag).FirstOrDefault().Id;
                        if (managedUserId is not null)
                        {
                            var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(managedUserId);
                            if (userBehavior != null)
                            {
                                userBehavior.MentionedPost.Remove(post.PostId);
                                var userResult = await _unitOfWork.UserBehavior.UpdateOneAsync(managedUserId, userBehavior);
                                if (!userResult)
                                {
                                    return BadRequest("There was an error deleting the tag");
                                }
                            }
                        }
                    }
                }
                var postResult = await _unitOfWork.Post.UpdateOneAsync(post);
                if (postResult)
                {
                    return Ok("Tag deleted succesfully");
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
