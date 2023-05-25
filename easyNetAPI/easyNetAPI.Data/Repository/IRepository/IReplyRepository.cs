using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IReplyRepository
    {
        public Task<List<Reply>> GetAllAsync();
        public Task<Reply?> GetFirstOrDefault(int replyId);
        public Task AddAsync(Reply reply, int commentId, int postId);
        public Task UpdateOneAsync(int replyId, Reply reply, int commentId, int postId);
        public Task UpdateManyAsync(Dictionary<int, Reply> replies, int commentId, int postId);
        public Task RemoveAsync(int replyId, int commentId, int postId);
    }
}