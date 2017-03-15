using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;

namespace FakeBlog.DAL
{
    public class FakeBlogRepository : IRepository
    {
        public void AddPost(string Title, Author author, string body, bool IsDraft)
        {
            throw new NotImplementedException();
        }

        public bool Edit(int postId, string body)
        {
            throw new NotImplementedException();
        }

        public Post GetPost(int postId)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetPostFromAuthor(int authorId)
        {
            throw new NotImplementedException();
        }

        public bool Publish(int postId)
        {
            throw new NotImplementedException();
        }

        public bool RemovePost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}