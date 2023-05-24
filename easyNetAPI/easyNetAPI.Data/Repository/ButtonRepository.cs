using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class ButtonRepository 
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;
        public ButtonRepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //public void Update(Button button)
        //{
        //    var buttonFromDb = GetFirstOrDefault(b => b.ButtonName == button.ButtonName);
        //    if (buttonFromDb is not null)
        //    {
        //        buttonFromDb.PanelId = button.PanelId;
        //    }
        //}
    }
}