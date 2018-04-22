using System;
using Xunit;
using TodoAPI.Controllers;
using TodoAPI.Models;
using TodoAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;



namespace XUnitTestProject1
{
    //testing to add to a list.
    public class UnitTest1
    {
        [Fact]
        public async System.Threading.Tasks.Task Test1Async()
        {
            //database set up
            DbContextOptions<ToDoDbContext> options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (ToDoDbContext context = new ToDoDbContext(options))
            {
                //arrange
                To_ListController controller = new To_ListController(context);
                int initialCount = await context.TodoLists.CountAsync();
                await controller.Post(new TodoList());
                {
                  
                }

                //assert 
                Assert.True((await context.TodoLists.CountAsync()) > initialCount);

            }
        }

        [Fact]
        public async void ListNumber()
        {
            //database set up
            DbContextOptions<ToDoDbContext> options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (ToDoDbContext context = new ToDoDbContext(options))
            {
                // Arrange
                await context.TodoLists.AddRangeAsync(
                    new TodoList()
                    {
                        Name = "Get Milk",
                    },

                    new TodoList()
                    {
                        Name = "Throw Away Milk",
                    }
                );
                await context.SaveChangesAsync();

                To_ListController controller = new To_ListController(context);

                // Act
                OkObjectResult result = controller.Getall() as OkObjectResult;
                DbSet<TodoList> lists = result.Value as DbSet<TodoList>;

                // Assert. Counts how many list number do you have
                Assert.Equal(2, await lists.CountAsync());


            }
        }// End of List number


    }
}
