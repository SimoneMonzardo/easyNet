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

namespace easyNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ILogger<CommentsController> _logger;
    public IUnitOfWork _unitOfWork;
    public CommentsController(ILogger<CommentsController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    [HttpGet("GetCommentsOfAPostAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public IEnumerable<Comment> Get(int Id)
    {
        var post = _unitOfWork.Post.GetFirstOrDefault(p => p.PostId == Id);
        return post.Comments;
    }
    [HttpPost("UpsertCommentOfAPostAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task UpsertAsync(Comment comment)
    {
        if (ModelState.IsValid)
        {
            var oldComment = _unitOfWork.Comment.GetFirstOrDefault(c => c.CommentId == comment.CommentId);
            if (oldComment==null)
            {
                //inserisco userId e username
                var token = Request.Headers["Authorization"];
                var claim = AuthControllerUtility.DecodeJWTToken(token).Result.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "name");
                comment.Username = claim.Value;
                comment.UserId = await GetUserIdFromJWTToken(token);
                _unitOfWork.Comment.Add(comment);
            }
            else
            {
                //prendo i dati del commento 
                comment.Username = oldComment.Username;
                comment.UserId = oldComment.UserId;
                comment.Like= oldComment.Like;
                comment.Replies = oldComment.Replies;
                _unitOfWork.Comment.Update(comment);
            }

        }
    }
    public static async Task<string> GetUserIdFromJWTToken(string token)
    {
        var principal = await AuthControllerUtility.DecodeJWTToken(token);

        // Retrieve the user ID claim
        var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);



        if (userIdClaim != null)
        {
            var userId = userIdClaim.Value;
            return userId;
        }



        // If user ID claim not found
        return null;
    }
};


