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
    public class BotRepository : IBotRepository
    {
        private readonly ICompanyRepository _companies;
        private readonly IMongoCollection<UserBehavior> _usersCollection;

        public BotRepository(IMongoCollection<UserBehavior> usersCollection, ICompanyRepository companies)
        {
            _usersCollection = usersCollection;
            _companies = companies;
        }

        private async Task<List<Bot>> Query()
        {
            
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
                {"newRoot","$company.bot" }
            });
            var pipeline = new[] {replaceRootStage };

            var _botsCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

            //trasforma in lista
            List<Bot> bots = new();
            foreach (var bsonDocument in _botsCollection)
            {
                bots.Add(BsonSerializer.Deserialize<Bot>(bsonDocument));
            }
            return bots;

        }

        public async Task<List<Bot>> GetAllAsync() => await Query();

        public async Task<Bot?> GetFirstOrDefault(int botId) =>
        Query().Result.FirstOrDefault(x => x.BotId == botId);

        public async Task<bool> AddAsync(Bot bot, int companyId)
        {
            Company company = _companies.GetFirstOrDefault(companyId).Result;
            company.Bot = bot;
            return await _companies.UpdateOneAsync(company);
        }

        public async Task<bool> RemoveAsync(int botId)
        {
            Company company = _companies.GetAllAsync().Result.ToList().FirstOrDefault(x => x.Bot.BotId == botId);
            company.Bot = null;
            return await _companies.UpdateOneAsync(company);
        }

        public async Task<bool> UpdateOneAsync(Bot bot)
        {
            Company company = _companies.GetAllAsync().Result.ToList().FirstOrDefault(copmany => copmany.Bot.BotId == bot.BotId);
            company.Bot = bot;
            return await _companies.UpdateOneAsync(company);
        }

        public async Task<bool> UpdateManyAsync(Dictionary<int, Bot> bots)
        {
            foreach (var bot in bots)
            {
                Dictionary<int, Company> dict = new();

                List<Company> companies = _companies.GetAllAsync().Result.ToList().Where(company => company.Bot.BotId == bot.Key).ToList();

                foreach (var company in companies)
                {
                    company.Bot = bot.Value;
                    dict.Add(company.CompanyId, company);
                }
                var result = await _companies.UpdateManyAsync(dict);
                if (!result)
                {
                    return result;
                }
            }
            return true;
        }
    }
}