using Microsoft.AspNetCore.Identity;

namespace AmarOvijog.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public string? ContractNumber { get; set; }
        public byte[] ProfilePicture { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }

    }
}
