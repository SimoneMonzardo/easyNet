using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly MongoDBService _db;
        public CommentRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(Comment comment)
        {
            var commentFromDb = GetFirstOrDefault(c => c.CommentId == comment.CommentId);
            if (commentFromDb is not null)
            {
                commentFromDb.Content = comment.Content;
                commentFromDb.Like = comment.Like;
                commentFromDb.Replies = comment.Replies;
            }
        }
    }
}