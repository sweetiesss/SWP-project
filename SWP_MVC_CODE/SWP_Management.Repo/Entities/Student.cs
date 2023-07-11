using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Student
    {
        public Student()
        {
            Accounts = new HashSet<Account>();
            AssignmentStudents = new HashSet<AssignmentStudent>();
            StudentCourses = new HashSet<StudentCourse>();
            StudentTeams = new HashSet<StudentTeam>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Main { get; set; }
        public bool? Leader { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AssignmentStudent> AssignmentStudents { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentTeam> StudentTeams { get; set; }
    }
}
