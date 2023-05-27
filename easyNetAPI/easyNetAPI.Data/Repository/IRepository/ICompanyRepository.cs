using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyNetAPI.Models;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface ICompanyRepository
    {
        public Task<List<Company>> GetAllAsync();
        public Task<Company?> GetFirstOrDefault(int companyId);
        public Task<bool> AddAsync(Company company, string userId);
        public Task<bool> UpdateOneAsync(Company company);
        public Task<bool> UpdateManyAsync(Dictionary<int, Company> companies);
        public Task<bool> RemoveAsync(int companyId);
    }
}