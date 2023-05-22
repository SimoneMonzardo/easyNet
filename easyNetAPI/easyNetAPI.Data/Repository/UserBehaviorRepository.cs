using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;

namespace easyNetAPI.Data.Repository
{
    public class UserBehaviorRepository : Repository<UserBehavior>, IUserBehaviorRepository
    {
        public UserBehaviorRepository(MongoDBService db) : base(db)
        {

        }

        public void Update(UserBehavior userBehavior)
        {
            throw new NotImplementedException();
        }
    }

}