using FakeBlog.Controllers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FakeBlog.DAL.Repository
{
    public class FakeBlogReadRepository : IReadPosts
    {
        IDbConnection _blogConnection;

        public FakeBlogReadRepository(IDbConnection blogConnection)
        {
            //_context = new FakeBlogContext();
            //_blogConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _blogConnection = blogConnection;
        }
        public Post GetPost(int postId)
        {
            _blogConnection.Open();

            try
            {
                var getPostCommand = _blogConnection.CreateCommand();
                getPostCommand.CommandText = @"
                    SELECT postId, Title, Body, Author
                    FROM Posts
                    WHERE PostId == @postId
                ";
                var postIdParam = new SqlParameter("postId", System.Data.SqlDbType.Int);
                postIdParam.Value = postId;
                getPostCommand.Parameters.Add(postIdParam);

                //going to return a sql data reader
                var reader = getPostCommand.ExecuteReader();

                //reads one row at a time
                if (reader.Read())
                {
                    var post = new Post()
                    {
                        PostId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Body = reader.GetString(2),
                        Author = new ApplicationUser { Id = reader.GetString(3) }
                    };
                    return post;
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _blogConnection.Close();
            }
            return null;
        }

        public List<Post> GetPostFromAuthor(string authorId)
        {
            _blogConnection.Open();

            try
            {
                var getPostCommand = _blogConnection.CreateCommand();
                getPostCommand.CommandText = @"
                    SELECT postId, Title, Body, Author
                    FROM Posts
                    WHERE AuthorID = @authorId
                ";
                var authorIdParam = new SqlParameter("authorId", System.Data.SqlDbType.VarChar);
                authorIdParam.Value = authorId;
                getPostCommand.Parameters.Add(authorIdParam);

                var reader = getPostCommand.ExecuteReader();

                var posts = new List<Post>();

                //reads one row at a time
                if (reader.Read())
                {
                    var post = new Post()
                    {
                        PostId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Body = reader.GetString(2),
                        Author = new ApplicationUser { Id = reader.GetString(3) }

                    };
                }
                return posts;
            }
            catch (SqlException ex)
            {

                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _blogConnection.Close();
            }
            return new List<Post>();
        }
    }
}