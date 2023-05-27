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
using easyNetAPI.Models.UpsertModels;

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
                {"path","$posts.comments" }
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

        public async Task<bool> AddAsync(Comment comment, int postId)
        {
            Post post = _posts.GetFirstOrDefault(postId).Result;
            if (post is null)
            {
                return false;
            }
            post.Comments.Add(comment);
            return await _posts.UpdateOneAsync(post);
        }

        public async Task<bool> UpdateOneAsync(Comment newComment, int postId)
        {
            Post post = await _posts.GetFirstOrDefault(postId);
            if (post is null)
            {
                return false;
            }
            Comment comment = await GetFirstOrDefault(newComment.CommentId);
            if (comment is null || !post.Comments.Contains(comment))
            {
                return false;
            }
            comment = newComment;
            return await _posts.UpdateOneAsync(post);
        }

        public async Task<bool> UpdateContentAsync(UpsertComment upsertComment)
        {
            var postFromDb = await _posts.GetFirstOrDefault(upsertComment.PostId);
            if (postFromDb is null)
            {
                return false;
            }
            var commentFromDb = postFromDb.Comments.Where(c => c.CommentId == upsertComment.CommentId).FirstOrDefault();
            if (commentFromDb is null)
            {
                return false;
            }
            commentFromDb.Content = upsertComment.Content;
            return await _posts.UpdateOneAsync(postFromDb);
        }

        public async Task<bool> UpdateManyAsync(Dictionary<int, Comment> comments, int postId)
        {
            try
            {
                foreach (var comment in comments)
                {
                    Post post = _posts.GetAllAsync().Result.FirstOrDefault(post => post.PostId == postId);
                    Comment oldComment = post.Comments.Where(x => x.CommentId == comment.Key).FirstOrDefault();
                    oldComment = comment.Value;
                    await _posts.UpdateOneAsync(post);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int postId, int commentId)
        {
            Post post = _posts.GetFirstOrDefault(postId).Result;
            Comment comment = post.Comments.Where(x => x.CommentId == commentId).FirstOrDefault();
            post.Comments.Remove(comment);
            return await _posts.UpdateOneAsync(post);
        }

        public async Task<List<string>>? GetCommentLikes(int commentId)
        {
            var comment = await GetFirstOrDefault(commentId);
            if (comment is null)
            {
                return null;
            }
            return comment.Likes.ToList();
        }
    }
}