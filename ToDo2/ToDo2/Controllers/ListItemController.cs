using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo2.Controllers.Data;
using ToDo2.Models;

namespace ToDo2.Controllers
{
    [Produces("application/json")]
    [Route("api/ListItem")]
    public class ListItemController : Controller
    {
        private readonly ToDoListDbContext _context;

        public ListItemController(ToDoListDbContext context)
        {
            _context = context;
        }

        // GET: api/ListItem
        [HttpGet]
        public IEnumerable<ListItem> GetListItems()
        {
            return _context.ListItems;
        }

        // GET: api/ListItem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetListItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listItem = await _context.ListItems.SingleOrDefaultAsync(m => m.Id == id);

            if (listItem == null)
            {
                return NotFound();
            }

            return Ok(listItem);
        }

        // PUT: api/ListItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListItem([FromRoute] int id, [FromBody] ListItem listItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(listItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ListItem
        [HttpPost]
        public async Task<IActionResult> PostListItem([FromBody] ListItem listItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ListItems.Add(listItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListItem", new { id = listItem.Id }, listItem);
        }

        // DELETE: api/ListItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listItem = await _context.ListItems.SingleOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            _context.ListItems.Remove(listItem);
            await _context.SaveChangesAsync();

            return Ok(listItem);
        }

        private bool ListItemExists(int id)
        {
            return _context.ListItems.Any(e => e.Id == id);
        }
    }
}