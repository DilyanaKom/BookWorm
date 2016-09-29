using System.Collections.Generic;

namespace BookWorm.Models
{
    public class Tag
    {
        public Tag()
        {
            this.Works = new HashSet<Work>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public virtual ICollection<Work> Works { get; set; }
    }
}