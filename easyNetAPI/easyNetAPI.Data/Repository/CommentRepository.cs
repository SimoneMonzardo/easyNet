using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using easyNetAPI.Models.UpsertModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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
        private UnitOfWork? _unitOfWork;

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

        public async Task<Comment?> GetCommentAsync(int commentId)
        {
            _unitOfWork = new UnitOfWork(_userBehaviorSettings);
            var posts = await _unitOfWork.Post.GetAllAsync();
            foreach (var post in posts)
            {
                foreach (var comment in post.Comments)
                {
                    if (comment.CommentId == commentId)
                    {
                        return comment;
                    }
                }
            }
            return null;
        }

        public async Task<List<string>>? GetLikesOfComment(int commentId)
        {
            var comment = await GetCommentAsync(commentId);
            if (comment is null)
            {
                return null;
            }
            return comment.Like.ToList();
        }

        public async Task<string> AddComment(UpsertComment comment, string userId, string userName)
        {
            _unitOfWork = new UnitOfWork(_userBehaviorSettings);
            var post = await _unitOfWork.Post.GetPostAsync(comment.PostId);
            if (post is null)
            {
                return "Post not found";
            }
            var commentsList = post.Comments.ToList();
            var newComment = new Comment
            {
                Content = comment.Content,
                UserId = userId,
                Username = userName,
                Like = Array.Empty<string>(),
                Replies = Array.Empty<Reply>()
            };
            if (commentsList.Count == 0)
            {
                newComment.CommentId = 1;
            }
            else
            {
                newComment.CommentId = commentsList.LastOrDefault().CommentId + 1;
            }
            commentsList.Add(newComment) ;
            post.Comments = commentsList.ToArray();
            await _unitOfWork.Post.ManagePostComments(post);
            return "Comment added successfully";
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