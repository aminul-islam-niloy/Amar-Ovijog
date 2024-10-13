using System.ComponentModel.DataAnnotations;

namespace AmarOvijog.Models
{
    public class ComplaintImage
    {
        public int Id { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public int ComplaintId { get; set; } // Foreign Key to Complaint

        public Complaint Complaint { get; set; } = null!;
    }
}
