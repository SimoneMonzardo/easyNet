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
    [ApiController]
    [Route("[controller]")]
    public class SaveController : Controller
    {
        private readonly ILogger<LikeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public SaveController(ILogger<LikeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("PostSave")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> PostSaveAsync(int postId)
        {
            /*
             This method do both add and delete
             in case the post is alredy saved it remove the post
             in case the post isn't already saved it add the post
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
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (user is null)
                    return BadRequest("User not found");
                bool alreadySaved = false;
                if (user.SavedPost is null)
                    user.SavedPost = new List<int>();
                alreadySaved = user.SavedPost.Contains(postId);
                if (alreadySaved)
                {
                    user.SavedPost.Remove(postId);
                    await _unitOfWork.UserBehavior.UpdateOneAsync(userId, user);
                    return Ok("Like removed succesfully");
                }
                user.SavedPost.Add(postId);
                await _unitOfWork.UserBehavior.UpdateOneAsync(userId, user);
                return Ok("Like Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetSavedPosts")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> GetSavedPostsAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (user is null)
                    return BadRequest("User not found");
                var posts = user.SavedPost;
                return Json(posts);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}
