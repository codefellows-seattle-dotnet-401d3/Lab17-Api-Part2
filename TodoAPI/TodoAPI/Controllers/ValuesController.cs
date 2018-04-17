using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;




namespace TodoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ToDoDbContext _context;

        public ValuesController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Todos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            try
            {
                return Ok(await _context.TodoLists.Include(t => t.List)
                                             .FirstAsync(t => t.Id == id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Todo todo)
        {
            if (todo is null || !ModelState.IsValid)
            {
                return BadRequest("Nope");
            }



            if (todo.ListId.HasValue &&
                !(await _context.Todos.AnyAsync(l => l.Id == todo.ListId)))
            {
                return BadRequest("nope");

            }

        
            await _context.Todo.AddAsync(todo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("nope");
            }

            return CreatedAtAction("GetToDo", new { todo.Id }, todo);
        }


        /// <summary>
        /// /////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Todo todo)
        {
            if (todo is null || id != todo.Id || !ModelState.IsValid)
            {
                return BadRequest("Nope");
            }

            if (todo.ListId.HasValue && !(await _context.TodoLists.AnyAsync(l => l.Id == todo.Id)))
            {
                return BadRequest("Nope");
            }

            Todo ExitStrategy;

            try
            {
                ExitStrategy = await _context.Todos.FirstAsync(t => t.Id == id);
            }
            catch
            {
                return NotFound();
            }
        }




 
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
