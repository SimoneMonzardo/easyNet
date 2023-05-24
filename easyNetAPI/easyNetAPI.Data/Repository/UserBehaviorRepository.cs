using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace easyNetAPI.Data.Repository
{
    public class UserBehaviorRepository : IUserBehaviorRepository
    {
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public UserBehaviorRepository(IMongoCollection<UserBehavior> usersCollection)
        {
            _usersCollection = usersCollection;
        }




        public async Task<List<UserBehavior>> GetAllAsync() =>
         await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<UserBehavior?> GetFirstOrDefault(string userId) =>
        await _usersCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        public async Task AddAsync(UserBehavior user) =>
        await _usersCollection.InsertOneAsync(user);
        public async Task UpdateAsync(Dictionary<string, UserBehavior> users)
        {
            foreach (var user in users){
                var filter = Builders<UserBehavior>.Filter.Eq(x => x.UserId, user.Key);
                var update = Builders<UserBehavior>.Update.Set(x => x, user.Value);
                _usersCollection.UpdateOne(filter, update);
            }
        }

        public async Task RemoveAsync(string userId) =>
        await _usersCollection.DeleteOneAsync(x => x.UserId == userId);
        
        
    }

}