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
        public Task<bool> AddAsync(UserBehavior user);
        /// <summary>
        /// updates one or more UserBehavior objects from DB.
        /// param:"users" dictionary of all the users to update key: userId value: updated user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public Task<bool> UpdateManyAsync(Dictionary<string, UserBehavior> users);
        public Task<bool> UpdateOneAsync(string userId, UserBehavior user);
        public Task<bool> RemoveAsync(string userId);
        public Task<bool> RemoveUserActivityAsync(UserBehavior userToDelete, IUnitOfWork _unitOfWork);
    }
}