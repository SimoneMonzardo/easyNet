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
    public string[]? Likes { get; set; }
    public string[]? Hastags { get; set; }
    public string[]? Tags { get; set; }
}