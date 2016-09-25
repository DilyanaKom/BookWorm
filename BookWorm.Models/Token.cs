using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorm.Models
{
   public class Token
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTime Expire { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsValid { get; set; }
    }
}
