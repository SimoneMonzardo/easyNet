using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IUserBehaviorRepository
    {
        public Task<List<UserBehavior>> GetAllAsync();
        public Task<UserBehavior?> GetFirstOrDefault(string userId);
        public Task AddAsync(UserBehavior user);
        /// <summary>
        /// updates one or more UserBehavior objects from DB.
        /// param:"users" dictionary of all the users to update key: userId value: updated user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Task UpdateAsync(Dictionary<string, UserBehavior> users);
        public Task RemoveAsync(string userId);
    }
}