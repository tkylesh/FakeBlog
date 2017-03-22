using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.Controllers.Contracts
{
    interface IReadPosts
    {
        //Read
        Post GetPost(int postId);
        List<Post> GetPostFromAuthor(string authorId);
    }
}
