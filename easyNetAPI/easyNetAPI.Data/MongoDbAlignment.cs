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
    static class MongoDbAlignment
    {
        public static async Task RemoveAllLikesAsync(string userId, IUnitOfWork unitOfWork)
        {
            var posts = unitOfWork.Post.GetAllAsync().Result.ToList()
                .Where(post => post.Likes.Contains(userId));
            var comments = unitOfWork.Comment.GetAllAsync().Result.ToList()
                .Where(comment => comment.Likes.Contains(userId));
            var replies = unitOfWork.Reply.GetAllAsync().Result.ToList().
                Where(reply => reply.Likes.Contains(userId));

            foreach (var post in posts)
            {
                post.Likes.Remove(post.Likes.FirstOrDefault(userId));
                unitOfWork.Post.UpdateOneAsync(post.PostId, post, userId);
            }

            foreach (var comment in comments)
            {
                int postId = unitOfWork.Post.GetAllAsync().Result.ToList()
                    .Where(post => post.Comments.Contains(comment))
                    .FirstOrDefault().PostId;
                comment.Likes.Remove(comment.Likes.FirstOrDefault(userId));
                await unitOfWork.Comment.UpdateOneAsync(comment.CommentId, comment, postId);
            }

            foreach (var reply in replies)
            {
                Comment comment = unitOfWork.Comment.GetAllAsync().Result.ToList()
                    .Where(comment => comment.Replies.Contains(reply)).FirstOrDefault();
                int postId = unitOfWork.Post.GetAllAsync().Result.ToList()
                    .Where(post => post.Comments.Contains(comment)).FirstOrDefault().PostId;
                reply.Likes.Remove(reply.Likes.FirstOrDefault(userId));
                await unitOfWork.Reply.UpdateOneAsync(reply.ReplyId, reply, postId, comment.CommentId);
            }
        }
        public static async Task RemoveAllTagsAsync(string userId, IUnitOfWork unitOfWork)
        {
            var posts = unitOfWork.Post.GetAllAsync().Result.ToList().Where(user => user.Tags.Contains(userId));
            foreach (var post in posts)
            {
                post.Tags.Remove(post.Tags.FirstOrDefault(userId));
                unitOfWork.Post.UpdateOneAsync(post.PostId, post, userId);
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