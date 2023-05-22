using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly MongoDBService _db;
        public CompanyRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            var companyFromDb = GetFirstOrDefault(c => c.CompanyId == company.CompanyId);
            if (companyFromDb is not null)
            {
                companyFromDb.CompanyName = company.CompanyName;
                companyFromDb.Bot = company.Bot;
            }
        }
    }
}