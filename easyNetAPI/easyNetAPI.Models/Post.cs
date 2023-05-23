using easyNetAPI.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

public class Post
{
    [JsonProperty("buttonName")]
    public int PostId { get; set; }
    public Comment[]? Comments { get; set; }
    [JsonProperty("user_id")]
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public string? Content { get; set; }
    
    //UserId
    public string[]? Likes { get; set; }

    //Keyword hashtag
    public string[]? Hastags { get; set; }

    //UserId mention
    public string[]? Tags { get; set; }
}