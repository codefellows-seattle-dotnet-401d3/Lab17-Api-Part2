using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoubleResource.Models;

namespace DoubleResource.Data
{
    public class ToDoDbContext : DbContext
    {

        public ToDoDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDo { get; set; }

        public DbSet<DoubleResource.Models.ToDo> ToDos{get; set;}

    }
}
