using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
    /* Use Post man to test routes with GET PUT Methods 
     */

{
    // Remeber to call localhost:5561/api/todo
    [Produces("application/json")]
    [Route("api/Todo")]

    public class To_ListController : Controller
    {
        // Setting read only to local Database
        private readonly ToDoDbContext _context;


        public To_ListController(ToDoDbContext context)
        {
            _context = context;
        }

        //Finds all data items
        [HttpGet]
        public IActionResult Getall()
        {
            return Ok(_context.TodoLists);
        }

        //passing in a parameter of id 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetToDoList(int id)
        {
            try
            {
                //await async context with lambda expressions
                TodoList list = await _context.TodoLists.FirstAsync(l => l.Id == id);
                IEnumerable<Todo> items = await _context.Todos.Where(i => i.ListId == id)
                                                             .ToListAsync();

                // Returns id and item
                return Ok(new { list, items });
            }
            catch
            {
                // Missing error
                return NotFound();
            }
        }
       
        // Post action from the ID place only in body the list of items with
   
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoList list)
        {
            // if list is empty returns empty list 
            if (list is null || !ModelState.IsValid)
            {

                return BadRequest("Empty");
            }

            // if there is items on the list better add to them
            await _context.TodoLists.AddAsync(list);

            try
            {
                //save items
                await _context.SaveChangesAsync();
            }
            catch
            {
                // TODO: Insert logging here
                return BadRequest("nope");
            }

            //find the new list items
            return CreatedAtAction("GetToDoList", new { list.Id }, list);
        }






    }
}
