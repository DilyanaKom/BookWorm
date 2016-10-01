using System.Collections.Generic;

namespace BookWorm.Models
{
    public class Work
    {

        public Work()
        {
            this.Comments = new HashSet<Comment>();
            this.Tags = new HashSet<Tag>();
            this.Chapters = new HashSet<string>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Genre Genre { get; set; }

        public string CoverPhoto { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public string Description { get; set; }

        public ulong Likes { get; set; }

        public ulong Views { get; set; }

        public virtual ICollection<string> Chapters { get; set; }
    }
}