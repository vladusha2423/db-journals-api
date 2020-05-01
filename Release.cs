using System;
using System.Collections.Generic;

namespace Lab5
{
    public partial class Release
    {
        public Release()
        {
            Post = new HashSet<Post>();
        }

        public decimal Id { get; set; }
        public decimal? Index { get; set; }
        public int Year { get; set; }
        public int? Number { get; set; }

        public virtual Journal IndexNavigation { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
