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
    /* Use postman to test routes with Get and Put Methods
     */
{
    //Remember to call localhost5561:/api/tod
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //for your eyes only
        private readonly ToDoDbContext _context;

        //setting the database up
        public ValuesController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet] // ->>>>> READ METHOD
        public IActionResult GetAll()
        {
            return Ok(_context.Todos);
        }

        //if the id in the database then get a todo item
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            try
            {
                return Ok(await _context.Todos.Include(t => t.List)
                                             .FirstAsync(t => t.Id == id));
            }
            catch
            {
                return NotFound();
            }
        }

        //putting on our items
        [HttpPost]//->>>>>>>Read Method
        public async Task<IActionResult> Post([FromBody] Todo todo)
        {
            // if the model is bad then return bad calls
            if (todo is null || !ModelState.IsValid)
            {
                return BadRequest("Nope");
            }
            // if found of to do and the id has something
            if (todo.ListId.HasValue &&
                !(await _context.Todos.AnyAsync(l => l.Id == todo.ListId)))
            {
                return BadRequest("nope");

            }

            await _context.Todos.AddAsync(todo);

            try
            {
                //adding items to the saving of the changes
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("nope");
            }

            return CreatedAtAction("GetToDo", new { todo.Id }, todo);
        }


        // Placing the method 
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] Todo todo)
        {
            if (todo is null || id != todo.Id || !ModelState.IsValid)
            {
                return BadRequest("Nope");
            }
            // Adding items to the lambda expressions
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
         
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {

                return BadRequest("nope");
            }
            return NoContent();
        }

        // Looks for an integer for Id example of toDo Value
        [HttpDelete("{id:int}")] //->>>>>>>>>>>>>DELETE METHOD
        public async Task<IActionResult> Delete(int id)
        {

            Todo existingToDo;

            try
            {
                //linq query to see where it exists
                existingToDo = await _context.Todos.FirstAsync(t => t.Id == id);
            }
            catch
            {
               
                return NotFound();
            }

            //.Remove function performed on Exisiting todo function
            _context.Todos.Remove(existingToDo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
             
                return BadRequest("nope");
            }

            return NoContent();
        }
    }
}
