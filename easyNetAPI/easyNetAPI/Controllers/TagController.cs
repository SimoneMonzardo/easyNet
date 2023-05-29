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
        [HttpPost("PostTags_AuthUser")]
        [Authorize(Roles = SD.ROLE_USER)]
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
                    tags.Add( await AuthControllerUtility.GetUserIdFromUsername(item, _db) );
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
        [HttpGet("GetTagsOfPost_AuthUser")]
        [Authorize(Roles = SD.ROLE_USER)]
        public async Task<IActionResult> GetTagsOfPostAsync(int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                var tagsId = post.Tags;
                if (tagsId.IsNullOrEmpty())
                    return Ok("[]");
                var tags = new List<string>();
                tagsId.ForEach(tag=>tags.Add(_db.Users.FirstOrDefault(u=>u.Id.Equals(tag)).UserName));
                return Ok(tags);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetPostWhereTagged_AuthUser")]
        [Authorize(Roles = SD.ROLE_USER)]
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
    }
}
