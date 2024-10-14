using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmarOvijog.Models
{
    public class Complaint
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        // Foreign key for ApplicationUser (Identity User)
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; } = string.Empty;

        // Navigation property for ApplicationUser
        public ApplicationUser User { get; set; } = null!;

        // Foreign key for Division
        [Required]
        [ForeignKey("Division")]
        public int DivisionId { get; set; }

        // Navigation property for Division
        public Division Division { get; set; } = null!;

        // Foreign key for District
        [Required]
        [ForeignKey("District")]
        public int DistrictId { get; set; }

        // Navigation property for District
        public District District { get; set; } = null!;

        // Foreign key for Upazila
        [Required]
        [ForeignKey("Upazila")]
        public int UpazilaId { get; set; }

        // Navigation property for Upazila
        public Upazila Upazila { get; set; } = null!;

        // Foreign key for Union
        [Required]
        [ForeignKey("Union")]
        public int UnionId { get; set; }

        // Navigation property for Union
        public Union Union { get; set; } = null!;

        // Collection for related ComplaintImages
        public ICollection<ComplaintImage> ComplaintImages { get; set; } = new List<ComplaintImage>();

        // Timestamp for when the complaint was created
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Track if the complaint is resolved
        public bool IsResolved { get; set; } = false;

        // Track if the complaint has been read
        public bool IsRead { get; set; } = false;
    }
}
