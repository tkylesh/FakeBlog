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
            try
            {

                Post found_post = GetPost(postId);
                found_post.Body = body;
                found_post.Edited = true;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public Post GetPost(int postId)
        {
            Post found_post = _context.Posts.FirstOrDefault(p => p.PostId == postId);
            return found_post;
        }

        public List<Post> GetPostFromAuthor(string authorId)
        {
            return _context.Posts.Where(p => p.Author.Id == authorId).ToList();
        }

        public bool Publish(int postId)
        {
            Post found_post = GetPost(postId);

            //if is draft
            if(found_post.IsDraft)
            { 
                found_post.IsDraft = false;
                found_post.PublishedAt = DateTime.Now;
                _context.SaveChanges();
                return true;
            }
            //if already published or not draft
            return false;
        }

        public bool RemovePost(int postId)
        {
            Post found_post = GetPost(postId);
            if(found_post != null)
            {
                _context.Posts.Remove(found_post);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}