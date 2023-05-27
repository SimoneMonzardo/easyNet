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
        public Task<bool> AddAsync(Reply reply, int commentId, int postId);
        public Task<bool> UpdateOneAsync(Reply reply, int commentId, int postId);
        public Task<bool> UpdateManyAsync(Dictionary<int, Reply> replies, int commentId, int postId);
        public Task<bool> RemoveAsync(int replyId, int commentId, int postId);
    }
}