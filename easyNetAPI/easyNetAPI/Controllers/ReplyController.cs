using System;
using System.Data;
using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using easyNetAPI.Models.UpsertModels;
using System.ComponentModel.Design;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplyController: ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        public IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;

        public ReplyController(ILogger<PostController> logger, IUnitOfWork unitOfWork, AppDbContext db)
		{
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
        }

        [HttpPost("UpsertReply"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<ActionResult<string>> UpsertAsync([FromBody]UpsertReply reply)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                if (reply.ReplyId == 0)
                {
                    var newReply = new Reply()
                    {
                        ReplyId = await IdAutoincrementService.GetReplyAutoincrementId(_unitOfWork),
                        UserId = userId,
                        Username = _db.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefault(),
                        Content = reply.Content,
                        Likes = new List<string>()
                    };
                    await _unitOfWork.Reply.AddAsync(newReply, reply.CommentId, reply.PostId);
                    return Ok("Reply created succesfully");
                }
                else
                {
                    var result = await _unitOfWork.Reply.UpdateContentAsync(reply);
                    if (result)
                    {
                        return Ok("Reply modified succesfully");
                    }
                    else
                    {
                        return BadRequest("Reply not found");       
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Unandled exception: " + ex.Message);
            }
        }

        [HttpGet("GetRepliesOfAComment"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<List<Reply>?> GetRepliesAsync (int commentId)
        {
            var comment = await _unitOfWork.Comment.GetFirstOrDefault(commentId);
            if (comment is null)
            {
                return null;
            }
            return comment.Replies;
        }

        [HttpGet("GetReply"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<Reply?> GetReplyAsync(int replyId)
        {
            var reply = await _unitOfWork.Reply.GetFirstOrDefault(replyId);
            if (reply is null)
            {
                return null;
            }
            return reply;
        }

        [HttpDelete("DeleteReply"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
        public async Task<ActionResult<string>> DeleteReplyAsync(int replyId)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
                var reply = await _unitOfWork.Reply.GetFirstOrDefault(replyId);
                if (reply is null)
                {
                    return BadRequest("Reply not found");
                }
                if (reply.UserId != userId)
                {
                    return Forbid("Can't delete comment");
                }
                var commentsList = await _unitOfWork.Comment.GetAllAsync();
                if (commentsList.Count() == 0)
                {
                    return BadRequest("Reply not found");
                }
                var comment = commentsList.Where(c => c.Replies.Select(r => r.ReplyId).ToList().Contains(replyId)).FirstOrDefault();
                if (comment is null)
                {
                    return BadRequest("Reply not found");
                }
                var postsList = await _unitOfWork.Post.GetAllAsync();
                if (postsList.Count() == 0)
                {
                    return BadRequest("Comment not found");
                }
                var post = postsList.Where(p => p.Comments.Select(c => c.CommentId).ToList().Contains(comment.CommentId)).FirstOrDefault();
                if (post is null)
                {
                    return BadRequest("Comment not found");
                }
                var result = await _unitOfWork.Reply.RemoveAsync(replyId, comment.CommentId, post.PostId);
                if (result)
                {
                    return Ok("Reply deleted succesfully");
                }
                return BadRequest("Comment not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Unandled exeption: " + ex.Message);
            }
        }
    }
}

