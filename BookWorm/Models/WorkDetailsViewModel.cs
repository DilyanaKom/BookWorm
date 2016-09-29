using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWorm.Models
{
    public class WorkDetailsViewModel
    {

        public WorkDetailsViewModel()
        {
            this.Tags = new List<string>();
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Genre { get; set; }

        public string CoverPhoto { get; set; }


        public string User { get; set; }

        public List<string> Tags { get; set; }
        public int Id { get; internal set; }
    }
}