using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace easyNetAPI.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly IUserBehaviorRepository _users;
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public PostRepository(IMongoCollection<UserBehavior> usersCollection, IUserBehaviorRepository users)
        {
            _usersCollection = usersCollection;
            _users = users;
        }
        private async Task<List<Post>> Query()
        {
            var unwindStage = new BsonDocument("$unwind", new BsonDocument { { "path", "$posts" } });
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
 {"newRoot", "$posts"}
 });
            var pipeline = new[] { unwindStage, replaceRootStage };
            var _botsCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();
            List<Post> posts = new();
            foreach (var bsonDocument in _botsCollection)
            {
                posts.Add(BsonSerializer.Deserialize<Post>(bsonDocument));
            }
            return posts;
        }
        public async Task<List<Post>> GetAllAsync() => await Query();
        public async Task<Post?> GetFirstOrDefault(int postId) => Query().Result.FirstOrDefault(x => x.PostId == postId);
        public async Task AddAsync(Post post, string userId)
        {
            UserBehavior user = _users.GetFirstOrDefault(userId).Result;
            user.Posts.Add(post);
            await _users.UpdateOneAsync(userId, user);
        }
        public async Task RemoveAsync(int postId, string userId)
        {
            Dictionary<string, UserBehavior> dict = new();
            UserBehavior users = _users.GetAllAsync().Result.ToList().Find(x => x.UserId == userId);
            users.Posts.RemoveAll(x => x.PostId == postId);
            dict.Add(userId, users);
            await _users.UpdateManyAsync(dict);
        }
        public async Task UpdateOneAsync(int postId, Post post, string userId)
        {
            var userBehavior = await _userBehaviorSettings.GetAsync(userId);
            if (userBehavior is null)
            {
                return "User not found";
            }

            var posts = userBehavior.Posts.ToList();
            var newPost = new Post {
                Comments = Array.Empty<Comment>(),
                UserId = userId,
                Username = userName,
                Content = post.Content,
                Likes = Array.Empty<string>(),
                Hashtags = Array.Empty<string>(),
                Tags = Array.Empty<string>()
            };

            var allUsersPosts = await GetAllAsync();
            if (allUsersPosts.Count() == 0)
            {
                newPost.PostId = 1;
            }
            else
            {
                var idList = allUsersPosts.Select(i => i.PostId).Order();
                newPost.PostId = idList.LastOrDefault() + 1;
            }

            posts.Add(newPost);

            userBehavior.Posts = posts.ToArray();
            await _userBehaviorSettings.UpdateAsync(userId, userBehavior);
            return "Post added successfully";
        }

        public async Task<string> DeletePost(int postId, string userId)
        {
            var userBehavior = await _userBehaviorSettings.GetAsync(userId);
            if (userBehavior is null)
            {
                return "User not found";
            }
            var posts = userBehavior.Posts.ToList();
            if (posts.Count() == 0)
            {
                return "Post not found";
            }
            var postFromDb = posts.Where(p => p.PostId == postId).FirstOrDefault();
            if (postFromDb is null)
            {
                return "Post not found";
            }
            posts.Remove(postFromDb);
            userBehavior.Posts = posts.ToArray();
            await _userBehaviorSettings.UpdateAsync(userId, userBehavior);
            return "Post deleted successfully";
        }

        public async Task<string> UpdatePostContent(UpsertPost post, string userId)
        {
            UserBehavior user = _users.GetAllAsync().Result.ToList().Find(x => x.UserId == userId);
            Post poste = user.Posts.Find(x => x.PostId == postId);
            poste = post;
            user.Posts.Add(poste);
            await _users.UpdateOneAsync(userId, user);
        }
        public async Task UpdateManyAsync(Dictionary<int, Post> posts, string userId)
        {
            UserBehavior user = _users.GetFirstOrDefault(userId).Result;
            foreach (var post in posts)
            {
                Post _post = user.Posts.Find(x => x.PostId == post.Key);
                _post = post.Value;
            }
            await _users.UpdateOneAsync(userId, user);
        }

        //public void Update(Post post)
        //{
        //    var postFromDb = GetFirstOrDefault(p => p.PostId == post.PostId);
        //    if (postFromDb is not null)
        //    {
        //        postFromDb.Content = post.Content;
        //        postFromDb.Comments = post.Comments;
        //        postFromDb.Hastags = post.Hastags;
        //        postFromDb.Likes = post.Likes;
        //        postFromDb.Hastags = post.Hastags;
        //    }
        //}
    }
}