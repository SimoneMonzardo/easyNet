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
                var post = _unitOfWork.Post.GetFirstOrDefault(post => post.Id == postId);
                post.Likes.Add(utente);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
