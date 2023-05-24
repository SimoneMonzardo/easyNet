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
    public async Task<IEnumerable<Comment>?> GetAsync(int Id)
    {
        var post = await _unitOfWork.Post.GetPostAsync(Id);
        if (post is not null)
        {
            var comment = await _unitOfWork.Comment.GetAllOfPostAsync(post);
            if (comment.Count() != 0)
            {
                return comment;
            }
        }
        return null;
    }
    //[HttpPost("UpsertCommentOfAPostAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    //public async Task<ActionResult<string>> UpsertAsync(Comment comment)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest("Model is not valid");
    //    }
    //    try
    //    {
    //        var oldComment = _unitOfWork.Comment.GetFirstOrDefault(c => c.CommentId == comment.CommentId);
    //        if (oldComment == null)
    //        {
    //            var token = Request.Headers["Authorization"].ToString();
    //            token = token.Remove(0, 7);
    //            var principal = await AuthControllerUtility.DecodeJWTToken(token);
    //            var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
    //            comment.UserId = userId;
    //            comment.Username = _db.Users.FirstOrDefault(u => u.Id == userId).UserName;
    //            _unitOfWork.Comment.Add(comment);
    //            return Ok("Comment created succesfully");
    //        }
    //        else
    //        {
    //            comment.Username = oldComment.Username;
    //            comment.UserId = oldComment.UserId;
    //            comment.Like = oldComment.Like;
    //            comment.Replies = oldComment.Replies;
    //            _unitOfWork.Comment.Update(comment);
    //            return Ok("Comment modified succesfully");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest("Unandled exception: " + ex.Message);
    //    }
    //}
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


