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

        public async Task<bool> AddAsync(UserBehavior user)
        {
            user._id = null;
            try
            {
                await _usersCollection.InsertOneAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<bool> UpdateManyAsync(Dictionary<string, UserBehavior> users)
        {
            try
            {
                foreach (var user in users)
                {
                    var filter = Builders<UserBehavior>.Filter.Eq(x => x.UserId, user.Key);
                    var update = Builders<UserBehavior>.Update
                        .Set(x => x.UserId, user.Value.UserId)
                        .Set(x => x.Administrator, user.Value.Administrator)
                        .Set(x => x.Description, user.Value.Description)
                        .Set(x => x.Company, user.Value.Company)
                        .Set(x => x.Posts, user.Value.Posts)
                        .Set(x => x.FollowedUsers, user.Value.FollowedUsers)
                        .Set(x => x.FollowersList, user.Value.FollowersList)
                        .Set(x => x.LikedPost, user.Value.LikedPost)
                        .Set(x => x.SavedPost, user.Value.SavedPost)
                        .Set(x => x.MentionedPost, user.Value.MentionedPost);
                       _usersCollection.UpdateOne(filter, update);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateOneAsync(string userId, UserBehavior user)
        {
            try
            {
                var filter = Builders<UserBehavior>.Filter.Eq(x => x.UserId, userId);
                var update = Builders<UserBehavior>.Update
                    .Set(x => x.UserId, user.UserId)
                    .Set(x => x.Administrator, user.Administrator)
                    .Set(x => x.Description, user.Description)
                    .Set(x => x.Company, user.Company)
                    .Set(x => x.Posts, user.Posts)
                    .Set(x => x.FollowedUsers, user.FollowedUsers)
                    .Set(x => x.FollowersList, user.FollowersList)
                    .Set(x => x.LikedPost, user.LikedPost)
                    .Set(x => x.SavedPost, user.SavedPost)
                    .Set(x => x.MentionedPost, user.MentionedPost);
                 var result = _usersCollection.UpdateOne(filter, update);
                return result.IsAcknowledged;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(string userId)
        {
            try
            {
                await _usersCollection.DeleteOneAsync(x => x.UserId == userId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveUserActivityAsync(UserBehavior userToDelete, IUnitOfWork _unitOfWork) {
            var users = await GetAllAsync();
            if (users.Count() !=0)
            {
                foreach (var user in users)
                {
                    if (user.FollowersList.Count() != 0)
                    {
                        if (user.FollowersList.Contains(userToDelete.UserId))
                        {
                            user.FollowersList.Remove(userToDelete.UserId);
                        }
                    }
                    if (user.FollowedUsers.Count != 0)
                    {
                        if (user.FollowedUsers.Contains(userToDelete.UserId))
                        {
                            user.FollowedUsers.Remove(userToDelete.UserId);
                        }
                    }
                    var postsListToDelete = userToDelete.Posts.ToList();
                    foreach (var post in postsListToDelete)
                    {
                        await MongoDbAlignment.RemovePostDataAsync(post.PostId, user ,_unitOfWork);
                    }
                    return await UpdateOneAsync(user.UserId, user);
                }
                await MongoDbAlignment.RemoveAllLikesAsync(userToDelete.UserId, _unitOfWork);
                await MongoDbAlignment.RemoveAllTagsAsync(userToDelete.UserId, _unitOfWork);
            }
            return true;
        }
    }
}