using System.ComponentModel.DataAnnotations;

namespace SWP_Frontend_Admin.Models
{
    public class Assignment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateEnd { get; set; }

    }
}
