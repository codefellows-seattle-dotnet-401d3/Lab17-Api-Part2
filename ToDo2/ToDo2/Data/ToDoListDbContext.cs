using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo2.Models;

namespace ToDo2.Controllers.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        // Database tables

        public DbSet<ListItem> ListItems { get; set; }

        // Database tables

        public DbSet<ToDo2.Models.ToDos> ToDos { get; set; }
    }
}
