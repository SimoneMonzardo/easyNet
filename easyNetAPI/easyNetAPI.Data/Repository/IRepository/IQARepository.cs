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
        public Task<bool> AddAsync(QA qa, int botId);
        public Task<bool> UpdateOneAsync(QA qa, int botId);
        public Task<bool> UpdateManyAsync(Dictionary<string, QA> qas, int botId);
        public Task<bool> RemoveAsync(string intent, int botId);
    }
}