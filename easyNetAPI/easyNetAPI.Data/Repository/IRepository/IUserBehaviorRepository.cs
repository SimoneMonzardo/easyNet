using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IUserBehaviorRepository : IRepository<UserBehavior>
    {
        void Update(UserBehavior userBehavior);
    }
}
