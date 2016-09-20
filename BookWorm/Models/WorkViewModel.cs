using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWorm.Models
{
    public class SimpleWorkViewModel
    {
       
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public string Genre { get; set; }
        
    }
}