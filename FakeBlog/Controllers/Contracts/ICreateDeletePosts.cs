using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.Controllers.Contracts
{
    interface ICreateDeletePosts
    {
        //List of methods to help deliver features
        //Create
        void AddPost(string Title, ApplicationUser author, string body, bool IsDraft);

        //Delete
        bool RemovePost(int postId);
    }
}
