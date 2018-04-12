using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_Api.Data;
using Todo_Api.Models;

namespace Todo_Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WeekDayController : Controller
    {
        private readonly TodoContext _context;

        public WeekDayController(TodoContext context)
        {
            _context = context;

            if (_context.WeekDay.Count() == 0)
            {
                _context.WeekDay.Add(new WeekDay { Day = "Monday" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<WeekDay> GetAll()
        {
            return _context.WeekDay.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeekDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WeekDay weekDay = await _context.WeekDay.SingleOrDefaultAsync(p => p.ID == id);
            var items = _context.TodoItems.Where(i => i.WeekDay == weekDay.ID).ToList();

            weekDay.Items = items;

            object[] dayObj = new object[] { weekDay, items };
            if(weekDay == null)
            {
                return NotFound();
            }

            return Ok(dayObj);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeekDay([FromRoute] int id, [FromBody] WeekDay weekDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != weekDay.ID)
            {
                return BadRequest();
            }

            _context.Entry(weekDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeekDayExists(id))
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

        [HttpPost]
        public async Task<IActionResult> PostWeekDay([FromBody] WeekDay weekDay)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WeekDay.Add(weekDay);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetWeekDay", new { id = weekDay.ID }, weekDay);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeekDay([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var weekDay = await _context.WeekDay.SingleOrDefaultAsync(d => d.ID == id);
            if (weekDay == null)
            {
                return NotFound();
            }

            _context.WeekDay.Remove(weekDay);
            await _context.SaveChangesAsync();

            return Ok(weekDay);
        }

        private bool WeekDayExists(int id)
        {
            return _context.WeekDay.Any(d => d.ID == id);
        }
    }
}
