using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoubleResource.Models;
using DoubleResource.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoubleResource.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ToDoController : Controller
    {
        private ToDoListDbContext _context;

        public ToDoController(ToDoListDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return _context.ToDos;
        }


        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetSong([FromRoute] int id)
        {
            var item = await _context.ToDos.FirstOrDefaultAsync(s => s.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }









    }//Bottom of the class
}
