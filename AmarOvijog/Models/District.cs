using System;
using System.Collections.Generic;

namespace AmarOvijog.Models
{
    public partial class District
    {
        public District()
        {
            Upazilas = new HashSet<Upazila>();
        }

        public int Id { get; set; }
        public int DivisionId { get; set; }
        public string Name { get; set; } = null!;
        public string BnName { get; set; } = null!;
        public string? Lat { get; set; }
        public string? Lon { get; set; }
        public string Url { get; set; } = null!;

        public virtual Division Division { get; set; } = null!;
        public virtual ICollection<Upazila> Upazilas { get; set; }
    }
}
