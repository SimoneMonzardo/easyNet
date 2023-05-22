using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class QARepository : Repository<QA>, IQARepository
    {
        public QARepository(MongoDBService db) : base(db)
        {

        }

        public void Update(QA qa)
        {
            throw new NotImplementedException();
        }
    }
}
