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
        public PanelRepository(MongoDBService db) : base(db)
        {

        }

        public void Update(Panel panel)
        {
            throw new NotImplementedException();
        }
    }
}