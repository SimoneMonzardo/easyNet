using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyNetAPI.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IPostRepository _posts;
        private readonly IMongoCollection<UserBehavior> _usersCollection;
        public CommentRepository(IMongoCollection<UserBehavior> usersCollection, IPostRepository posts)
        {
            _usersCollection = usersCollection;
            _posts = posts; 
        }
        private async Task<List<Comment>> Query()
        {
            var unwindStage = new BsonDocument("$unwind", new BsonDocument {
                {"path","$posts" }
            });
            var secondUnwindStage = new BsonDocument("$unwind", new BsonDocument {
                {"path","$$posts.comments" }
            });
            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
                {"newRoot","$posts.comments" }
            });
            var pipeline = new[] { unwindStage, secondUnwindStage, replaceRootStage };

            var _commentscollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

            
            List<Comment> comments = new();
            foreach (var bsonDocument in _commentscollection)
            {
                comments.Add(BsonSerializer.Deserialize<Comment>(bsonDocument));
            }
            return comments;
        }

        public async Task<List<Comment>> GetAllAsync() => await Query();

        public async Task<Comment?> GetFirstOrDefault(int commentId) =>
        Query().Result.FirstOrDefault(x => x.CommentId == commentId);

        public async Task AddAsync(Comment comment, int postId)
        {
            Post post = _posts.GetFirstOrDefault(postId).Result;
            post.Comments.Add(comment);
            await _posts.UpdateOneAsync(postId, post, post.UserId);
        }
        public async Task UpdateOneAsync(int commentId, Comment comment, int postId)
        {
            Post post = _posts.GetAllAsync().Result.ToList().FirstOrDefault(post => post.PostId == postId);
            Comment _comment = post.Comments.Where(x => x.CommentId == commentId).FirstOrDefault();
            _comment = comment;
            await _posts.UpdateOneAsync(post.PostId, post, post.UserId);

        }
        public async Task UpdateManyAsync(Dictionary<int, Comment> comments, int postId)
        {

            foreach (var comment in comments)
            {
                Post post = _posts.GetAllAsync().Result.FirstOrDefault(post => post.PostId == postId);
                Comment _comment = post.Comments.Where(x => x.CommentId == comment.Key).FirstOrDefault();
                _comment = comment.Value;
                await _posts.UpdateOneAsync(post.PostId, post, post.UserId);
            }
        }
        public async Task RemoveAsync(int postId, int commentId)
        {
            Post post = _posts.GetFirstOrDefault(postId).Result;
            Comment comment = post.Comments.Where(x => x.CommentId == commentId).FirstOrDefault();
            post.Comments.Remove(comment);
            await _posts.UpdateOneAsync(postId, post, post.UserId);
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