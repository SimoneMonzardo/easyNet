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
        // public async Task<List<UserBehavior>> GetAllAsync() =>
        //await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Bot?> GetFirstOrDefault(int botId) =>
        Query().Result.FirstOrDefault(x => x.BotId == botId);

        public async Task AddAsync(Bot bot, int companyId)
        {
            Company company = _companies.GetFirstOrDefault(companyId).Result;
            company.Bot = bot;
            await _companies.UpdateOneAsync(companyId, company);
        }
        public async Task RemoveAsync(int botId)
        {
            Company company = _companies.GetAllAsync().Result.ToList().FirstOrDefault(x => x.Bot.BotId == botId);
            company.Bot = null;
            await _companies.UpdateOneAsync(company.CompanyId, company);
        }
        public async Task UpdateOneAsync(int botId, Bot bot)
        {
            Company company = _companies.GetAllAsync().Result.ToList().FirstOrDefault(copmany => copmany.Bot.BotId == botId);
            company.Bot = bot;
            await _companies.UpdateOneAsync(company.CompanyId,company);

        }
        public async Task UpdateManyAsync(Dictionary<int, Bot> bots)
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
                await _companies.UpdateManyAsync(dict);
            }
        }
    }
}