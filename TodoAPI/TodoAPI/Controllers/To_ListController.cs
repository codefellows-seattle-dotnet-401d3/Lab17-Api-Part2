﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Todo")]

    public class To_ListController : Controller
    {
        private readonly ToDoDbContext _context;

        public To_ListController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            return Ok(_context.TodoLists);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetToDoList(int id)
        {
            try
            {
                TodoList list = await _context.TodoLists.FirstAsync(l => l.Id == id);
                IEnumerable<Todo> items = await _context.Todos.Where(i => i.ListId == id)
                                                             .ToListAsync();

                // Return an object with the requested list along with its ToDo items
                return Ok(new { list, items });
            }
            catch
            {
                // TODO: Insert logging here
                return NotFound();
            }
        }
       
        // 
       
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoList list)
        {
            if (list is null || !ModelState.IsValid)
            {
                return BadRequest("Empty or invalid ToDoList body provided");
            }

            await _context.TodoLists.AddAsync(list);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // TODO: Insert logging here
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Could not commit new ToDoList entity to database");
            }

            return CreatedAtAction("GetToDoList", new { list.Id }, list);
        }






    }
}
