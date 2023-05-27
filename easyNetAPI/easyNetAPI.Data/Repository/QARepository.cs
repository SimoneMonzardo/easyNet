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
    public class QARepository : IQARepository
    {
        private readonly IBotRepository _bots;
        private readonly IMongoCollection<UserBehavior> _usersCollection;

        public QARepository(IMongoCollection<UserBehavior> usersCollection, IBotRepository bots)
        {
            _usersCollection = usersCollection;
            _bots = bots;
        }

        private async Task<List<QA>> Query()
        {
            var unwindStage = new BsonDocument("$unwind", new BsonDocument {
                {"path","$company.bot.Q&A" }
            });
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
                {"newRoot","$company.bot.Q&A" }
            });
            var pipeline = new[] { unwindStage, replaceRootStage };

            var _qasCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

            //trasforma in lista
            List<QA> qas = new();
            foreach (var bsonDocument in _qasCollection)
            {
                qas.Add(BsonSerializer.Deserialize<QA>(bsonDocument));
            }
            return qas;

        }

        public async Task<List<QA>> GetAllAsync() => await Query();

        public async Task<QA?> GetFirstOrDefault(string intent) =>
            Query().Result.FirstOrDefault(x => x.Intent == intent);

        public async Task<bool> AddAsync(QA qa, int botId)
        {
            Bot bot = _bots.GetFirstOrDefault(botId).Result;
            bot.QA.Add(qa);
            return await _bots.UpdateOneAsync(bot);
        }

        public async Task<bool> RemoveAsync(string intent, int botId)
        {
            Bot bot = _bots.GetFirstOrDefault(botId).Result;
            QA qa = bot.QA.FirstOrDefault(qa => qa.Intent== intent);
            bot.QA.Remove(qa);
            return await _bots.UpdateOneAsync(bot);
        }

        public async Task<bool> UpdateOneAsync(QA qa, int botId)
        {
            Bot bot = _bots.GetAllAsync().Result.ToList().FirstOrDefault(bot => bot.BotId == botId);
            QA oldQA = bot.QA.Where(x=>x.Intent==qa.Intent).FirstOrDefault();
            oldQA = qa;
            return await _bots.UpdateOneAsync(bot);

        }

        public async Task<bool> UpdateManyAsync(Dictionary<string, QA> qas, int botId)
        {
            Bot bot = _bots.GetFirstOrDefault(botId).Result;
            foreach (var qa in qas)
            {
                QA oldQA = bot.QA.Where(x => x.Intent == qa.Key).FirstOrDefault();
                oldQA = qa.Value;
                var result = await _bots.UpdateOneAsync(bot);
                if (!result)
                {
                    return result;
                }
            }
            return true;
        }
    }
}