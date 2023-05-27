using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IPanelRepository
    {
        public Task<List<Panel>> GetAllAsync();
        public Task<Panel?> GetFirstOrDefault(int panelId);
        public Task<bool> AddAsync(Panel panel, int botId);
        public Task<bool> UpdateOneAsync(Panel panel, int botId);
        public Task<bool> UpdateManyAsync(Dictionary<int, Panel> panels, int botId);
        public Task<bool> RemoveAsync(int panelId, int botId);
    }
}