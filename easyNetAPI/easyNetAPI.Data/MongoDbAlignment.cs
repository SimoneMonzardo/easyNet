using easyNetAPI.Data.Repository;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data
{
    public static class MongoDbAlignment
    {
        public static async Task RemoveAllLikesAsync(string userId, IUnitOfWork unitOfWork)
        {
            var posts = unitOfWork.Post.GetAllAsync().Result.ToList()
                .Where(post => post.Likes.Contains(userId)).ToList();
            var comments = unitOfWork.Comment.GetAllAsync().Result.ToList()
                .Where(comment => comment.Likes.Contains(userId)).ToList();
            var replies = unitOfWork.Reply.GetAllAsync().Result.ToList().
                Where(reply => reply.Likes.Contains(userId)).ToList();

            foreach (var post in posts)
            {
                post.Likes.Remove(userId);
                await unitOfWork.Post.UpdateOneAsync(post);
            }

            foreach (var comment in comments)
            {
                int postId = unitOfWork.Post.GetAllAsync().Result.ToList()
                    .Where(post => post.Comments.Select(c=> c.CommentId).ToList().Contains(comment.CommentId))
                    .FirstOrDefault().PostId;
                comment.Likes.Remove(userId);
                await unitOfWork.Comment.UpdateOneAsync(comment, postId);
            }

            foreach (var reply in replies)
            {
                Comment comment = unitOfWork.Comment.GetAllAsync().Result.ToList()
                    .Where(comment => comment.Replies.Select(r=> r.ReplyId).ToList().Contains(reply.ReplyId)).FirstOrDefault();
                int postId = unitOfWork.Post.GetAllAsync().Result.ToList()
                    .Where(post => post.Comments.Select(c => c.CommentId).ToList().Contains(comment.CommentId)).FirstOrDefault().PostId;
                reply.Likes.Remove(userId);
                await unitOfWork.Reply.UpdateOneAsync(reply, postId, comment.CommentId);
            }
        }

        public static async Task RemoveAllTagsAsync(string userId, IUnitOfWork unitOfWork)
        {
            var posts = unitOfWork.Post.GetAllAsync().Result.ToList().Where(user => user.Tags.Contains(userId)).ToList();
            foreach (var post in posts)
            {
                post.Tags.Remove(userId);
                await unitOfWork.Post.UpdateOneAsync(post);
            }
        }

        public static async Task RemoveAllCommentsAsync(string userId, IUnitOfWork unitOfWork)
        {
            var comments = await unitOfWork.Comment.GetAllAsync();
            var commentsToDelete = comments.Where(c => c.UserId == userId).ToList();
            foreach (var comment in commentsToDelete)
            {
                int postId = unitOfWork.Post.GetAllAsync().Result.ToList().Where(p=> p.Comments.Select(c=>c.CommentId).Contains(comment.CommentId)).Select(p => p.PostId).FirstOrDefault();
                await unitOfWork.Comment.RemoveAsync(postId, comment.CommentId);
            }
        }

        public static async Task RemoveAllRepliesAsync(string userId, IUnitOfWork unitOfWork)
        {
            var replies = await unitOfWork.Reply.GetAllAsync();
            var repliesToDelete = replies.Where(r => r.UserId == userId).ToList();
            foreach (var reply in repliesToDelete)
            {
                int commentId = unitOfWork.Comment.GetAllAsync().Result.ToList().Where(c => c.Replies.Select(r => r.ReplyId).Contains(reply.ReplyId)).Select(c => c.CommentId).FirstOrDefault();
                int postId = unitOfWork.Post.GetAllAsync().Result.ToList().Where(p => p.Comments.Select(c => c.CommentId).Contains(commentId)).Select(p => p.PostId).FirstOrDefault();
                await unitOfWork.Reply.RemoveAsync(reply.ReplyId, commentId, postId);
            }
        }

        public static async Task RemovePostDataAsync(int postId,UserBehavior user, IUnitOfWork unitOfWork)
        {
            if (user.LikedPost.Count() != 0)
            {
                if (user.LikedPost.Contains(postId))
                {
                    user.LikedPost.Remove(postId);
                }
            }
            if (user.SavedPost.Count() != 0)
            {
                if (user.SavedPost.Contains(postId))
                {
                    user.SavedPost.Remove(postId);
                }
            }
            if (user.MentionedPost.Count() != 0)
            {
                if (user.MentionedPost.Contains(postId))
                {
                    user.MentionedPost.Remove(postId);
                }
            }
        }
    }
}