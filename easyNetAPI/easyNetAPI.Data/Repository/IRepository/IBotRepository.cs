using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IBotRepository
    {
        public Task<List<Bot>> GetAllAsync();
        public Task<Bot?> GetFirstOrDefault(int botId);
        public Task AddAsync(Bot bot, int companyId);
        public Task UpdateOneAsync(int botId, Bot bot);
        public Task UpdateManyAsync(Dictionary<int, Bot> bots);
        public Task RemoveAsync(int botId);
    }
}