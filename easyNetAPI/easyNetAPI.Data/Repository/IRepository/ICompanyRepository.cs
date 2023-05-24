using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface ICompanyRepository
    {
        public Task<List<Company>> GetAllAsync();
        public Task<Company?> GetFirstOrDefault(int companyId);
        public Task AddAsync(Company company, string userId);
        public Task RemoveAsync(Company company);
    }
}