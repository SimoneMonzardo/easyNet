using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class ReplyRepository : Repository<Reply>, IReplyRepository
    {
        public ReplyRepository(MongoDBService db) : base(db)
        {

        }

        public void Update(Reply reply)
        {
            throw new NotImplementedException();
        }
    }
}