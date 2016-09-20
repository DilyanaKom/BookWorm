using System.Collections.Generic;

namespace BookWorm.Models
{
    public class Work
    {

        public Work()
        {
             this.Comments = new HashSet<Comment>();
            this.Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public Genre Genre { get; set; }

        public string CoverPhoto { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}