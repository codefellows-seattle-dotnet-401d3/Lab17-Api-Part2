using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;


namespace TodoAPI.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {

        }


        //setting the to do list
        public DbSet<Todo> todos { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

    }
}
