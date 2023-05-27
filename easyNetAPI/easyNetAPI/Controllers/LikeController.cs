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
    public class LikeController : Controller
    {
        private readonly ILogger<LikeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public LikeController(ILogger<LikeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("PostLike")]
        [Authorize(Roles = SD.ROLE_USER)]
        public async Task<IActionResult> PostLikeAsync(int postId)
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
                bool alreadyLiked = false;
                if (post.Likes is null)
                    post.Likes = new List<string>();
                alreadyLiked = post.Likes.Contains(userId);
                if (alreadyLiked)
                {
                    post.Likes.Remove(userId);
                    _unitOfWork.Post.UpdateOneAsync(post);
                    return Ok("Like removed succesfully");
                }
                post.Likes.Add(userId);
                _unitOfWork.Post.UpdateOneAsync(post);
                return Ok("Like Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("LikedPosts")]
        [Authorize(Roles = SD.ROLE_USER)]
        public async Task<IActionResult> LikedPostsAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var posts = await _unitOfWork.Post.GetAllAsync();
                var likedPosts = new List<Post>();
                foreach (var item in posts)
                {
                    if (item.Likes.Contains(userId))
                        likedPosts.Add(item);
                }
                return Json(likedPosts);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
