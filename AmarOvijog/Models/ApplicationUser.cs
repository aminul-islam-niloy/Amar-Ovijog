using Microsoft.AspNetCore.Identity;

namespace AmarOvijog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? NID { get; set; }
        public string? ContractNumber { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
