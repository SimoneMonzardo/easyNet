using Microsoft.AspNetCore.Mvc;
using System;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

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
    [HttpGet("GetCommentsOfAPostAuthUser"), Authorize(Roles = ROLE_USER)]
    public IEnumerable<Comments> Get(int Id)
    {
        var posts = _postsCollection.Find(p=> p.Id == Id).First();
        return posts.Comments;
    }
};


