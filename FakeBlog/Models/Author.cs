using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        public ApplicationUser BaseUser { get; set; }

        public ICollection<Post> AuthorPosts { get; set; }
        public ICollection<Draft> AuthorDrafts { get; set; }
    }
}