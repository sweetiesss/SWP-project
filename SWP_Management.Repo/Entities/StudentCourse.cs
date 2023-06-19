using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class StudentCourse
    {
        public string CourseId { get; set; }
        public string StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
