using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Honeydew.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new HoneydewContext(
                serviceProvider.GetRequiredService<DbContextOptions<HoneydewContext>>()))
            {
                if (context.Todos.Any())
                {
                    return; //db already holds todos
                }
                else
                {
                    Todolist IAmAPotato = new Todolist() { Name = "I am a potato" };
                    Todolist Daily = new Todolist() { Name = "Daily" };
                    context.Todos.AddRange(
                        new Todo { Name = "Survive", Complete = false, Belongs = IAmAPotato },
                        new Todo { Name = "Feed the Dog", Complete = true, Belongs = Daily },
                        new Todo { Name = "Know Thyself", Complete = false, Belongs = Daily}
                        );
                }
                context.Todolists.RemoveRange();
                List <Todolist> ListofTodoLists = context.Todos.Select(x => x.Belongs).Distinct().ToList();
                foreach(Todolist ul in ListofTodoLists)
                {
                    context.Todolists.Add(ul);
                }
                
                context.SaveChanges();

            }
        }
    }
}
