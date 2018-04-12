using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo_Api.Models;

namespace Todo_Api.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<WeekDay> WeekDay { get; set; }
    }
}
