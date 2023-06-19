using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Main { get; set; }
        public bool? Leader { get; set; }
    }
}
