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
        public Task<bool> AddAsync(Bot bot, int companyId);
        public Task<bool> UpdateOneAsync(Bot bot);
        public Task<bool> UpdateManyAsync(Dictionary<int, Bot> bots);
        public Task<bool> RemoveAsync(int botId);
    }
}