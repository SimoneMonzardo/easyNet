using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class BotRepository
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;
        public BotRepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //public void Update(Bot bot)
        //{
        //    var botFromDb = GetFirstOrDefault(b => b.BotId == bot.BotId);
        //    if (botFromDb is not null)
        //    {
        //        botFromDb.Type = bot.Type;
        //        botFromDb.Platform = bot.Platform;
        //        botFromDb.QA = bot.QA;
        //        botFromDb.Panels = bot.Panels;
        //    }
        //}
    }
}