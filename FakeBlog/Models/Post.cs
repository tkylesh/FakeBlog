using System;
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

        public DateTime DateCreated { get; set; } // Required by default
        public DateTime PublishedAt { get; set; }
        public string Body { get; set; }
        public bool IsDraft { get; set; }
        public bool Edited { get; set; }
        public string URL { get; set; }
    }
}