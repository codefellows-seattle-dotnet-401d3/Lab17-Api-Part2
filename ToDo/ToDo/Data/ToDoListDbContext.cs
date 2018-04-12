using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DoubleResource.Models;

namespace DoubleResource.Data
{
    public class ToDoListDbContext : DbContext
    {
        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        //Links back to the to do list
       // public DbSet<ToDoList> ToDoLists { get; set; } -> might not need this.s
        public DbSet<ToDo> ToDos {get;set;}



        //Links back to the Todo list
        public DbSet<ToDoList> ToDoLists { get; set;}


    }
}
