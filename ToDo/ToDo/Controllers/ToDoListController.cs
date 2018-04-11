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
    [Route("api/ToDoList")]

    public class ToDoListController : Controller
    {
        private readonly ToDoListDbContext _context;

        public ToDoListController(ToDoListDbContext context)
        {
            _context = context;

        }


        [HttpGet]
        public IEnumerable<ToDoList> GetDoLists()
        {
            return _context.ToDoLists;
        }

        
    }//Bottom of the class
}
