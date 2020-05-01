using System;
using System.Collections.Generic;

namespace Lab5
{
    public partial class Rubricator
    {
        public Rubricator()
        {
            Post = new HashSet<Post>();
        }

        public string Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Post> Post { get; set; }
    }
}
