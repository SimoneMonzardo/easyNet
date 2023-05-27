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
        public async Task<IActionResult> PostLikeAsync(int postId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                token = token.Remove(0, 7);
                var principal = await AuthControllerUtility.DecodeJWTToken(token);
                var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
                if (userId == null)
                    return BadRequest("Not Logged in");
                var post = _unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId);
                if (_unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId).Likes.Contains(userId))
                {
                    _unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId).Likes.Remove(userId);
                    _unitOfWork.Save();
                    return Ok("User already liked this post so like was removed");
                }
                _unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId).Likes.Add(userId);
                _unitOfWork.Save();
                return Ok("Like Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        //[HttpDelete("DeleteLike")]
        //public async Task<IActionResult> DeleteLikeAsync(int postId)
        //{
        //    try
        //    {
        //        var token = Request.Headers["Authorization"];
        //        var utente = await GetUserIdFromJWTToken(token); if (utente == null)
        //            return BadRequest("Not Logged in");
        //        var post = _unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId);
        //        if (!post.Likes.Contains(utente))
        //            return BadRequest("User never liked this post");
        //        post.Likes.Remove(utente);
        //        _unitOfWork.Save();
        //        return Ok("Like Added Succesfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Something went wrong");
        //    }
        //}
        [HttpGet("LikedPosts")]
        public async Task<IActionResult> LikedPostsAsync()
        {
            try
            {
                var token = Request.Headers["Authorization"];
                var utente = await GetUserIdFromJWTToken(token);
                if (utente == null)
                    return BadRequest("Not Logged in");
                var posts = _unitOfWork.Post.GetAll();
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
