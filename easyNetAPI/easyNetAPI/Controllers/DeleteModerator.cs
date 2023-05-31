using System;
using easyNetAPI.Data;
using easyNetAPI.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using easyNetAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace easyNetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteModerator : ControllerBase
    {
        private readonly ILogger<DeleteModerator> _logger;
        private IUnitOfWork _unitOfWork;
        private readonly AppDbContext _db;

        public DeleteModerator(ILogger<DeleteModerator> logger, AppDbContext db, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _db = db;
        }

        [HttpDelete("DeletePost"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> DeletePost(int postId)
        {
            var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
            if (post is null)
            {
                return BadRequest("Post not found");
            }
            var result = await _unitOfWork.Post.RemoveAsync(post.PostId, post.UserId);
            if (result)
            {
                var userBehavior = await _unitOfWork.UserBehavior.GetFirstOrDefault(post.UserId);
                await MongoDbAlignment.RemovePostDataAsync(post.PostId, userBehavior, _unitOfWork);
                return Ok("Post deleted successfully");
            }
            return BadRequest("Couldn't delete post");
        }

        [HttpDelete("DeleteComment"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> DeleteComment(int commentId)
        {
            var comment = await _unitOfWork.Comment.GetFirstOrDefault(commentId);
            if (comment is null)
            {
                return BadRequest("Comment not found");
            }
            var post = _unitOfWork.Post.GetAllAsync().Result.ToList().Where(p => p.Comments.Select(c => c.CommentId).Contains(commentId)).FirstOrDefault();
            if (post is null)
            {
                return BadRequest("Post not found");
            }
            var result = await _unitOfWork.Comment.RemoveAsync(post.PostId, commentId);
            if (result)
            {
                return Ok("Comment deleted successfully");
            }
            return BadRequest("Couldn't delete post");
        }

        [HttpDelete("DeleteReply"), Authorize(Roles = SD.ROLE_MODERATOR)]
        public async Task<ActionResult<string>> DeleteReply(int replyId)
        {
            var reply = await _unitOfWork.Reply.GetFirstOrDefault(replyId);
            if (reply is null)
            {
                return BadRequest("Reply not found");
            }
            var comment = _unitOfWork.Comment.GetAllAsync().Result.Where(c => c.Replies.Select(r => r.ReplyId).Contains(replyId)).FirstOrDefault();
            if (comment is null)
            {
                return BadRequest("Comment not found");
            }
            var post = _unitOfWork.Post.GetAllAsync().Result.ToList().Where(p => p.Comments.Select(c => c.CommentId).Contains(comment.CommentId)).FirstOrDefault();
            if (post is null)
            {
                return BadRequest("Post not found");
            }
            var result = await _unitOfWork.Reply.RemoveAsync(replyId, comment.CommentId, post.PostId);
            if (result)
            {
                return Ok("Reply deleted successfully");
            }
            return BadRequest("Couldn't delete post");
        }
    }
}

