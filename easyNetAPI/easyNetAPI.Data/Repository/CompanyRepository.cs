using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class CompanyRepository 
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;
        public CompanyRepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //public void Update(Company company)
        //{
        //    var companyFromDb = GetFirstOrDefault(c => c.CompanyId == company.CompanyId);
        //    if (companyFromDb is not null)
        //    {
        //        companyFromDb.CompanyName = company.CompanyName;
        //        companyFromDb.Bot = company.Bot;
        //    }
        //}
    }
}