using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class BotRepository : Repository<Bot>, IBotRepository
    {
        private readonly MongoDBService _db;
        public BotRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(Bot bot)
        {
            var botFromDb = GetFirstOrDefault(b => b.BotId == bot.BotId);
            if (botFromDb is not null)
            {
                botFromDb.Type = bot.Type;
                botFromDb.Platform = bot.Platform;
                botFromDb.QA = bot.QA;
                botFromDb.Panels = bot.Panels;
            }
        }
    }
}