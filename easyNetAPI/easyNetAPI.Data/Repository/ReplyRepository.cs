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
using System.ComponentModel.Design;
using easyNetAPI.Models.UpsertModels;

namespace easyNetAPI.Data.Repository
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly ICommentRepository _comments;
        private readonly IMongoCollection<UserBehavior> _usersCollection;

        public ReplyRepository(IMongoCollection<UserBehavior> usersCollection, ICommentRepository comments)
        {
            _usersCollection = usersCollection;
            _comments = comments;
        }

        private async Task<List<Reply>> Query()
        {

            var unwindStage = new BsonDocument("$unwind", new BsonDocument {
                {"path","$posts" }
            });
            var secondUnwindStage = new BsonDocument("$unwind", new BsonDocument {
                {"path","$posts.comments" }
            });
            var thirdUnwindStage = new BsonDocument("$unwind", new BsonDocument {
                { "path", "$posts.comments.replies"}
            } );

            var replaceRootStage = new BsonDocument("$replaceRoot", new BsonDocument {
                {"newRoot","$posts.comments.replies" }
            });
            var pipeline = new[] { unwindStage, secondUnwindStage,thirdUnwindStage, replaceRootStage };
            var _repliesCollection = _usersCollection.Aggregate<BsonDocument>(pipeline).ToList();

            //trasforma in lista
            List<Reply> replies = new();
            foreach (var bsonDocument in _repliesCollection)
            {
                replies.Add(BsonSerializer.Deserialize<Reply>(bsonDocument));
            }
            return replies;
        }

        public async Task<List<Reply>> GetAllAsync() => await Query();

        public async Task<Reply?> GetFirstOrDefault(int replyId) =>
          Query().Result.FirstOrDefault(x => x.ReplyId == replyId);

        public async Task<bool> AddAsync(Reply reply, int commentId, int postId)
        {
            Comment comment = _comments.GetFirstOrDefault(commentId).Result;
            comment.Replies.Add(reply);
            return await _comments.UpdateOneAsync(comment, postId);
        }

        public async Task<bool> UpdateOneAsync(Reply reply, int commentId, int postId)
        {
            Comment comment = _comments.GetAllAsync().Result.ToList().FirstOrDefault(comment => comment.CommentId == commentId);
            Reply oldReply = comment.Replies.Where(x => x.ReplyId == reply.ReplyId).FirstOrDefault();
            oldReply = reply;
            return await _comments.UpdateOneAsync(comment,postId);
        }

        public async Task<bool> UpdateContentAsync(UpsertReply reply)
        {
            Comment comment = await _comments.GetFirstOrDefault(reply.CommentId);
            if (comment is null)
            {
                return false;
            }

            Reply oldReply = comment.Replies.Where(r => r.ReplyId == reply.ReplyId).FirstOrDefault();
            if (oldReply is null)
            {
                return false;
            }

            oldReply.Content = reply.Content;

            return await _comments.UpdateOneAsync(comment, reply.PostId);
        }

        public async Task<bool> UpdateManyAsync(Dictionary<int, Reply> replies, int commentId, int postId)
        {
            Comment comment = _comments.GetAllAsync().Result.ToList().FirstOrDefault(comment => comment.CommentId == commentId);
            foreach (var reply in replies)
            {
                Reply oldReply = comment.Replies.Where(x => x.ReplyId == reply.Key).FirstOrDefault();
                oldReply = reply.Value;
                var result = await _comments.UpdateOneAsync(comment, postId);
                if (!result)
                {
                    return result;
                }
            }
            return true;
        }

        public async Task<bool> RemoveAsync(int replyId,int commentId, int postId)
        {
            Comment comment = _comments.GetFirstOrDefault(commentId).Result;
            Reply reply = comment.Replies.Where(x => x.ReplyId == replyId).FirstOrDefault();
            comment.Replies.Remove(reply);
            return await _comments.UpdateOneAsync(comment, postId);
        }
    }
}