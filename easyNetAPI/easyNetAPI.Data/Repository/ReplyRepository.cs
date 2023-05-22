using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class ReplyRepository : Repository<Reply>, IReplyRepository
    {
        private readonly MongoDBService _db;
        public ReplyRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(Reply reply)
        {
            var replyFromDb = GetFirstOrDefault(r => r.CommentId == reply.CommentId && r.UserId == reply.UserId);
            if (replyFromDb is not null)
            {
                replyFromDb.Content = reply.Content;
                replyFromDb.Like = reply.Like;
            }
        }
    }
}