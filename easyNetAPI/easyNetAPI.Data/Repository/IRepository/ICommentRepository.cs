using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface ICommentRepository
    {

        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetFirstOrDefault(int commentId);
        public Task AddAsync(Comment comment, int postId);
        public Task UpdateOneAsync(int commentId, Comment comment, int postId);
        public Task UpdateManyAsync(Dictionary<int, Comment> comments, int postId);
        public Task RemoveAsync(int postId, int commentId);
    }
}