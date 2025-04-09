using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using INDIACom.Models;
using System.Reflection;

namespace INDIACom.App_Cude
{
    public class DAL
    {
        private static SqlConnection con = null;
        private static void OpenConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlConnection.ClearAllPools();
            con.Open();
        }
        private static void DisposeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
        }
        public string filter_bad_chars_rep(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            string[] sL_Char = {
            "onfocus",
            "\"\"",
            "=",
            "onmouseover",
            "prompt",
            "%27",
            "alert#",
            "alert",
            "'or",
            "`or",
            "`or`",
            "'or'",
            "'='",
            "`=`",
            "'",
            "`",
            "9,9,9",
            "src",
            "onload",
            "select",
            "drop",
            "insert",
            "delete",
            "xp_",
            "having",
            "union",
            "group",
            "update",
            "script",
            "<script",
            "</script>",
            "language",
            "javascript",
            "vbscript",
            "http",
            "~",
            "$",
            "<",
            ">",
            "%",
            "\\",
            ";",
            "",
            "_",
            "{",
            "}",
            "^",
            "?",
            "[",
            "]",
            "!",
            "#",
            "&",
            "*"
        };
            int sL_Char_Length = sL_Char.Length - 1;
            int i = 0;
            while (i <= sL_Char_Length)
            {
                if (s.Contains(sL_Char[i]))
                {
                    s = s.Replace(sL_Char[i], "");
                    // break; // TODO: might not be correct. Was : Exit While
                }
                i++;
                sL_Char_Length -= 1;
            }
            return s.Trim();



        }




        #region department

        public string AddDepartment(Department dept)
        {
            string message = "";
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_AddDepartment";
                cmd.Parameters.AddWithValue("Dept_Name", dept.DeptName);


                cmd.Parameters.AddWithValue("Msg", "");
                cmd.Parameters["Msg"].Size = 256;
                cmd.Parameters["Msg"].Direction = ParameterDirection.InputOutput;
                cmd.Transaction = transaction;
                cmd.ExecuteNonQuery();
                message = cmd.Parameters["Msg"].Value.ToString();
                transaction.Commit();
            }
            catch (Exception ex1)
            {
                transaction.Rollback();
                message = "Something went wrong";
            }
            finally
            {
                DisposeConnection();
            }
            return message;
        }
        #endregion


        #region Event

        public string InsertEvent(EventModel model)
        {
            string message = "";
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProcInsertEvent";

                cmd.Parameters.AddWithValue("@eventID", model.Event_Id);
                cmd.Parameters.AddWithValue("@eventName", model.Event_Name);
                cmd.Parameters.AddWithValue("@eventCreation", model.Event_Creation_date);
                cmd.Parameters.AddWithValue("@eventOpeningDate", model.Event_Opening_date);
                cmd.Parameters.AddWithValue("@eventClosingDate", model.Event_Closing_date);
                cmd.Parameters.AddWithValue("@description", model.Event_Description);
                cmd.Parameters.AddWithValue("@eventType", model.Event_Type);

                cmd.ExecuteNonQuery();
                transaction.Commit();
                message = "Success";
            }
            catch (Exception)
            {
                transaction.Rollback();
                message = "Something went wrong";
            }
            finally
            {
                DisposeConnection();
            }

            return message;
        }



        #endregion


        #region SpecialSession 
        public string InsertSession(SpecialSessionModel ss)
        {
            string message = "";
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_InsertSession";
                cmd.Parameters.AddWithValue("@SSName", ss.SSName);
                cmd.Parameters.AddWithValue("@MemberID", ss.MemberID);
                cmd.Parameters.AddWithValue("@TrackID", ss.TrackID);
                cmd.Parameters.AddWithValue("@Mobile", ss.Mobile);
                cmd.Parameters.AddWithValue("@Email", ss.Email);
                cmd.Parameters.AddWithValue("@Org", ss.Organization);
                cmd.Parameters.AddWithValue("@Topic", ss.Topic);

                cmd.ExecuteNonQuery();
                transaction.Commit();
                message = "Success";

            }
            catch (Exception)
            {
                transaction.Rollback();
                message = "Something went wrong";
            }
            finally
            {
                DisposeConnection();
            }
            return message;
        }

        #endregion


        #region CheckBioDataExists

        public string GetUserDetails(int memberId, out UserModel user)
        {
            string message = "";
            user = null;
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_GetUserDetails"; // Calling stored procedure
                cmd.Parameters.AddWithValue("@MemberID", memberId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            MemberID = Convert.ToInt32(reader["MemberID"]),
                            BioDataPath = reader["BioDataPath"] != DBNull.Value ? reader["BioDataPath"].ToString() : null
                        };
                        message = "Success";
                    }
                    else
                    {
                        message = "User not found";
                    }
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                message = "Something went wrong";
            }
            finally
            {
                DisposeConnection();
            }
            return message;
        }


        #endregion

      




        #region News
        public string InsertNews(NewsModel news)
        {
            string message = "";
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProcInsertNews";
                cmd.Parameters.AddWithValue("@Headline", news.Headline);
                cmd.Parameters.AddWithValue("@Details", news.Details);
                cmd.Parameters.AddWithValue("@NewsDate", news.NewsDate);
                cmd.Parameters.AddWithValue("@FromDate", news.FromDate);
                cmd.Parameters.AddWithValue("@ClosingDate", news.ClosingDate);
                cmd.Parameters.AddWithValue("@Event", news.Event);
                cmd.Parameters.AddWithValue("@FilePath", news.FilePath);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                message = "Success";

            }


            catch (Exception ex)
            {
                transaction.Rollback();
                message = "Error: " + ex.Message;
            }
            finally
            {
                DisposeConnection();
            }

            return message;
        }
        public List<NewsModel> GetNews()
        {
            List<NewsModel> newsList = new List<NewsModel>();
            OpenConnection();
            SqlCommand cmd = new SqlCommand("SELECT NewsId, Headline, NewsDate, FilePath FROM News", con);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    newsList.Add(new NewsModel
                    {
                        NewsId = Convert.ToInt32(reader["NewsId"]),
                        Headline = reader["Headline"].ToString(),
                        NewsDate = Convert.ToDateTime(reader["NewsDate"]),
                        FilePath = reader["FilePath"].ToString()
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                DisposeConnection();
            }

            return newsList;
        }
    }
    #endregion
    
}

     
