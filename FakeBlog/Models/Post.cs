using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string URL { get; set; }
        public DateTime DateCreated { get; set; } //Required by default //datetime is not nullable by default
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        //if we give post bool prop of IsDraft we don't need to implement a draft class
        public bool IsDraft { get; set; }


        //public List<Draft> PostDrafts { get; set; }
    }
}