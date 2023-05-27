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
using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace easyNetAPI.Data.Repository
{
    public class ButtonRepository : IButtonRepository
    {
        private readonly IPanelRepository _panels;
        private readonly IMongoCollection<UserBehavior> _usersCollection;

        public ButtonRepository(IMongoCollection<UserBehavior> usersCollection, IPanelRepository panels)
        {
            _usersCollection = usersCollection;
            _panels = panels;
        }

        private async Task<List<Button>> Query()
        {
            var unwindStage = new BsonDocument("$unwind", new BsonDocument {
        {
          "path",
          "$company.bot.panels"
        }
      });
            var secondUnwindStage = new BsonDocument("$unwind", new BsonDocument {
        {
          "path",
          "$company.bot.panels.buttons"
        }
      });
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
        {
          "newRoot",
          "$company.bot.panels.buttons"
        }
      });
            var pipeline = new[] {
        unwindStage,
        secondUnwindStage,
        replaceRootStage
      };
            var _buttonsCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();
            List<Button> buttons = new();
            foreach (var bsonDocument in _buttonsCollection)
            {
                buttons.Add(BsonSerializer.Deserialize<Button>(bsonDocument));
            }
            return buttons;
        }

        public async Task<List<Button>> GetAllAsync() => await Query();

        public async Task<Button?> GetFirstOrDefault(string buttonName) => Query().Result.FirstOrDefault(x => x.ButtonName == buttonName);

        public async Task<bool> AddAsync(Button button, int botId)
        {
            Panel panel = _panels.GetFirstOrDefault(button.PanelId).Result;
            panel.Buttons.Add(button);
            return await _panels.UpdateOneAsync(panel, botId);
        }

        public async Task<bool> UpdateOneAsync(string buttonName, Button button, int panelId, int botId)
        {
            Panel panel = _panels.GetAllAsync().Result.ToList().FirstOrDefault(panel => panel.PanelId == panelId);
            Button _button = panel.Buttons.Where(x => x.ButtonName == buttonName).FirstOrDefault();
            _button = button;
            return await _panels.UpdateOneAsync(panel, botId);
        }

        public async Task<bool> UpdateManyAsync(Dictionary<string, Button> buttons, int panelId, int botId)
        {
            foreach (var button in buttons)
            {
                Panel panel = _panels.GetAllAsync().Result.FirstOrDefault(panel => panel.PanelId == panelId);
                Button oldButton = panel.Buttons.Where(x => x.ButtonName == button.Key).FirstOrDefault();
                oldButton = button.Value;
                var result = await _panels.UpdateOneAsync(panel, botId);
                if (!result)
                {
                    return result;
                }
            }
            return true;
        }

        public async Task<bool> RemoveAsync(int panelId, int botId, string buttonName)
        {
            Panel panel = _panels.GetFirstOrDefault(panelId).Result;
            Button button = panel.Buttons.Where(x => x.ButtonName == buttonName).FirstOrDefault();
            panel.Buttons.Remove(button);
            return await _panels.UpdateOneAsync(panel, botId);
        }
    }
}