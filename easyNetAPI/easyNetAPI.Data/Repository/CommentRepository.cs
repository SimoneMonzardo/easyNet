using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class CommentRepository //: ICommentRepository
    {
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public CommentRepository(IMongoCollection<UserBehavior> usersCollection)
        {
            _usersCollection= usersCollection;
        }


        //private async Task<List<Company>> Query()
        //{
        //    var groupStage = new BsonDocument("$group", "$comment");
        //    // Aggiungi le fasi all'elenco delle fasi di aggregazione
        //    var pipeline = new[] { groupStage };
        //    // Esegui l'aggregazione
        //    var _commentCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

        //    //trasforma in lista
        //    List<Comment comments = new();
        //    foreach (var bsonDocument in _commentCollection)
        //    {
        //        comments.Add(BsonSerializer.Deserialize<Comment>(bsonDocument));
        //    }
        //    return comments;
        //}
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