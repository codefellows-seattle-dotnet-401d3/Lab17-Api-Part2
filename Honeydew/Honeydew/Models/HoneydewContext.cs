using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honeydew.Models
{
    public class HoneydewContext : DbContext
    {
        public HoneydewContext(DbContextOptions<HoneydewContext> options) :base (options)
        {
            // Eternal emptyness
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Todolist> Todolists { get; set; }
    }
}
