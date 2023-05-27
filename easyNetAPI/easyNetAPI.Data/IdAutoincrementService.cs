using System;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Data.Repository;

namespace easyNetAPI.Data
{
    public static class IdAutoincrementService
    {
        public static async Task<int> GetPostAutoincrementId(IUnitOfWork _unitOfWork)
        {
            var postsList = await _unitOfWork.Post.GetAllAsync();
            var idList = postsList.Select(p => p.PostId).Order();
            if (idList.Count() != 0)
            {
                return idList.LastOrDefault() + 1;
            }
            return 1;
        }

        public static async Task<int> GetBotAutoincrementId(IUnitOfWork _unitOfWork)
        {
            var botList = await _unitOfWork.Bot.GetAllAsync();
            var idList = botList.Select(b => b.BotId).Order();
            if (idList.Count() != 0)
            {
                return idList.LastOrDefault() + 1;
            }
            return 1;
        }

        public static async Task<int> GetPanelAutoincrementId(IUnitOfWork _unitOfWork)
        {
            var panelList = await _unitOfWork.Panel.GetAllAsync();
            var idList = panelList.Select(p => p.PanelId).Order();
            if (idList.Count() != 0)
            {
                return idList.LastOrDefault() + 1;
            }
            return 1;
        }

        public static async Task<int> GetCommentAutoincrementId(IUnitOfWork _unitOfWork)
        {
            var commentsList = await _unitOfWork.Comment.GetAllAsync();
            var idList = commentsList.Select(c => c.CommentId).Order();
            if (idList.Count() != 0)
            {
                return idList.LastOrDefault() + 1;
            }
            return 1;
        }

        public static async Task<int> GetCompanyAutoincrementId(IUnitOfWork _unitOfWork)
        {
            var companiesList = await _unitOfWork.Company.GetAllAsync();
            var idList = companiesList.Select(c => c.CompanyId).Order();
            if (idList.Count() != 0)
            {
                return idList.LastOrDefault() + 1;
            }
            return 1;
        }

        public static async Task<int> GetReplyAutoincrementId(IUnitOfWork _unitOfWork)
        {
            var repliesList = await _unitOfWork.Reply.GetAllAsync();
            var idList = repliesList.Select(r => r.ReplyId).Order();
            if (idList.Count() != 0)
            {
                return idList.LastOrDefault() + 1;
            }
            return 1;
        }
    }
}