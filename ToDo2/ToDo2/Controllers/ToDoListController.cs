using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo2.Controllers.Data;
using ToDo2.Models;


namespace ToDo2.Controllers
{
    [Route("api/[controller]")]
    public class ToDoListController : Controller
    {
        private ToDoListDbContext _context;

        public ToDoListController(ToDoListDbContext context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<ListItem> Get()
        {
            return _context.ListItems;
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            ToDos toDos = await _context.ToDos.FirstOrDefaultAsync(s => s.Id == id);
            var ListItem = _context.ListItems.Where(s => toDos.Id == toDos.Id).ToList();

            toDos.ListItems = ListItem;


            if (ListItem == null)
                return NotFound();
            return Ok(toDos);
            
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ListItem value)
        {
            await _context.ListItems.AddAsync(value);
            _context.SaveChanges();
            return CreatedAtRoute("Get", new {value.Id}, value);
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
