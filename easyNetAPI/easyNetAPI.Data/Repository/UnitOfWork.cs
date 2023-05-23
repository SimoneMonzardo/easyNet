using easyNetAPI.Models;
using easyNetAPI.Data.Repository.IRepository;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;


namespace easyNetAPI.Data.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        public readonly MongoDBService _db;
        public UnitOfWork(MongoDBService db)
        {
            _db = db;
            UserBehavior = new UserBehaviorRepository(_db);
            Company = new CompanyRepository(_db);
            Bot = new BotRepository(_db);
            QA = new QARepository(_db);
            Panel = new PanelRepository(_db);
            Button = new ButtonRepository(_db);
            Post = new PostRepository(_db);
            Comment = new CommentRepository(_db);
            Reply = new ReplyRepository(_db);
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
        public void Save()
        {
            _db.SaveChanges();
        }

    }
}