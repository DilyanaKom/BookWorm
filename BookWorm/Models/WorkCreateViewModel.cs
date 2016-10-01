using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWorm.Models
{
    public class WorkCreateViewModel
    {
        public WorkCreateViewModel()
        {
            this.Tags = new List<string>();
            this.Chapters = new List<string>();
        }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public Genre Genre { get; set; }

        public string CoverPhoto { get; set; }

        public List<string> Tags { get; set; }

        public List<string> Chapters { get; set; }

        public string Description { get; set; }

        public ulong Likes { get; set; }

        public ulong Views { get; set; }
    }
}