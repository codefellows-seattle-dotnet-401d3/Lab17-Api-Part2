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
     * 
     * 
     */
{
    //Remember to call localhost5561:/api/todo
    [Produces("application/json")]
    [Route("api/ToDo")]
    public class To_doController : Controller
    {
        //Setting up the database context 
        private readonly ToDoDbContext _context;

        //setting the database up
        public To_doController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet] // ->>>>> READ METHOD
        public IActionResult GetAll()
        {
            return View();
        }

        //if the id in the database then get a todo item
        [HttpGet("{id:int}")] // ->>> READ Method
        public async Task<IActionResult> GetTodo(int id)
        {
            /*   Async method, for finding to Do by ID 
             *  
             */

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
        [HttpPost]//->>>>>>>UPDATE Method
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
        [HttpPut("{id:int}")] // ->>>> CREATE METHOD
        public async Task<IActionResult> Put(int id, [FromBody] Todo todo)

            /* Async method to add using the request body,
             * 
             */

        {
            if (todo is null || id != todo.Id || !ModelState.IsValid)
            {
                return BadRequest("Nope");
            }

            /* 
             * If another Id has been made with the same return bad response
             */

            if (todo.ListId.HasValue && !(await _context.TodoLists.AnyAsync(l => l.Id == todo.Id)))
            {
                return BadRequest("Nope");
            }

            /*
             */

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

        // Looks for an integer for Id example of toDo Value ("{id:int}")

        // Remember to enter in ID url, example; http://localhost:58780/api/values/1/ in order to delete id of #1
        [HttpDelete ("{id:int}")] //- >>DELETE METHOD
        public async Task<IActionResult> Delete (int id , Todo todo)
        {
            /* Method for finding the and deleting to do by ID, as long as the Id is not null.
             *
             */

            var result = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (result != null)
            {
                _context.Remove(result);
                await _context.SaveChangesAsync();
                return Ok(result);
            }

            return BadRequest(id);

        }
    }
}
