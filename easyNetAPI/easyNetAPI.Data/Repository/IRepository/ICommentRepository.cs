using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository.IRepository
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetFirstOrDefault(int commentsId);
        public Task AddAsync(Comment comment, string userId);
        /// <summary>
        /// updates one or more Company objects from DB.
        /// param: "companies" dictionary of all the companies to update key: companyId value: updated company
        /// </summary>
        /// <param name="companies"></param>
        /// <returns></returns>
        public Task UpdateAsync(Dictionary<int, Comment> comments);
        public Task RemoveAsync(Comment comment);
    }
}