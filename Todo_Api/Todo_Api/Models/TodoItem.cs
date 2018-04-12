using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo_Api.Models
{
    public class TodoItem
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public int WeekDay { get; set; }
    }
}
