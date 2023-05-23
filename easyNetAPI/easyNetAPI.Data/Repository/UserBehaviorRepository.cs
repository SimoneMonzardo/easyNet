using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using easyNetAPI.Data.Repository.IRepository;
using easyNetAPI.Models;

namespace easyNetAPI.Data.Repository
{
    public class UserBehaviorRepository : Repository<UserBehavior>, IUserBehaviorRepository
    {
        private readonly MongoDBService _db;
        public UserBehaviorRepository(MongoDBService db) : base(db)
        {
            _db = db;
        }

        public void Update(UserBehavior userBehavior)
        {
            var userBehaviorFromDb = GetFirstOrDefault(u => u.UserId == userBehavior.UserId);
            if (userBehaviorFromDb is not null)
            {
                userBehaviorFromDb.Administrator = userBehavior.Administrator;
                userBehaviorFromDb.Company = userBehavior.Company;
                userBehaviorFromDb.FollowedList = userBehavior.FollowedList;
                userBehaviorFromDb.FollowedUsers = userBehaviorFromDb.FollowedUsers;
                userBehaviorFromDb.LikedPost = userBehavior.LikedPost;
                userBehaviorFromDb.MentionedPost = userBehavior.MentionedPost;
                userBehaviorFromDb.Posts = userBehavior.Posts;
                userBehaviorFromDb.SavedPost = userBehavior.SavedPost;
            }
        }
    }

}