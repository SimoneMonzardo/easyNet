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
    public IEnumerable<Post> Get(string? id)
    {
        var posts = _unitOfWork.Post.GetAll().Where(post => post.UserId == id);
        return posts;
    }



    //get di tutti i post
    [HttpGet("GetPost"), Authorize(Roles = SD.ROLE_USER)]
    public IEnumerable<Post> GetAll()
    {
        var posts = _unitOfWork.Post.GetAll();
        return posts;
    }



    [HttpDelete("RemoveAPost"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<ActionResult<string>> Delete(int Id)
    {
        try
        {
            var post = _unitOfWork.Post.GetFirstOrDefault(p => p.PostId == Id);
            if (post == null)
                return BadRequest("Post not found");
            _unitOfWork.Post.Remove(post);
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
            var oldPost = _unitOfWork.Post.GetFirstOrDefault(c => c.PostId == post.PostId);
            if (oldPost == null)
            {
                var token = Request.Headers["Authorization"].ToString();
                token = token.Remove(0, 7);
                var principal = await AuthControllerUtility.DecodeJWTToken(token);
                var userId = principal.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" && c.Value.Contains("-")).Value;
                post.UserId = userId;
                post.Username = _db.Users.FirstOrDefault(u => u.Id == userId).UserName;
                _unitOfWork.Post.Add(post);
                return Ok("Post created succesfully");
            }
            else
            {
                post.Username = oldPost.Username;
                post.UserId = oldPost.UserId;
                post.Likes = oldPost.Likes;
                post.Content = oldPost.Content;
                post.Hastags = oldPost.Hastags;
                post.Tags = oldPost.Tags;



                _unitOfWork.Post.Update(post);
                return Ok("Post modified succesfully");
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exception: " + ex.Message);
        }
    }
}