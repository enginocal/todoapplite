using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TodoAppLite.Models;
using TodoAppLite.Services;

namespace TodoAppLite.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService service;

        public TodoItemController(ITodoItemService service)
        {
            this.service = service;
        }

        [HttpPost("AddTodoItem")]

        public IActionResult AddTodoItem(NewTodoItem todoItem)
        {
            var userId = User.Identity.Name;

            var result = service.AddItem(todoItem, userId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        // GET: api/TodoItem/5
        [HttpGet("GetIncompleteItems")]
        public IEnumerable<TodoItem> GetIncompleteItems()
        {
            var userId = User.Identity.Name;
            return service.GetIncompleteItems(userId);
        }

       

        // PUT: api/TodoItem/5
        [HttpPut("{id}", Name = "MarkDone")]
        public IActionResult MarkDone(Guid id)
        {
            var userId = User.Identity.Name;
            var result = service.MarkDone(id, userId);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
