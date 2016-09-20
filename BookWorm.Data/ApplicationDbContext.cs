using BookWorm.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWorm.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        : base("BookWorm")
        {
        }



        public IDbSet<User> Settings
        {
            get;
            set;
        }

        public IDbSet<Comment> Comments
        {
            get;
            set;
        }


        public IDbSet<Token> Tokens
        {
            get;
            set;
        }


        public IDbSet<Tag> Tags
        {
            get;
            set;
        }


        public IDbSet<Work> Works
        {
            get;
            set;
        }

    }
}
