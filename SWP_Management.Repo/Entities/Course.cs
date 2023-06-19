using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Course
    {
        public Course()
        {
            Teams = new HashSet<Team>();
        }

        public string Id { get; set; }
        public string SemesterId { get; set; }
        public string SubjectId { get; set; }
        public string LecturerId { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual Lecturer Lecturer { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
