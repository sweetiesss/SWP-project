namespace SWP_Frontend_Admin.Models
{
    public class AssignmentStudent
    {
        public string TaskId { get; set; }
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Assignment Task { get; set; }
    }
}
