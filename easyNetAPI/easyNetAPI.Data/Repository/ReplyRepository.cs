using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public ReplyRepository(IMongoCollection<UserBehavior> usersCollection)
        {
            _usersCollection = usersCollection;
        }

        public async Task<List<UserBehavior>> GetAllAsync() =>
         await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<UserBehavior?> GetFirstOrDefault(string userId) =>
        await _usersCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        public async Task AddAsync(UserBehavior user) =>
        await _usersCollection.InsertOneAsync(user);
        public async Task RemoveAsync(string userId) =>
        await _usersCollection.DeleteOneAsync(x => x.UserId == userId);
    }
}