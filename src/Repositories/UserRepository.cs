using Couchbase;
using Couchbase.Core;
using System;
using TodoAppLite.Common;
using TodoAppLite.Models;

namespace TodoAppLite.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IBucket bucket;

        public UserRepository(ITodoAppBucketProvider bucketProvider)
        {
            bucket = bucketProvider.GetBucket();
        }

        public void AddUser(User user)
        {
            var userDoc = new Document<User>
            {
                Id = user.Id.ToString(),
                Content = user,
                Expiry = 5
            };

            var result = bucket.Insert(userDoc);
        }

        public User Get(string userKey)
        {
            var userDocument = bucket.GetDocument<User>(userKey);
            return userDocument.Content;
        }

        public User Get(Guid id)
        {
            var userDocument = bucket.GetDocument<User>(id.ToString());
            var user = userDocument.Content;
            return user;
        }
    }
}
