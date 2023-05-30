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

    [HttpGet("GetPostsOfUser"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
    public async Task<IEnumerable<Post>> GetAsync(string username)
    {
        var posts = (await _unitOfWork.Post.GetAllAsync()).Where(post => post.Username == username);
        return posts;
    }

    //get di tutti i post
    [HttpGet("GetAllPosts"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
    public async Task<IEnumerable<Post>?> GetAllAsync()
    {
        var posts = await _unitOfWork.Post.GetAllAsync();
        return posts;
    }

    //get di un post in base all id
    [HttpGet("GetPostById"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER},{SD.ROLE_MODERATOR}")]
    public async Task<Post?> GetByIdAsync(int postId)
    {
        var post = await _unitOfWork.Post.GetFirstOrDefault(postId);
        return post;
    }

    [HttpGet("GetAllPostsOfFollowed"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN},{SD.ROLE_USER}")]
    public async Task<IEnumerable<Post>?> GetAllFollowedAsync()
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

    [HttpDelete("DeletePost"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
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
            var result = await _unitOfWork.Post.RemoveAsync(postId, userId);
            if (result)
            {
                var usersFromDb = await _unitOfWork.UserBehavior.GetAllAsync();
                if (usersFromDb.Count() != 0)
                {
                    foreach (var user in usersFromDb)
                    {
                        MongoDbAlignment.RemovePostDataAsync(postId, user, _unitOfWork);
                    }
                }
                return Ok("Post removed succesfully");
            }
            return BadRequest("Something went wrong");
        }
        catch (Exception ex)
        {
            return BadRequest("Unandled exeption: " + ex.Message);
        }
    }

    [HttpPost("UploadImage"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
    public async Task<object> PostImage(IFormFile? file)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images/post");
                var extension = Path.GetExtension(file.FileName);
                var link = Path.Combine(uploads, fileName + extension);
                string url = "https://localhost/" + link; // da modificare con il link futuro del sito
                using (var fileStreams = new FileStream(link, FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                return Ok(url);
            }
            return BadRequest("File is null");
        }
        catch(Exception ex)
        {
            return BadRequest("Error " + ex.Message);
        }
    }
    [HttpDelete("DeleteImage"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
    public async Task<object> DeleteImage(string link)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string location = (new Uri(link)).PathAndQuery;
            System.IO.File.Delete(Path.Combine(wwwRootPath, location));
            return Ok("Deleted " + link);
        }
        catch (Exception ex)
        {
            return BadRequest("Error " + ex.Message);
        }
    }

    [HttpPost("UpsertPost"), Authorize(Roles = $"{SD.ROLE_EMPLOYEE},{SD.ROLE_COMPANY_ADMIN}")]
    public async Task<ActionResult<string>> UpsertAsync([FromBody]UpsertPost post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Model is not valid");
        }

        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var userId = await AuthControllerUtility.GetUserIdFromTokenAsync(token);
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