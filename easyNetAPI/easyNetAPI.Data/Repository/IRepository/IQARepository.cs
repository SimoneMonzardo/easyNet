using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IQARepository
    {
        public Task<List<QA>> GetAllAsync();
        public Task<QA?> GetFirstOrDefault(string intent);
        public Task AddAsync(QA qa, int botId);
        public Task UpdateOneAsync(string intent, QA qa, int botId);
        public Task UpdateManyAsync(Dictionary<string, QA> qas, int botId);
        public Task RemoveAsync(string intent, int botId);
    }
}