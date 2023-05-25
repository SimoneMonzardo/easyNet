using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IButtonRepository
    {
        public Task<List<Button>> GetAllAsync();
        public Task<Button?> GetFirstOrDefault(string buttonName);
        public Task AddAsync(Button button, int panelId, int botId);
        public Task UpdateOneAsync(string buttonName, Button button, int panelId, int botId);
        public Task UpdateManyAsync(Dictionary<string, Button> buttons, int panelId, int botId);
        public Task RemoveAsync(int panelId, int botId, string buttonName);
    }
}