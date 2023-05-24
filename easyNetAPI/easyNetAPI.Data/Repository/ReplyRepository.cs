using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class ReplyRepository
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;
        public ReplyRepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //public void Update(Reply reply)
        //{
        //    var replyFromDb = GetFirstOrDefault(r => r.CommentId == reply.CommentId && r.UserId == reply.UserId);
        //    if (replyFromDb is not null)
        //    {
        //        replyFromDb.Content = reply.Content;
        //        replyFromDb.Like = reply.Like;
        //    }
        //}
    }
}