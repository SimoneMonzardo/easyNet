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
        /// <summary>
        /// updates one or more Company objects from DB.
        /// param: "companies" dictionary of all the companies to update key: companyId value: updated company
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        public Task UpdateAsync(Dictionary<int, Company> companies);
        public Task RemoveAsync(int companyId);
    }
}