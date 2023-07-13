using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            Courses = new HashSet<Course>();
            SubjectTopics = new HashSet<SubjectTopic>();
            TopicAssigns = new HashSet<TopicAssign>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }
        public virtual ICollection<TopicAssign> TopicAssigns { get; set; }
    }
}
