using System;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace easyNetAPI.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        //to get username from token
        //var utente = AuthControllerUtility.DecodeJWTToken("stringa").Result.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "name");
    }
}