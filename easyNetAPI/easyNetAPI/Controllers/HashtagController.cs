using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Data;
using System.Security.Claims;

namespace easyNetAPI.Controllers
{
    public class HashtagController : Controller
    {
        private readonly ILogger<HashtagController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HashtagController(ILogger<HashtagController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("PostHashtag")]
        [Authorize(Roles = SD.ROLE_USER)]
        public async Task<IActionResult> PostHashtagAsync(int postId)
        {
            /*
             Questo metodo fa sia la post che la delete 
             in base a se hai o no lasciato like al post
             */
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
                post.Hashtags.Add(userId);
                _unitOfWork.Post.UpdateOneAsync(post);
                return Ok("Hashtag Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("HashtaggedPosts")]
        [Authorize(Roles = SD.ROLE_USER)]
        public async Task<IActionResult> HashtaggedPostsAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var posts = await _unitOfWork.Post.GetAllAsync();
                var hashtaggedPosts = new List<Post>();
                foreach (var item in posts)
                {
                    if (item.Hashtags.Contains(userId))
                        hashtaggedPosts.Add(item);
                }
                return Json(hashtaggedPosts);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
