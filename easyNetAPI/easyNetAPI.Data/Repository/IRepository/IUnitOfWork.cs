namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {

        ICompanyRepository Company { get; }
        IUserBehaviorRepository UserBehavior { get; }
        IBotRepository Bot { get; }
        IQARepository QA { get; }
        IPanelRepository Panel { get; }
        IButtonRepository Button { get; }
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        IReplyRepository Reply { get; }

    }
}