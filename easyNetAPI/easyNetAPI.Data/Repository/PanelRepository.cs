using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class PanelRepository : Repository<Panel>, IPanelRepository
    {
        private readonly MongoDBService _db;
        public PanelRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(Panel panel)
        {
            var panelFromDb = GetFirstOrDefault(p => p.PanelId == panel.PanelId);
            if (panelFromDb is not null)
            {
                panelFromDb.PanelName = panelFromDb.PanelName;
                panelFromDb.Buttons = panelFromDb.Buttons;
                panelFromDb.Content = panelFromDb.Content;
            }
        }
    }
}