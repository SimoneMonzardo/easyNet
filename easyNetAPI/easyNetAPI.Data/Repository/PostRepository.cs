using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
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

        public async Task<List<String>>? GetLikesOfPost(int postId)
        {
            var post = await GetPostAsync(postId);
            return post.Likes.ToList();
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