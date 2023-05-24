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
        public Task<List<UserBehavior>> GetAllAsync();
        public Task<UserBehavior?> GetFirstOrDefault(string userId);
        public Task AddAsync(UserBehavior user);
        public Task RemoveAsync(string userId);
    }
}