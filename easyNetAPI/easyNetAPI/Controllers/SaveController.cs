using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Models.ModelVM;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        private readonly AppDbContext _db;

        public SaveController(ILogger<LikeController> logger, IUnitOfWork unitOfWork, AppDbContext db)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
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
                    return Ok("Save removed succesfully");
                }
                user.SavedPost.Add(postId);
                await _unitOfWork.UserBehavior.UpdateOneAsync(userId, user);
                return Ok("Post saved Succesfully");
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
                List<Post> posts = new List<Post>();
                if (user.SavedPost is null)
                    user.SavedPost = new List<int>();
                foreach (var p in user.SavedPost)
                {
                    var post = await _unitOfWork.Post.GetFirstOrDefault(p);
                    if (post is not null)
                        posts.Add(post);
                }
                var postsList = new List<PostVM>();
                foreach (var post in posts)
                {
                    var profilepic = _db.Users.Where(u => u.Id == post.UserId).FirstOrDefault().ProfilePicture;
                    postsList.Add(new PostVM
                    {
                        Username = post.Username,
                        PostId = post.PostId,
                        Comments = post.Comments,
                        Content = post.Content,
                        DataDiCreazione = post.DataDiCreazione,
                        Hashtags = post.Hashtags,
                        ImgUrl = profilepic,
                        Likes = post.Likes,
                        Tags = post.Tags
                    });
                }
                return Json(postsList.OrderBy(p => p.DataDiCreazione));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

		[HttpGet("GetSavedPostsIds")]
		[Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
		public async Task<IActionResult> GetSavedPostIdsAsync()
		{
			try
			{
				var token = Request.Headers["Authorization"].ToString();
				var userId = string.IsNullOrWhiteSpace(token) 
                    ? null 
                    : await AuthControllerUtility.GetUserIdFromTokenAsync(token);

				if (userId is null)
                {
					return Unauthorized("Not Logged in");
                }

				var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (user is null)
                {
                    return NotFound("User not found");
                }

                user.SavedPost ??= new();
				
				return Json(user.SavedPost);
			}
			catch (Exception)
			{
				return BadRequest("Something went wrong");
			}
		}
	}
}
