﻿using FakeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeBlog.Controllers.Contracts
{
    interface IRepository
    {
        //List of methods to help deliver features
        //Create
        void AddPost(string Title, ApplicationUser author, string body, bool IsDraft);

        //Read
        Post GetPost(int postId);
        List<Post> GetPostFromAuthor(string authorId);

        //Update
        bool Publish(int postId);
        bool Edit(int postId, string body);

        //Delete
        bool RemovePost(int postId);
    }
}
