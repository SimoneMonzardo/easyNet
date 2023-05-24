namespace easyNetAPI.Data.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        CompanyRepository Company { get; }
        BotRepository Bot { get; }
        QARepository QA { get; }
        PanelRepository Panel { get; }
        ButtonRepository Button { get; }
        PostRepository Post { get; }
        CommentRepository Comment { get; }
        ReplyRepository Reply { get; }
    }
}