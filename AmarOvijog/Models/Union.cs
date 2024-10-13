using System;
using System.Collections.Generic;

namespace AmarOvijog.Models
{
    public partial class Union
    {
        public int Id { get; set; }
        public int UpazillaId { get; set; }
        public string? Name { get; set; }
        public string? BnName { get; set; }
        public string? Url { get; set; }

        public virtual Upazila Upazilla { get; set; } = null!;
    }
}
