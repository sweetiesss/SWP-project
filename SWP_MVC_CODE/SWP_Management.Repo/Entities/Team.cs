using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Team
    {
        public Team()
        {
            Projects = new HashSet<Project>();
            Reports = new HashSet<Report>();
            StudentTeams = new HashSet<StudentTeam>();
        }

        public string Id { get; set; }
        public string CourseId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<StudentTeam> StudentTeams { get; set; }
        public virtual ICollection<AssignmentStudente> AssignmentStudents { get; set; }
    }
}
