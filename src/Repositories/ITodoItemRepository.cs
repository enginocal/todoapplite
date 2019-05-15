using System;
using System.Collections.Generic;
using TodoAppLite.Models;

namespace TodoAppLite.Repositories
{
    public interface ITodoItemRepository
    {
        bool AddItem(NewTodoItem item, string userId);
        bool MarkDone(Guid id, string userId);
        IEnumerable<TodoItem> GetIncompleteItems(string userId);
    }
}
