using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.Controllers.Contracts
{
    interface IUpdatePosts
    {
        //Update
        bool Publish(int postId);
        bool Edit(int postId, string body);
    }
}
