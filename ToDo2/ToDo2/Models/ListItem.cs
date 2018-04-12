using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo2.Models
{
    public class ListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Task { get; set; }
        public ListItem ListItems { get; set; }
        public ToDos ToDos { get; internal set; }
    }
}
