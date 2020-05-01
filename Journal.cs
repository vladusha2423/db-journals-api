using System;
using System.Collections.Generic;

namespace Lab5
{
    public partial class Journal
    {
        public Journal()
        {
            Release = new HashSet<Release>();
        }

        public decimal Index { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }

        public virtual ICollection<Release> Release { get; set; }
    }
}
