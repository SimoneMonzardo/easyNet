using Microsoft.AspNetCore.Mvc;
using System;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;
using easyNetAPI.Utility;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Data;
using easyNetAPI.Models.UpsertModels;
using Microsoft.Extensions.Hosting;

namespace easyNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;
    public IUnitOfWork _unitOfWork;
    private readonly AppDbContext _db;

    public PostController(ILogger<PostController> logger, IUnitOfWork unitOfWork, AppDbContext db, IWebHostEnvironment hostEnvironment)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _db = db;
        _hostEnvironment = hostEnvironment;
    }

    //get di un post tramite il suo id
    [HttpGet("GetPostOfUser_AuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<IEnumerable<Post>> GetAsync(string username)
    {
        var posts = (await _unitOfWork.Post.GetAllAsync()).Where(post => post.Username == username);
        return posts;
    }

    //get di tutti i post
    [HttpGet("GetAllPost_AuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        var posts = await _unitOfWork.Post.GetAllAsync();
        return posts;
    }

    [HttpGet("GetAllPostOfFollowed_AuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<IEnumerable<Post>> GetAllFollowedAsync()
    {
        var token = Request.Headers["Authorization"].ToString();
        var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
        if (userId is null)
            return null;
        var user = await _unitOfWork.UserBehavior.GetFirstOrDefault(userId);
        if (user is null || user.FollowedUsers is null || user.FollowedUsers.Count == 0)
            return null;
        var posts = await _unitOfWork.Post.GetAllAsync();
        var followedPost = new List<Post>();
        foreach (var post in posts)
        {
            if (user.FollowedUsers.Contains(post.UserId))
                followedPost.Add(post);
        }
        return followedPost;
    }

    [HttpDelete("RemovePost_AuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<ActionResult<string>> Delete(int postId)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            if (userId is null)
                return BadRequest("User not found");
            var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
            if (post == null)
                return BadRequest("Post not found");
            if (!post.UserId.Equals(userId))
                return BadRequest("User is not Authorized to remove post");
            await _unitOfWork.Post.RemoveAsync(postId, userId);
            return Ok("Post removed succesfully");
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exeption: " + ex.Message);
        }
    }

    [HttpPost("UpsertPost_AuthUser"), Authorize(Roles = SD.ROLE_USER)]
    public async Task<ActionResult<string>> UpsertAsync(UpsertPost post, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is not valid");
        }

        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null) // se è un post con immagine aggiunge l'immagine e modifica il content di conseguenza
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\post");
                var extension = Path.GetExtension(file.FileName);
                var link = Path.Combine(uploads, fileName + extension);
                using (var fileStreams = new FileStream(link, FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                if (post.PostId == 0)
                {

                    string placeholder = "{0}";
                    string url = "https://localhost/" + link; // da modificare quando verrà hostata la api con il nuovo link
                    var newPost = new Post()
                    {
                        PostId = await IdAutoincrementService.GetPostAutoincrementId(_unitOfWork),
                        Comments = new List<Comment>(),
                        UserId = userId,
                        Username = _db.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefault(),
                        Content = post.Content.Replace(placeholder, url),
                        Likes = new List<string>(),
                        Hashtags = new List<string>(),
                        Tags = new List<string>()
                    };
                    await _unitOfWork.Post.AddAsync(newPost);
                    return Ok("Post created succesfully");
                }
                else
                {
                    var result = await _unitOfWork.Post.UpdatePostContentAsync(post, userId);
                    if (result)
                    {
                        return Ok("Post modified succesfully");
                    }
                    else
                    {
                        return BadRequest("Post not found");
                    }
                }
            }
            if (post.PostId == 0)
            {
                var newPost = new Post()
                {
                    PostId = await IdAutoincrementService.GetPostAutoincrementId(_unitOfWork),
                    Comments = new List<Comment>(),
                    UserId = userId,
                    Username = _db.Users.Where(u => u.Id == userId).Select(u => u.UserName).FirstOrDefault(),
                    Content = post.Content,
                    Likes = new List<string>(),
                    Hashtags = new List<string>(),
                    Tags = new List<string>()
                };
                await _unitOfWork.Post.AddAsync(newPost);
                return Ok("Post created succesfully");
            }
            else
            {
                var result = await _unitOfWork.Post.UpdatePostContentAsync(post, userId);
                if (result)
                {
                    return Ok("Post modified succesfully");
                }
                else
                {
                    return BadRequest("Post not found");
                }
            }

        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exception: " + ex.Message);
        }
    }
}