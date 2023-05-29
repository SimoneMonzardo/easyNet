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
    public class HashtagController : Controller
    {
        private readonly ILogger<HashtagController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HashtagController(ILogger<HashtagController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddHashtagToPost")]
        [Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> PostHashtagAsync(List<string> hashtags, int postId)
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
                post.Hashtags = new List<string>();
                post.Hashtags = hashtags;
                _unitOfWork.Post.UpdateOneAsync(post);
                return Ok("Hashtag Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("GetHashtagsOfPost")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> GetTagsOfPostAsync(int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                var hashtags = post.Hashtags;
                if (hashtags.IsNullOrEmpty())
                    return Ok("[]");
                return Ok(hashtags);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetPostFromHashtags")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_MODERATOR}")]
        public async Task<IActionResult> GetPostWhereTaggedAsync(string hashtag)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var posts = (await _unitOfWork.Post.GetAllAsync()).Where(p => p.Hashtags.IsNullOrEmpty() ? false : p.Hashtags.ConvertAll(d => d.ToLower()).Contains(hashtag.ToLower())).ToList();
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
