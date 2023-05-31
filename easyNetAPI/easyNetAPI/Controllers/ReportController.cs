using System;
using System.Data;
using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        public IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;

        public ReportController(ILogger<ReportController> logger, IUnitOfWork unitOfWork, AppDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("ReportPost"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_USER},{SD.ROLE_COMPANY_ADMIN}")]
        public async Task<ActionResult<string>> UpsertReportPostAsync(int postId)
        {
            try
            {
                bool added = true;
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (userId is null)
                {
                    return BadRequest("User not found");
                }
                var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
                if (userBehavior is null)
                {
                    return BadRequest("User not found");
                }
                var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
                if (post is null)
                {
                    return BadRequest("Post not found");
                }
                if (userBehavior.ReportedPost.Contains(postId))
                {
                    userBehavior.ReportedPost.Remove(postId);
                    added = false;
                }
                else
                {
                    userBehavior.ReportedPost.Add(postId);
                    added = true;
                }
                var result = await _unitOfWork.UserBehavior.UpdateOneAsync(userId, userBehavior);
                if (result)
                {
                    if (added)
                    {
                        return Ok("Post reported successfully");
                    }
                    return Ok("Post reporting canceled successfully");
                }
                return BadRequest("Something went wrong");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("GetReportedPosts"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<List<Post>?>> GetReportedPostsAsync()
        {
            var users = _unitOfWork.UserBehavior.GetAllAsync().Result;
            List<int> reportedIds = new List<int>();
            foreach (var item in users)
            {
                if (item.ReportedPost.Count() != 0)
                {
                    foreach (var id in item.ReportedPost)
                    {
                        if (reportedIds.Count() != 0)
                        {
                            if (!reportedIds.Contains(id))
                            {
                                reportedIds.Add(id);
                            }
                        }
                        else
                        {
                            reportedIds.Add(id);
                        }
                    }
                }
            }
            List<Post> postsList = new List<Post>();
            foreach (var item in reportedIds)
            {
                var post = await _unitOfWork.Post.GetFirstOrDefault(item);
                if (post != null)
                {
                    postsList.Add(post);
                }
            }
            return Ok(postsList);
        }
    }
}

