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

        /* ToDo Database table w/
         * To_dos | to_Do_Lists
         */

        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }

    }
}
