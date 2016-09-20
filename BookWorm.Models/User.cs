using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorm.Models
{
  public class User
    {
        public User()
        {
            this.Comments = new HashSet<Tag>();
            this.Works = new HashSet<Work>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Tag> Comments { get; set; }

         public virtual ICollection<Work> Works { get; set; }
    }
}
