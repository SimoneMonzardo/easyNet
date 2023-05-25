using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Models.UpsertModels;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class PostRepository
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;

        public PostRepository(UserBehaviorSettings userBehaviorSettings) 
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //prende tutti i posti di tutti gli user
        public async Task<IEnumerable<Post>?> GetAllAsync() {
            var users = await _userBehaviorSettings.GetAllAsync();
            var posts = new List<Post>();
            foreach (var item in users)
            {
                if (item.Posts is not null)
                {
                    foreach (var p in item.Posts)
                    {
                        posts.Add(p);
                    }
                }
            }
            return posts;
        }

        //prendi un post dato il suo id
        public async Task<Post>? GetPostAsync(int postId) {
            var post = (await GetAllAsync()).FirstOrDefault(p => p.PostId == postId);
            return post;
        }

        public async Task<List<string>>? GetLikesOfPost(int postId)
        {
            var post = await GetPostAsync(postId);
            return post.Likes.ToList();
        }

        //aggiungi un nuovo post
        public async Task<string> AddPost(UpsertPost post, string userId, string userName)
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

            if (posts.Count() == 0)
            {
                newPost.PostId = 1;
            }
            else
            {
                newPost.PostId = posts.LastOrDefault().PostId + 1;
            }

            posts.Add(newPost);

            userBehavior.Posts = posts.ToArray();
            await _userBehaviorSettings.UpdateAsync(userId, userBehavior);
            return "Post added successfully";
        }

        public async Task<string> UpdatePostContent(UpsertPost post, string userId)
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
            var postFromDb = posts.Where(p => p.PostId == post.PostId).FirstOrDefault();
            if (postFromDb is null)
            {
                return "Post not found";
            }
            postFromDb.Content = post.Content;
            userBehavior.Posts = posts.ToArray();
            await _userBehaviorSettings.UpdateAsync(userId, userBehavior);
            return "Post updated successfully";
        }

        //fare questo metodo anche per hashtags, likes e tags
        public async Task<string> ManagePostComments(Post post)
        {
            var userBehavior = await _userBehaviorSettings.GetAsync(post.UserId);
            if (userBehavior is null)
            {
                return "User not found";
            }
            var posts = userBehavior.Posts.ToList();
            if (posts.Count() == 0)
            {
                return "Post not found";
            }
            var postFromDb = posts.Where(p => p.PostId == post.PostId).FirstOrDefault();
            if (postFromDb is null)
            {
                return "Post not found";
            }
            postFromDb.Comments = post.Comments;
            userBehavior.Posts = posts.ToArray();
            await _userBehaviorSettings.UpdateAsync(post.UserId, userBehavior);
            return "Comment added successfully";
        }
    }
}