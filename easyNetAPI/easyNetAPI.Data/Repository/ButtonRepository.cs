using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class ButtonRepository : Repository<Button>, IButtonRepository
    {
        private readonly MongoDBService _db;
        public ButtonRepository(MongoDBService db) : base(db)
        {

        }

        public void Update(Button button)
        {
            var buttonFromDb = GetFirstOrDefault(b => b.ButtonName == button.ButtonName);
            if (buttonFromDb is not null)
            {
                buttonFromDb.PanelId = button.PanelId;
            }
        }
    }
}