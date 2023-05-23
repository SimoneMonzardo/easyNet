//using easyNetAPI.Models;
//using easyNetAPI.Utility;
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
        private static IMongoCollection<Like> _likesCollection;
        private static IMongoCollection<Post> _postCollection;
        public LikeController(ILogger<LikeController> logger)
        {
            _logger = logger;
        }
        [HttpPost("PostLike"), Authorize(Roles = SD.ROLE_USER)]
        public Like PostLike(int postId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var post = _postCollection.Find(post => post.Id == postId).First();
            post.Likes.Add(new Like
            {
                UserId = userId,
            });
            return new Like();
        }
    }
}
