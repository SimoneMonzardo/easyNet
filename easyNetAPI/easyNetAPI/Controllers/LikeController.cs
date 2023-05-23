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
        public IActionResult PostLike(int postId)
        {
            try
            {
                var utente = AuthControllerUtility.DecodeJWTToken("stringa").Result.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "name").Value;
                if (utente == null)
                    return BadRequest("Not Logged in");
                var post = _unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId);
                if (post.Likes.Contains(utente))
                    return BadRequest("User already liked this post");
                post.Likes.Add(utente);
                _unitOfWork.Save();
                return Ok("Like Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpDelete("DeleteLike")]
        public IActionResult DeleteLike(int postId)
        {
            try
            {
                var utente = AuthControllerUtility.DecodeJWTToken("stringa").Result.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "name").Value;
                if (utente == null)
                    return BadRequest("Not Logged in");
                var post = _unitOfWork.Post.GetFirstOrDefault(post => post.PostId == postId);
                if (!post.Likes.Contains(utente))
                    return BadRequest("User never liked this post");
                post.Likes.Remove(utente);
                _unitOfWork.Save();
                return Ok("Like Added Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [HttpGet("LikedPosts")]
        public IActionResult LikedPosts()
        {
            try
            {
                var utente = AuthControllerUtility.DecodeJWTToken("stringa").Result.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "name").Value;
                if(utente == null)
                    return BadRequest("Not Logged in");
                var posts = _unitOfWork.Post.GetAll();
                var likedPosts = new List<Post>();
                foreach (var item in posts)
                {
                    if (item.Likes.Contains(utente))
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
