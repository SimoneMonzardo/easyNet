using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

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
            
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
                {"newRoot","$company" }      
            });
            var pipeline = new[] { replaceRootStage };
     
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
            await _users.UpdateOneAsync(userId, user);
        }
      
        
        public async Task RemoveAsync(int companyId){
            Dictionary<string, UserBehavior> dict= new();
            List<UserBehavior> users = _users.GetAllAsync().Result.ToList().Where(user => user.Company.CompanyId == companyId).ToList();
            foreach (var user in users)
            {
                user.Company = null;
                dict.Add(user.UserId, user);
            }
            await _users.UpdateManyAsync(dict);
        }
        public async Task UpdateOneAsync(int companyId, Company company)
        {
           
                Dictionary<string, UserBehavior> dict = new();

                List<UserBehavior> users = _users.GetAllAsync().Result.ToList().Where(user => user.Company.CompanyId == companyId).ToList();

                foreach (var user in users)
                {
                    user.Company = company;
                    dict.Add(user.UserId, user);
                }
                await _users.UpdateManyAsync(dict);
            
        }
        public async Task UpdateManyAsync(Dictionary<int, Company> companies)
        {
            foreach (var company in companies)
            {
                Dictionary<string, UserBehavior> dict = new();
                
                List<UserBehavior> users = _users.GetAllAsync().Result.ToList().Where(user => user.Company.CompanyId == company.Key).ToList();
                
                foreach (var user in users)
                {
                    user.Company = company.Value;
                    dict.Add(user.UserId, user);
                }
                await _users.UpdateManyAsync(dict);
            }
        }
    }
}