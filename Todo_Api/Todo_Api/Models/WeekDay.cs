using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo_Api.Models
{
    public class WeekDay
    {
        public int ID { get; set; }
        public string Day { get; set; }
        public List<TodoItem> Items { get; set; }
    }
}
