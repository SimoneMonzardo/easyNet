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
        private readonly IUnitOfWork _unitOfWork;

        public CommentRepository(UserBehaviorSettings userBehaviorSettings, UnitOfWork unitOfWork)
        {
            _userBehaviorSettings = userBehaviorSettings;
            _unitOfWork = unitOfWork;
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

        public async Task<Comment?> GetCommentAsync(int commentId)
        {
            var posts = await _unitOfWork.Post.GetAllAsync();
            foreach (var post in posts)
            {
                foreach(var comment in post.Comments)
                {
                    if(comment.CommentId == commentId)
                        return comment;
                }
            }
            return null;
        }

        public async Task<List<String>>? GetLikesOfComment(int commentId)
        {
            var comment = await GetCommentAsync(commentId);
            if (comment is null)
                return null;
            return comment.Like.ToList();
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