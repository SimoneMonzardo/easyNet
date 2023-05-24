using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class CommentRepository 
    {
        private readonly UserBehaviorSettings _userBehaviorSettings;

        public CommentRepository(UserBehaviorSettings userBehaviorSettings)
        {
            _userBehaviorSettings = userBehaviorSettings;
        }

        //prende tutti i commenti di un post
        public async Task<IEnumerable<Comment>?> GetAllOfPostAsync(Post post)
        {
            var comments = post.Comments.ToList();
            if (comments.Count() == 0)
            {
                return null;
            }
            return comments;
        }

        //public void Update(Comment comment)
        //{
        //    var commentFromDb = GetFirstOrDefault(c => c.CommentId == comment.CommentId);
        //    if (commentFromDb is not null)
        //    {
        //        commentFromDb.Content = comment.Content;
        //        commentFromDb.Like = comment.Like;
        //        commentFromDb.Replies = comment.Replies;
        //    }
        //}
    }
}