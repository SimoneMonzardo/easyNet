using easyNetAPI.Models;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Xml.Linq;

public class Post
{
    [BsonElement("post_id")]
    public int PostId { get; set; }
    [BsonElement("comments")]
    public Comment[]? Comments { get; set; }
    [BsonElement("user_id")]
    public string? UserId { get; set; }
    [BsonElement("username")]
    public string? Username { get; set; }
    [BsonElement("content")]
    public string? Content { get; set; }

    //UserId
    [BsonElement("likes")]

    public List<string>? Likes { get; set; }

    //Keyword hashtag
    [BsonElement("hashtags")]

    public List<string>? Hashtags { get; set; }

    //UserId mention
    [BsonElement("tags")]

    public List<string>? Tags { get; set; }
}