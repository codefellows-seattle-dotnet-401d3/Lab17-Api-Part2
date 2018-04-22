using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoubleResource.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Links back to the models.todo
        public List<ToDo> ToDo { get; set; }
    }
}
