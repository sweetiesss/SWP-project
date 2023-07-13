using System;
using System.Collections.Generic;

namespace SWP_Management.Repo.Entities
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string StudentId { get; set; }
        public string TeacherId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Lecturer Teacher { get; set; }
    }
}
