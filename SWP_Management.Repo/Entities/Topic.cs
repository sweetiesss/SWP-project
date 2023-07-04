using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Projects = new HashSet<Project>();
            SubjectTopics = new HashSet<SubjectTopic>();
            TopicAssigns = new HashSet<TopicAssign>();
        }

        public string Id { get; set; }
        public string LecturerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Approval { get; set; }

        public virtual Lecturer Lecturer { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<SubjectTopic> SubjectTopics { get; set; }
        public virtual ICollection<TopicAssign> TopicAssigns { get; set; }
    }
}
