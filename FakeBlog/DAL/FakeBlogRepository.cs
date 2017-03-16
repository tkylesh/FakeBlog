using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;

namespace FakeBlog.DAL
{
    public class FakeBlogRepository : IRepository
    {
        public FakeBlogContext _context { get; set; }

        public FakeBlogRepository()
        {
            _context = new FakeBlogContext();
        }

        public FakeBlogRepository(FakeBlogContext context)
        {
            _context = context;
        }
        public void AddPost(string Title, ApplicationUser author, string body, bool IsDraft)
        {
            Post post = new Post { Title = Title, Author = author, Body = body, IsDraft = IsDraft};
            _context.Posts.Add(post);
            _context.SaveChanges();
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