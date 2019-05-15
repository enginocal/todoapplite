using System;
using System.Collections.Generic;
using TodoAppLite.Models;
using TodoAppLite.Repositories;

namespace TodoAppLite.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository repository;

        public TodoItemService(ITodoItemRepository repository)
        {
            this.repository = repository;
        }

        public bool AddItem(NewTodoItem item, string userId)
        {
           return repository.AddItem(item, userId);
        }

        public IEnumerable<TodoItem> GetIncompleteItems(string userId)
        {
            return repository.GetIncompleteItems(userId);
        }

        public bool MarkDone(Guid id, string userId)
        {
            return repository.MarkDone(id, userId);
        }
    }
}
