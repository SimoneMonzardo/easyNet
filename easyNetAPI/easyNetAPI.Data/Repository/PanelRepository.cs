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
    public class PanelRepository : IPanelRepository
    {
        private readonly IBotRepository _bots;
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public PanelRepository(IMongoCollection<UserBehavior> usersCollection, IBotRepository bots)
        {
            _usersCollection = usersCollection;
            _bots = bots;
        }
        private async Task<List<Panel>> Query()
        {
            var unwindStage = new BsonDocument("$unwind", new BsonDocument {
                {"path","$company.bot.panels" }
            });
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
                {"newRoot","$company.bot.panels" }
            });
            var pipeline = new[] { unwindStage, replaceRootStage };

            var _panelsCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

            //trasforma in lista
            List<Panel> panels = new();
            foreach (var bsonDocument in _panelsCollection)
            {
                panels.Add(BsonSerializer.Deserialize<Panel>(bsonDocument));
            }
            return panels;

        }
        public async Task<List<Panel>> GetAllAsync() => await Query();
        public async Task<Panel?> GetFirstOrDefault(int panelId) =>
            Query().Result.FirstOrDefault(x => x.PanelId == panelId);
        public async Task AddAsync(Panel panel, int botId)
        { 

            Bot bot = _bots.GetFirstOrDefault(botId).Result;
            bot.Panels.Add(panel);
            await _bots.UpdateOneAsync(botId, bot);
        }
        public async Task RemoveAsync(int panelId, int botId)
        {
            Bot bot = _bots.GetFirstOrDefault(botId).Result;
            Panel panel = bot.Panels.FirstOrDefault(panel => panel.PanelId == panelId);
            bot.Panels.Remove(panel);
            await _bots.UpdateOneAsync(bot.BotId, bot);
        }
        public async Task UpdateOneAsync(int panelId, Panel panel, int botId)
        {
            Bot bot = _bots.GetAllAsync().Result.ToList().FirstOrDefault(bot => bot.BotId == botId);
            Panel _panel = bot.Panels.Where(x => x.PanelId == panelId).FirstOrDefault();
            _panel = panel;
            await _bots.UpdateOneAsync(bot.BotId, bot);

        }
        public async Task UpdateManyAsync(Dictionary<int, Panel> panels, int botId)
        {
            Bot bot = _bots.GetFirstOrDefault(botId).Result;
            foreach (var panel in panels)
            {
                Panel _panel = bot.Panels.Where(x => x.PanelId == panel.Key).FirstOrDefault();
                _panel = panel.Value;
                await _bots.UpdateOneAsync(bot.BotId, bot);
            }
        }
    }
}