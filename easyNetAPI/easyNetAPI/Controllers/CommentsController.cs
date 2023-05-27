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

    [HttpGet("GetCommentsOfAPostAuthUser"), Authorize(Roles = SD.ROLE_USER)]
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

    //fatto per provare l'add su commentRepository
    //[HttpPost("AddComment"), Authorize(Roles = SD.ROLE_USER)]
    //public async Task<string> AddCommentAsync(UpsertComment upsertComment)
    //{
    //    if (!ModelState.IsValid ||upsertComment is null)
    //        return "comment model is not valid or comment is null";
    //    var token = Request.Headers["Authorization"].ToString();
    //    token = token.Remove(0, 7);
    //    var principal = await AuthControllerUtility.DecodeJWTToken(token);
    //    var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
    //    if (userId == null)
    //    {
    //        return "Not Logged in";
    //    }
    //    var userName = _db.Users.Where(u => u.Id.Equals(userId)).Select(u => u.UserName).FirstOrDefault();
    //    var comment = new Comment() { Content = upsertComment.Content, UserId= userId, Username = userName, Likes = new List<string>(), Replies = new List<Reply>() };
    //    await _unitOfWork.Comment.AddAsync(comment, upsertComment.PostId);
    //    return "comment added";
    //}

    [HttpPost("UpsertComment"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<ActionResult<string>> UpsertAsync(UpsertComment comment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is not valid");
        }
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Remove(0, 7);
            var principal = await AuthControllerUtility.DecodeJWTToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
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

    //[HttpDelete("RemoveAComment"), Authorize(Roles = SD.ROLE_USER)]
    //public async Task<ActionResult<string>> Delete(int Id)
    //{
    //    try
    //    {
    //        var comment = _unitOfWork.Comment.GetFirstOrDefault(p => p.CommentId == Id);
    //        if (comment == null)
    //            return BadRequest("Comment not found");
    //        _unitOfWork.Comment.Remove(comment);
    //        return Ok("Comment removed succesfully");
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest("Unandled exeption: " + ex.Message);
    //    }

    //}
};


