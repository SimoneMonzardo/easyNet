using easyNetAPI.Models;
using easyNetAPI.Models.UpsertModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IPostRepository
    {
        public Task<List<Post>> GetAllAsync();
        public Task<Post?> GetFirstOrDefault(int postId);
        public Task<bool> AddAsync(Post post);
        public Task<bool> UpdateOneAsync(Post post);
        public Task<bool> UpdateManyAsync(Dictionary<int, Post> posts, string userId);
        public Task<bool> RemoveAsync(int postId, string userId);
        public Task<bool> UpdatePostContentAsync(UpsertPost post, string userId);
    }
}