using Couchbase.Core;
using System;
using System.Collections.Generic;
using TodoAppLite.Common;
using TodoAppLite.Models;

namespace TodoAppLite.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IBucket bucket;

        public TodoItemRepository(ITodoAppBucketProvider bucketProvider)
        {
            bucket = bucketProvider.GetBucket();
        }

        public bool AddItem(NewTodoItem item, string userId)
        {
            var userDocument = bucket.GetDocument<User>(userId);
            var user = userDocument.Content;
            var todoItem = new TodoItem()
            {
                Id = Guid.NewGuid(),
                IsDone = false,
                Title = item.Title,
                CreatedUtc = DateTime.UtcNow
            };

            if (userDocument.Content.TodoList == null)
            {
                userDocument.Content.TodoList = new List<TodoItem> { todoItem };
            }
            else
            {
                userDocument.Content.TodoList.Add(todoItem);
            }

            var result = bucket.Replace(userId, userDocument.Content);

            return result.Success;
        }

        public IEnumerable<TodoItem> GetIncompleteItems(string userId)
        {
            var userDocument = bucket.GetDocument<User>(userId);
            return userDocument.Content.TodoList?.FindAll(x => x.IsDone == false);
        }

        public bool MarkDone(Guid id, string userId)
        {
            var userDocument = bucket.GetDocument<User>(userId);
            var user = userDocument.Content;
            
            userDocument.Content.TodoList.Find(x=>x.Id==id).IsDone=true;
            // .Find(x => x.Id == id).IsDone = true;
            var result = bucket.Replace(userId, userDocument.Content).Success;
            return result;
        }
    }
}
