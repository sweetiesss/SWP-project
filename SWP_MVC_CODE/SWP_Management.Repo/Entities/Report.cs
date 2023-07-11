using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Report
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string TeamId { get; set; }

        public virtual Team Team { get; set; }
    }
}
