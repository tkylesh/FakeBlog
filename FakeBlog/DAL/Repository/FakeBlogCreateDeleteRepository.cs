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
    public class FakeBlogCreateDeleteRepository : ICreateDeletePosts
    {
        //public FakeBlogContext _context { get; set; }
        IDbConnection _blogConnection;

        public FakeBlogCreateDeleteRepository(IDbConnection blogConnection)
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