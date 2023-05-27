using Microsoft.AspNetCore.Mvc;
using System;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using easyNetAPI.Utility;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Data;

namespace easyNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    public IUnitOfWork _unitOfWork;
    private readonly AppDbContext _db;
    public PostController(ILogger<PostController> logger, IUnitOfWork unitOfWork, AppDbContext db)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _db = db;
    }

    //get di un post tramite il suo id
    [HttpGet("GetPostOfAAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<IEnumerable<Post>> GetAsync(string? id)
    {
        var posts = (await _unitOfWork.Post.GetAllAsync()).Where(post => post.UserId == id);
        return posts;
    }

    //get di tutti i post
    [HttpGet("GetPost"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        var posts = await _unitOfWork.Post.GetAllAsync();
        return posts;
    }

    [HttpDelete("RemoveAPost"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<ActionResult<string>> Delete(int postId, string userId)
    {
        try
        {
            var post = _unitOfWork.Post.GetFirstOrDefault(postId);
            if (post == null)
                return BadRequest("Post not found");
            await _unitOfWork.Post.RemoveAsync(postId, userId);
            return Ok("Post removed succesfully");
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exeption: " + ex.Message);
        }
    }

    [HttpPost("UpsertPostOfAuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<ActionResult<string>> UpsertAsync(Post post)
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
            var oldPost = await _unitOfWork.Post.GetFirstOrDefault(post.PostId);
            if (oldPost == null)
            {
                post.UserId = userId;
                post.Username = _db.Users.FirstOrDefault(u => u.Id == userId).UserName;
                await _unitOfWork.Post.AddAsync(post, userId);
                return Ok("Post created succesfully");
            }
            if (oldPost.UserId == userId)
                return BadRequest("userid not authorized to update post");

            post.Username = oldPost.Username;
            post.UserId = oldPost.UserId;
            post.Likes = oldPost.Likes;
            post.Content = oldPost.Content;
            post.Tags = oldPost.Tags;
            await _unitOfWork.Post.UpdateOneAsync(post.PostId, post, post.UserId);
            return Ok("Post modified succesfully");
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exception: " + ex.Message);
        }
    }
}