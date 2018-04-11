﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Honeydew.Models
{
    public class Todo
    {
        public string Name { get; set; }
        public bool Complete { get; set; }
        public int Id { get; set; }
        public Todolist Belongs { get; set; }
    }
}
