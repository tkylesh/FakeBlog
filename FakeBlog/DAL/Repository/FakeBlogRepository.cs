using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using FakeBlog.Controllers.Contracts;
using System.Data.Common;
using System.Data;

namespace FakeBlog.DAL.Repository
{
    public class FakeBlogRepository : ICreateDeletePosts, IReadPosts, IUpdatePosts
    {
        //public FakeBlogContext _context { get; set; }
        IDbConnection _blogConnection;

        public FakeBlogRepository(IDbConnection blogConnection)
        {
            //_context = new FakeBlogContext();
            //_blogConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _blogConnection = blogConnection;
        }


        public void AddPost(string Title, string author_Id, string body, bool IsDraft)
        {

            //open connection
            _blogConnection.Open();

            try 
            {
                //run queries
                var addPostCommand = _blogConnection.CreateCommand();
                addPostCommand.CommandText = @"Insert into Posts(Title,Author,Body,IsDraft) values(@Title,@author,@body,@IsDraft)";
                var titleParameter = new SqlParameter("Title", System.Data.SqlDbType.VarChar);
                titleParameter.Value = Title;
                addPostCommand.Parameters.Add(titleParameter);
                var authorParameter = new SqlParameter("Author", System.Data.SqlDbType.VarChar);
                authorParameter.Value = author_Id;
                addPostCommand.Parameters.Add(authorParameter);
                var bodyParameter = new SqlParameter("Body", System.Data.SqlDbType.VarChar);
                bodyParameter.Value = body;
                addPostCommand.Parameters.Add(bodyParameter);
                var draftParameter = new SqlParameter("IsDraft", System.Data.SqlDbType.VarChar);
                draftParameter.Value = IsDraft;
                addPostCommand.Parameters.Add(draftParameter);

                //execute the command
                addPostCommand.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                //close the connection
                _blogConnection.Close();
            }
        }

        public bool Edit(int postId, string body)
        {

            _blogConnection.Open();

            try
            {
                var updatePostCommand = _blogConnection.CreateCommand();

                updatePostCommand.CommandText = @"
                    Update Posts
                    Set Body = @body
                    Where PostId = @postId
                ";
                var bodyParameter = new SqlParameter("body", System.Data.SqlDbType.VarChar);
                bodyParameter.Value = body;
                updatePostCommand.Parameters.Add(bodyParameter);
                var postIdParameter = new SqlParameter("postId", System.Data.SqlDbType.Int);
                postIdParameter.Value = postId;
                updatePostCommand.Parameters.Add(postIdParameter);

                updatePostCommand.ExecuteNonQuery();
                //"if it made it this far than it must have worked"
                return true;
            }
            catch(SqlException ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine(ex);
            }
            finally
            {
                _blogConnection.Close();
            }
            return false;
            
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
                if(reader.Read())
                {
                    var post = new Post()
                    {
                        PostId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Body = reader.GetString(2),
                        Author = new ApplicationUser {Id = reader.GetString(3)}
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
                if(reader.Read())
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

        public bool Publish(int postId)
        {
            _blogConnection.Open();

            try
            {
                var updatePostCommand = _blogConnection.CreateCommand();
                updatePostCommand.CommandText = @"
                    Update Posts
                    Set IsDraft = false
                    Where PostId == @postId
                ";
                var postIdParam = new SqlParameter("postId", System.Data.SqlDbType.VarChar);
                postIdParam.Value = postId;
                updatePostCommand.Parameters.Add(postIdParam);

                updatePostCommand.ExecuteNonQuery();

                return true;
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
            return false;
        }

        public bool RemovePost(int postId)
        {

            _blogConnection.Open();

            try
            {
                var removePostCommand = _blogConnection.CreateCommand();
                removePostCommand.CommandText = @"
                    DELETE
                    FROM Posts
                    WHERE PostId == @postId
                ";
                var postIdParam = new SqlParameter("postId", System.Data.SqlDbType.VarChar);
                postIdParam.Value = postId;
                removePostCommand.Parameters.Add(postIdParam);

                removePostCommand.ExecuteNonQuery();

                return true;
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
            return false;
        }
    }
}