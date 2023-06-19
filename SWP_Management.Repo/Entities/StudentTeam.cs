using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class StudentTeam
    {
        public string TeamId { get; set; }
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Team Team { get; set; }
    }
}
