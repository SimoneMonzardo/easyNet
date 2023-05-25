using easyNetAPI.Models;
using easyNetAPI.Data.Repository.IRepository;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace easyNetAPI.Data.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IOptions<MongoDbSettings> _settings;
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public UnitOfWork(IOptions<MongoDbSettings> settings)
        {
            _settings = settings;
            var mongoClient = new MongoClient(
            settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<UserBehavior>(
                settings.Value.CollectionName);
            UserBehavior = new UserBehaviorRepository(_usersCollection);
            Company = new CompanyRepository(_usersCollection, UserBehavior);
            Bot = new BotRepository(_usersCollection, Company);
            QA = new QARepository(_usersCollection, Bot);
            Panel = new PanelRepository(_usersCollection, Bot);
            Button = new ButtonRepository(_usersCollection, Panel);
            Post = new PostRepository(_usersCollection,UserBehavior);
            Comment = new CommentRepository(_usersCollection, Post);
            Reply = new ReplyRepository(_usersCollection, Comment );
        }
        public ICompanyRepository Company { get; private set; } = null!;
        public IUserBehaviorRepository UserBehavior { get; private set;}=null!;
        public IBotRepository Bot { get; private set;}=null!;
        public IQARepository QA { get; private set;}=null!;
        public IPanelRepository Panel { get; private set;}=null!;
        public IButtonRepository Button { get; private set;}=null!;
        public IPostRepository Post { get; private set;}=null!;
        public ICommentRepository Comment { get; private set;}=null!;
        public IReplyRepository Reply { get; private set;}=null!;

    }
}