using System;
using System.Text;
using easyNetAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace easyNetAPI.Data
{
    public class UserBehaviorSettings
    {
        public readonly IMongoCollection<UserBehavior> _userBehaviorCollection;

        public UserBehaviorSettings(
            IOptions<MongoDbSettings> mongoDatabaseSettings)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(mongoDatabaseSettings.Value.ConnectionString);

            clientSettings.ReadEncoding = (UTF8Encoding)Encoding.UTF8;
            var mongoClient = new MongoClient(
                mongoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDatabaseSettings.Value.DatabaseName);

            _userBehaviorCollection = mongoDatabase.GetCollection<UserBehavior>(
                mongoDatabaseSettings.Value.CollectionName);
        }

        public async Task<IEnumerable<UserBehavior>> GetAllAsync() =>
             await _userBehaviorCollection.Find(_ => true).ToListAsync();

        public async Task<UserBehavior?> GetAsync(string userId) =>
            await _userBehaviorCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();

        public async Task AddAsync(UserBehavior newUser) =>
            await _userBehaviorCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string userId, UserBehavior updatedUserBehavior) =>
            await _userBehaviorCollection.ReplaceOneAsync(x => x.UserId == userId, updatedUserBehavior);

        public async Task RemoveAsync(string userId) =>
            await _userBehaviorCollection.DeleteOneAsync(x => x.UserId == userId);
    }
}