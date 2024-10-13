using System;
using System.Collections.Generic;

namespace AmarOvijog.Models
{
    public partial class Division
    {
        public Division()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? BnName { get; set; }
        public string? Url { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
