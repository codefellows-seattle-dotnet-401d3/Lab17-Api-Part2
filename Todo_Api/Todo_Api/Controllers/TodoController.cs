using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo_Api.Data;
using Todo_Api.Models;

namespace Todo_Api.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;
        public TodoController(TodoContext context)
        {
            _context = context;

            
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Fold Laundry" });
                _context.SaveChanges();
            }
            
        }

        // Gets all todo items
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        // Get a single todo item by ID
        [HttpGet("{id}", Name = "GetItem")]
        public IActionResult GetById(long id)
        {
            var item = _context.TodoItems.FirstOrDefault(i => i.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // Create a new todo item
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if(item == null)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetItem", new { id = item.ID }, item);
        }

        // Update a todo item
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if(item == null || item.ID != id)
            {
                return BadRequest();
            }

            var todo = _context.TodoItems.FirstOrDefault(i => i.ID == id);
            if(todo == null)
            {
                return NotFound();
            }

            // Sets the values for the new todo item
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            todo.WeekDay = item.WeekDay;

            // Adds the new todo item to the DB and saves the DB
            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return new NoContentResult();
        }

        // Delete a todo Item
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.FirstOrDefault(i => i.ID == id);
            if(todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
