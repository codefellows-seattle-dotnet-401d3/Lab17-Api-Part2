using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsComplete { get; set; }


        //Links back to The Models.TodoList
        public ToDoList ToDoList { get; set;}
    }
}
