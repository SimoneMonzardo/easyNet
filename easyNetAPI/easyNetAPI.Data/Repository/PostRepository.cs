using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly MongoDBService _db;
        public PostRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(Post post)
        {
            var postFromDb = GetFirstOrDefault(p => p.PostId == post.PostId);
            if (postFromDb is not null)
            {
                postFromDb.Content = post.Content;
                postFromDb.Comments = post.Comments;
                postFromDb.Hastags = post.Hastags;
                postFromDb.Likes = post.Likes;
                postFromDb.Hastags = post.Hastags;
            }
        }
    }
}