using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public String Name { get; set; }

        //Links back to the models.todo
        public ToDo ToDo { get; set; }
    }
}
