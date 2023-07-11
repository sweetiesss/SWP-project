using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class SubjectTopic
    {
        public int Id { get; set; }
        public string SubjectId { get; set; }
        public string TopicId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
