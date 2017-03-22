using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeBlog.Models;
using FakeBlog.Controllers.Contracts;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace FakeBlog.DAL.Repository
{
    public class FakeBlogUpdateRepository : IUpdatePosts
    {
        IDbConnection _blogConnection;

        public FakeBlogUpdateRepository(IDbConnection blogConnection)
        {
            //_context = new FakeBlogContext();
            //_blogConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            _blogConnection = blogConnection;
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
            catch (SqlException ex)
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
    }
}