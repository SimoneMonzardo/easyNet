using easyNetAPI.Models;
public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<Like> Likes { get; set; }
    public List<Comments> Comments { get; set; }

    //MD format page
    public string Content { get; set; }
    public List<Hashtag> Hashtag { get; set; }
    public List<Mention> Mentions { get; set; }
}