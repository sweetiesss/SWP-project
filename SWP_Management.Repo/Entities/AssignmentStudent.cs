using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class AssignmentStudent
    {
        public string TaskId { get; set; }
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Assignment Task { get; set; }
    }
}
