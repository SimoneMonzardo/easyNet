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
using easyNetAPI.Data.Repository.IRepository;
using System.Xml.Linq;
using NuGet.Versioning;
using Amazon.Auth.AccessControlPolicy;
using easyNetAPI.Data;
using easyNetAPI.Data.Repository;
using easyNetAPI.Models.UpsertModels;

namespace easyNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    private IUnitOfWork _unitOfWork;
    private readonly AppDbContext _db;

    public CommentsController(ILogger<CommentsController> logger, AppDbContext db, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _db = db;
    }

    [HttpGet("GetCommentsOfAPost"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_MODERATOR}")]
    public async Task<IEnumerable<Comment>?> GetAsync(int postId)
    {
        var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
        if (post is not null)
        {
            var comments = post.Comments;
            if (comments.Count() != 0)
            {
                return comments;
            }
        }
        return null;
    }

    [HttpPost("UpsertComment"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
    public async Task<ActionResult<string>> UpsertAsync(UpsertComment comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is not valid");
        }
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (comment.CommentId == 0)
            {
                var newComment = new Comment()
                {
                    CommentId = await IdAutoincrementService.GetCommentAutoincrementId(_unitOfWork),
                    UserId = userId,
                    Username = _db.Users.Where(u=>u.Id == userId).Select(u=> u.UserName).FirstOrDefault(),
                    Content = comment.Content,
                    Likes = new List<string>(),
                    Replies = new List<Reply>()
                };
                var result = await _unitOfWork.Comment.AddAsync(newComment, comment.PostId);
                if (result)
                {
                    return Ok("Comment created succesfully");
                }
                return BadRequest("There was an error creating the comment");
            }
            else
            {
                var result = await _unitOfWork.Comment.UpdateContentAsync(comment);
                if (result)
                {
                    return Ok("Comment modified succesfully");
                }
                return BadRequest("There was an error updating the comment");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exception: " + ex.Message);
        }
    }

    [HttpGet("GetComment"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_MODERATOR}")]
    public async Task<Comment?> GetComment (int commentId)
    {
        var comment = await _unitOfWork.Comment.GetFirstOrDefault(commentId);
        if (comment is null)
        {
            return null;
        }
        return comment;
    }

    [HttpDelete("DeleteComment"), Authorize(Roles = $"{SD.ROLE_USER},{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
    public async Task<ActionResult<string>> Delete(int commentId)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            bool result = true;
            var comment = await _unitOfWork.Comment.GetFirstOrDefault(commentId);
            if (comment == null)
            {
                return BadRequest("Comment not found");
            }
            if (comment.UserId != userId)
            {
                return Forbid("Can't delete comment");
            }
            var postsList = await _unitOfWork.Post.GetAllAsync();
            if (postsList.Count() == 0)
            {
                return BadRequest("Comment not found");
            }
            foreach (var post in postsList)
            {
                if (post.Comments.Select(c => c.CommentId).ToList().Contains(commentId))
                {
                   result = await _unitOfWork.Comment.RemoveAsync(post.PostId, comment.CommentId);
                    if (result)
                    {
                        return Ok("Comment removed succesfully");
                    }
                    return BadRequest("Comment not found");
                }
            }
            return BadRequest("Comment not found");
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exeption: " + ex.Message);
        }

    }
};


