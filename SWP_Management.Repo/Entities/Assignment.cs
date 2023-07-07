using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP_Management.Repo.Entities
{
    public partial class Assignment
    {
        public Assignment()
        {
            AssignmentStudents = new HashSet<AssignmentStudent>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public virtual ICollection<AssignmentStudent> AssignmentStudents { get; set; }
    }
}
