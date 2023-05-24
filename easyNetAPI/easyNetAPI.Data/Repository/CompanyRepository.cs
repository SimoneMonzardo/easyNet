using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using System.Runtime.CompilerServices;

namespace easyNetAPI.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IUserBehaviorRepository _users;
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public CompanyRepository(IMongoCollection<UserBehavior> usersCollection, IUserBehaviorRepository users)
        {
            _usersCollection = usersCollection;
            _users = users;
        }

        private async Task<List<Company>> Query()
        {
            var groupStage = new BsonDocument("$group", "$company");
            // Aggiungi le fasi all'elenco delle fasi di aggregazione
            var pipeline = new[] { groupStage };
            // Esegui l'aggregazione
            var _companyCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

            //trasforma in lista
            List<Company> companies = new();
            foreach (var bsonDocument in _companyCollection) {
                companies.Add(BsonSerializer.Deserialize<Company>(bsonDocument));
            }
            return companies;
        }

        public async Task<List<Company>> GetAllAsync() => await Query();
        // public async Task<List<UserBehavior>> GetAllAsync() =>
        //await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Company?> GetFirstOrDefault(int companyId) =>
        Query().Result.FirstOrDefault(x => x.CompanyId == companyId);

        public async Task AddAsync(Company company, string userId)
        {
            UserBehavior user = _users.GetFirstOrDefault(userId).Result;
            user.Company = company;
            await _users.UpdateAsync(new Dictionary<string, UserBehavior>()
            {
                { user.UserId, user }   
            });
        }
        public async Task RemoveAsync(Company company){
            Dictionary<string, UserBehavior> dict= new();
            List<UserBehavior> users = _users.GetAllAsync().Result.ToList().Where(user => user.Company.CompanyId == company.CompanyId).ToList();
            foreach (var user in users)
            {
                user.Company = null;
                dict.Add(user.UserId, user);
            }
            await _users.UpdateAsync(dict);
        }
    }
}