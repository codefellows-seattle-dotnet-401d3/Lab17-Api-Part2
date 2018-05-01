using System;
using Xunit;
using Honeydew.Models;
using Microsoft.EntityFrameworkCore;
using Honeydew.Controllers;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void CanMakeTodolist()
        {
            Todolist tdl = new Todolist() { Name = "Essential" };
            Assert.NotNull(tdl);
        }
        [Fact]
        public void CanMakeTodo()
        {
            Todolist tdl = new Todolist() { Name = "Essential", Id = 1 };
            Todo td = new Todo() { Name = "Turn this in", Complete = false, Belongs = tdl, Id = 1 };
            Assert.False(td.Complete);
        }
        [Fact]
        public void CanGetAllTodos()
        {
            var options = new DbContextOptionsBuilder<HoneydewContext>()
                .UseInMemoryDatabase(databaseName: "testing")
                .Options;
            using (var context = new HoneydewContext(options))
            {
                var tController = new TodoController(context);
                Todolist example = new Todolist() { Name = "example", Id = 9 };
                Todo first = new Todo() { Name = "example", Belongs = example, Complete = true };
                Todo second = new Todo() { Name = "example2", Belongs = example, Complete = false };
                context.Todos.Add(first);
                context.Todos.Add(second);

                context.SaveChanges();

                int count = 0;
                var results = tController.GetAll();
                foreach (Todo t in results)
                {
                    Assert.True(first == t || second == t);
                    count++;
                }
                Assert.Equal(2, count);
            }
        }

        [Fact]
        public async Task CanGetATodo()
        {
            var options = new DbContextOptionsBuilder<HoneydewContext>()
                .UseInMemoryDatabase(databaseName: "testing")
                .Options;
            using (var context = new HoneydewContext(options))
            {
                var tController = new TodoController(context);
                Todolist example = new Todolist() { Name = "example", Id = 9 };
                Todo Example = await context.Todos.FirstOrDefaultAsync(x => x.Name == "example");
                var result = tController.GetTodo(example.Id);
                Assert.NotNull(result);
            }
        }
    }
}
