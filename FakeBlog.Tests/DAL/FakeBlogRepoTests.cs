using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeBlog.DAL;
using Moq;
using FakeBlog.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace FakeBlog.Tests.DAL
{
    [TestClass]
    public class FakeBlogRepoTests
    {

        public Mock<FakeBlogContext> fake_context { get; set; }
        public FakeBlogRepository repo { get; set; }
        public Mock<DbSet<Post>> mock_posts_set { get; set; }
        public IQueryable<Post> query_posts { get; set; }
        public List<Post> fake_post_table { get; set; }
        public ApplicationUser sally { get; set; }
        public ApplicationUser sammy { get; set; }


        [TestInitialize]
        public void Setup()
        {
            fake_post_table = new List<Post>();
            fake_context = new Mock<FakeBlogContext>();
            mock_posts_set = new Mock<DbSet<Post>>();
            repo = new FakeBlogRepository(fake_context.Object);

            sally = new ApplicationUser { Id = "sally-id-1" };
            sammy = new ApplicationUser { Id = "sammy-id-1" };
        }

        public void CreateFakeDatabase()
        {
            query_posts = fake_post_table.AsQueryable(); // Re-express this list as something that understands queries

            // Hey LINQ, use the Provider from our *cough* fake *cough* board table/list
            mock_posts_set.As<IQueryable<Post>>().Setup(b => b.Provider).Returns(query_posts.Provider);
            mock_posts_set.As<IQueryable<Post>>().Setup(b => b.Expression).Returns(query_posts.Expression);
            mock_posts_set.As<IQueryable<Post>>().Setup(b => b.ElementType).Returns(query_posts.ElementType);
            mock_posts_set.As<IQueryable<Post>>().Setup(b => b.GetEnumerator()).Returns(() => query_posts.GetEnumerator());

            mock_posts_set.Setup(b => b.Add(It.IsAny<Post>())).Callback((Post post) => fake_post_table.Add(post));

            mock_posts_set.Setup(b => b.Remove(It.IsAny<Post>())).Callback((Post post) => fake_post_table.Remove(post));

            fake_context.Setup(c => c.Posts).Returns(mock_posts_set.Object); // Context.Boards returns fake_board_table (a list)
        }


        [TestMethod]
        public void EnsureICanCreateInstanceOfRepo()
        {
            FakeBlogRepository repo = new FakeBlogRepository();

            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void EnsureIHaveNotNullContext()
        {
            FakeBlogRepository repo = new FakeBlogRepository();

            Assert.IsNotNull(repo._context);
        }

        [TestMethod]
        public void EnsureICanInjectContextInstance()
        {
            FakeBlogContext context = new FakeBlogContext();
            FakeBlogRepository repo = new FakeBlogRepository(context);

            Assert.IsNotNull(repo._context);
        }

        [TestMethod]
        public void EnsureICanAddPost()
        {
            //Arrange
            CreateFakeDatabase();

            ApplicationUser a_user = new ApplicationUser
            {
                Id = "my-user-id",
                UserName = "Sammy",
                Email = "sammy@gmail.com"
            };

            //Act
            repo.AddPost("Test Post", a_user, "",true);

            //Assert
            Assert.AreEqual(1, repo._context.Posts.Count());
        }


    }
}
