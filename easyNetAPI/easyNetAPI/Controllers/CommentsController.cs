using Microsoft.AspNetCore.Mvc;
using System;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using easyNetAPI.Utility;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;

namespace easyNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private static IMongoCollection<Post> _postsCollection;
    public CommentsController(ILogger<CommentsController> logger)
    {
        _logger = logger;
    }
    [HttpGet("GetCommentsOfAPostAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public IEnumerable<Comments> Get(int Id)
    {
        var post = _postsCollection.Find(p => p.Id == Id).First();
        return post.Comments;
    }
    [HttpPost("UpsertCommentOfAPostAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task UpsertAsync(Comments comment, Post post)
    {
        if (ModelState.IsValid)
        {
            var p = _postsCollection.Find(p => p.Id == post.Id).First();
            if (p != null)
            {
                var oldComment = p.Comments.Find(c => c.CommentId == comment.CommentId);
                if (oldComment == null)
                {
                    comment.CommentId = 0;
                    string userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    comment.UserName = userName;
                    comment.UserId = _unitOfWork.IdentityUser.GetFirstOrDefault(i => i.UserName == userName).Id;
                    p.Comments.Add(comment);
                    var filter = Builders<Post>.Filter.Eq(po => po.Id, post.Id);
                    var update = Builders<Post>.Update.Set(po => po.Comments, p.Comments);
                    await _postsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    string userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    comment.UserName = userName;
                    comment.UserId = _unitOfWork.IdentityUser.GetFirstOrDefault(i => i.UserName == userName).Id;
                    comment.Answers = oldComment.Answers;
                    p.Comments.Remove(oldComment);
                    p.Comments.Add(comment);
                    var filter = Builders<Post>.Filter.Eq(po => po.Id, post.Id);
                    var update = Builders<Post>.Update.Set(po => po.Comments, p.Comments);
                    await _postsCollection.UpdateOneAsync(filter, update);
                }
            }
        }
    }
};


