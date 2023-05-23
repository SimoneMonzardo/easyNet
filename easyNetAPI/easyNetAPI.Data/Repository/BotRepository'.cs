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
        public BotRepository(MongoDBService db) : base(db)
        {

        }

        public void Update(Bot bot)
        {
            throw new NotImplementedException();
        }
    }
}
