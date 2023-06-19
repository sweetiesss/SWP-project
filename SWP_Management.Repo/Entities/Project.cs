using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Project
    {
        public string TeamId { get; set; }
        public string TopicId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Topic Topic { get; set; }
    }
}
