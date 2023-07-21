using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SWP_Management.Repo.Entities
{
    public partial class Student
    {
        private const string V = @"^SE+\d{6}$";

        public Student()
        {
            Accounts = new HashSet<Account>();
            AssignmentStudents = new HashSet<AssignmentStudente>();
            StudentCourses = new HashSet<StudentCourse>();
            StudentTeams = new HashSet<StudentTeam>();
        }

        [RegularExpression(V)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Main { get; set; }
        public bool? Leader { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<AssignmentStudente> AssignmentStudents { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        public virtual ICollection<StudentTeam> StudentTeams { get; set; }
    }
}
