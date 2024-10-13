using System;
using System.Collections.Generic;

namespace AmarOvijog.Models
{
    public partial class Upazila
    {
        public Upazila()
        {
            Unions = new HashSet<Union>();
        }

        public int Id { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; } = null!;
        public string BnName { get; set; } = null!;
        public string Url { get; set; } = null!;

        public virtual District District { get; set; } = null!;
        public virtual ICollection<Union> Unions { get; set; }
    }
}
