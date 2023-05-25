using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
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
            _usersCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync().Result;
        public async Task AddAsync(UserBehavior user)
        {
            user._id = null;
            await _usersCollection.InsertOneAsync(user);
        }
        
        public async Task UpdateManyAsync(Dictionary<string, UserBehavior> users)
        {
            foreach (var user in users){
                var filter = Builders<UserBehavior>.Filter.Eq(x => x.UserId, user.Key);
                var update = Builders<UserBehavior>.Update
            .Set(x => x.UserId, user.Value.UserId)
            .Set(x => x.Administrator, user.Value.Administrator)
            .Set(x => x.Description, user.Value.Description)
            .Set(x => x.Company, user.Value.Company)
            .Set(x => x.Posts, user.Value.Posts)
            .Set(x => x.FollowedUsers, user.Value.FollowedUsers)
            .Set(x => x.FollowedList, user.Value.FollowedList)
            .Set(x => x.LikedPost, user.Value.LikedPost)
            .Set(x => x.SavedPost, user.Value.SavedPost)
            .Set(x => x.MentionedPost, user.Value.MentionedPost);
                _usersCollection.UpdateOne(filter, update);
            }
        }
        public async Task UpdateOneAsync(string userId, UserBehavior user)
        {
     
                var filter = Builders<UserBehavior>.Filter.Eq(x => x.UserId, userId);
                var update = Builders<UserBehavior>.Update
            .Set(x => x.UserId, user.UserId)
            .Set(x => x.Administrator, user.Administrator)
            .Set(x => x.Description, user.Description)
            .Set(x => x.Company, user.Company)
            .Set(x => x.Posts, user.Posts)
            .Set(x => x.FollowedUsers, user.FollowedUsers)
            .Set(x => x.FollowedList, user.FollowedList)
            .Set(x => x.LikedPost, user.LikedPost)
            .Set(x => x.SavedPost, user.SavedPost)
            .Set(x => x.MentionedPost, user.MentionedPost);
                _usersCollection.UpdateOne(filter, update);

        }

        public async Task RemoveAsync(string userId) =>
        await _usersCollection.DeleteOneAsync(x => x.UserId == userId);
        
        
    }

}