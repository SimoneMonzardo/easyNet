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
        public Task AddAsync(Panel panel, int botId);
        public Task UpdateOneAsync(int panelId, Panel panel, int botId);
        public Task UpdateManyAsync(Dictionary<int, Panel> panels, int botId);
        public Task RemoveAsync(int panelId, int botId);
    }
}