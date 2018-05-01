using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Honeydew.Models;

namespace Honeydew.Controllers
{
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly HoneydewContext _context;

        public TodoController (HoneydewContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Todo> GetAll()
        {
            List<Todolist> list = _context.Todolists.ToList();
            return _context.Todos.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult GetTodo(int id)
        {
            var td = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (td == null)
            {
                return NotFound();
            }
            return new ObjectResult(td);
        }

        [HttpPost]
        public IActionResult AddTodo([FromBody] Todo td)
        {
            if (td == null)
            {
                return BadRequest();
            }
            _context.Todos.Add( new Todo { Belongs = td.Belongs,
                                           Complete = td.Complete,
                                           Name = td.Name
                                          });
            _context.SaveChanges();
             int newId = _context.Todos.First<Todo>(t => t.Belongs == td.Belongs
                                          && t.Complete == td.Complete
                                          && t.Name == td.Name)
                                            .Id;

            return CreatedAtRoute("GetTodo",new { id = newId },td);
        }

        [HttpPut("{id}")]
        public IActionResult Edit (int id, [FromBody] Todo td)
        {
            if (td == null || td.Id != id)
            {
                return BadRequest();
            }

            var todo = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Complete = td.Complete;
            todo.Name = td.Name;
            todo.Belongs = td.Belongs;

            _context.Todos.Update(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Annihilate (int id)
        {
            var td = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (td == null)
            {
                return BadRequest();
            }
            _context.Todos.Remove(td);
            _context.SaveChanges();
            return new ObjectResult(td);
        }
    }
}
