using easyNetAPI.Models;
using easyNetAPI.Models.UpsertModels;
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
        public Task<bool> AddAsync(Comment comment, int postId);
        public Task<bool> UpdateOneAsync(Comment comment, int postId);
        public Task<bool> UpdateManyAsync(Dictionary<int, Comment> comments, int postId);
        public Task<bool> RemoveAsync(int postId, int commentId);
        public Task<bool> UpdateContentAsync(UpsertComment upsertComment);
        public Task<List<string>>? GetCommentLikes(int commentId);
    }
}