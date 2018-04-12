using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo2.Models
{
    public class ToDos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ListItem> ListItems { get; set; }
    }
}
