using easyNetAPI.Models;
using easyNetAPI.Data.Repository.IRepository;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace easyNetAPI.Data.Repository
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;

        public UnitOfWork(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
            Post = new PostRepository(_userBehaviorSettings);
            Comment = new CommentRepository(_userBehaviorSettings);
            Bot = new BotRepository(_userBehaviorSettings);
            Button = new ButtonRepository(_userBehaviorSettings);
            Company = new CompanyRepository(_userBehaviorSettings);
            Panel = new PanelRepository(_userBehaviorSettings);
            QA = new QARepository(_userBehaviorSettings);
            Reply = new ReplyRepository(_userBehaviorSettings);
        }
        public PostRepository Post { get; private set; } = null!;
        public CommentRepository Comment { get; private set; } = null!;
        public BotRepository Bot { get; private set; } = null!;
        public ButtonRepository Button { get; private set; } = null!;
        public CompanyRepository Company { get; private set; } = null!;
        public PanelRepository Panel { get; private set; } = null!;
        public QARepository QA { get; private set; } = null!;
        public ReplyRepository Reply { get; private set; } = null!;
    }
}