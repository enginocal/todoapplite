using System;
using System.Collections.Generic;
using TodoAppLite.Models;

namespace TodoAppLite.Services
{
    public interface ITodoItemService
    {
        bool AddItem(NewTodoItem item, string userId);
        bool MarkDone(Guid id, string userId);
        IEnumerable<TodoItem> GetIncompleteItems(string userId);
    }
}
