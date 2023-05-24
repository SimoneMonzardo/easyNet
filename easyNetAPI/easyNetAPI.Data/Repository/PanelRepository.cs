using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class PanelRepository 
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;
        public PanelRepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //public void Update(Panel panel)
        //{
        //    var panelFromDb = GetFirstOrDefault(p => p.PanelId == panel.PanelId);
        //    if (panelFromDb is not null)
        //    {
        //        panelFromDb.PanelName = panelFromDb.PanelName;
        //        panelFromDb.Buttons = panelFromDb.Buttons;
        //        panelFromDb.Content = panelFromDb.Content;
        //    }
        //}
    }
}