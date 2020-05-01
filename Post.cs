using System;
using System.Collections.Generic;

namespace Lab5
{
    public partial class Post
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public decimal Id { get; set; }
        public string Code { get; set; }
        public string Pages { get; set; }
        public string Notice { get; set; }

        public virtual Rubricator CodeNavigation { get; set; }
        public virtual Release IdNavigation { get; set; }
    }
}
