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
        public Task<bool> AddAsync(Button button, int botId);
        public Task<bool> UpdateOneAsync(string buttonName, Button button, int panelId, int botId);
        public Task<bool> UpdateManyAsync(Dictionary<string, Button> buttons, int panelId, int botId);
        public Task<bool> RemoveAsync(int panelId, int botId, string buttonName);
    }
}