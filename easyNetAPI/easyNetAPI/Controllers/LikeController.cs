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
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
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
                var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (user is null)
                    return BadRequest("User not found");
                if (alreadyLiked)
                {
                    post.Likes.Remove(userId);
                    await _unitOfWork.Post.UpdateOneAsync(post);
                    if (user.LikedPost is null)
                        user.LikedPost = new List<int>();
                    if (user.LikedPost.Contains(postId))
                        user.LikedPost.Remove(postId);
                    await _unitOfWork.UserBehavior.UpdateOneAsync(userId, user);
                    return Ok("Like removed succesfully");
                }
                post.Likes.Add(userId);
                await _unitOfWork.Post.UpdateOneAsync(post);
                if (user.LikedPost is null)
                    user.LikedPost = new List<int>();
                if (!user.LikedPost.Contains(postId))
                    user.LikedPost.Add(postId);
                await _unitOfWork.UserBehavior.UpdateOneAsync(userId, user);
                return Ok("Like Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("GetLikedPosts")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
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

        [HttpPost("CommentLike")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> CommentLikeAsync(int commentId, int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var comment = await _unitOfWork.Comment.GetFirstOrDefault(commentId);
                if (comment == null)
                    return BadRequest("Comment doesn't exist");
                bool alreadyLiked = false;
                if (comment.Likes is null)
                    comment.Likes = new List<string>();
                alreadyLiked = comment.Likes.Contains(userId);
                if (alreadyLiked)
                {
                    comment.Likes.Remove(userId);
                    var result = await _unitOfWork.Comment.UpdateOneAsync(comment, postId);
                    if (result)
                    {
                        return Ok("Like removed succesfully");
                    }
                }
                comment.Likes.Add(userId);
                var result1 = await _unitOfWork.Comment.UpdateOneAsync(comment, postId);
                if (result1)
                {
                    return Ok("Like Added Succesfully");
                }
                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetLikedComments")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> LikedCommentsAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var comments = await _unitOfWork.Comment.GetAllAsync();
                var likedComments = new List<Comment>();
                foreach (var item in comments)
                {
                    if (item.Likes.Contains(userId))
                        likedComments.Add(item);
                }
                return Json(likedComments);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("ReplyLike")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> ReplyLikeAsync(int replyId,int commentId, int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var reply = await _unitOfWork.Reply.GetFirstOrDefault(replyId);
                if (reply == null)
                    return BadRequest("Reply doesn't exist");
                bool alreadyLiked = false;
                if (reply.Likes is null)
                    reply.Likes = new List<string>();
                alreadyLiked = reply.Likes.Contains(userId);
                if (alreadyLiked)
                {
                    reply.Likes.Remove(userId);
                    var result = await _unitOfWork.Reply.UpdateOneAsync(reply,commentId, postId);
                    if (result)
                    {
                        return Ok("Like removed succesfully");
                    }
                }
                reply.Likes.Add(userId);
                var result1 = await _unitOfWork.Reply.UpdateOneAsync(reply,commentId, postId);
                if (result1)
                {
                    return Ok("Like Added Succesfully");
                }
                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetLikedReplies")]
        [Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<IActionResult> LikedRepliesAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId == null)
                    return BadRequest("Not Logged in");
                var replies = await _unitOfWork.Reply.GetAllAsync();
                var likedReplies = new List<Reply>();
                foreach (var item in replies)
                {
                    if (item.Likes.Contains(userId))
                        likedReplies.Add(item);
                }
                return Json(likedReplies);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
