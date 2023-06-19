using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Team
    {
        public Team()
        {
            Reports = new HashSet<Report>();
        }

        public string Id { get; set; }
        public string CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
