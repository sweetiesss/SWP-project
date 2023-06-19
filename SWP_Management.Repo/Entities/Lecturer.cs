using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Lecturer
    {
        public Lecturer()
        {
            Courses = new HashSet<Course>();
            Topics = new HashSet<Topic>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Main { get; set; }
        public bool? Leader { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
