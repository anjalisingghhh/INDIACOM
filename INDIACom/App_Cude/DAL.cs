using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using INDIACom.Models;
using System.Drawing;
using System.Reflection;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;

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
                cmd.CommandText = "ProcInsertSession";
                cmd.Parameters.AddWithValue("@SSName", ss.SSName);
                cmd.Parameters.AddWithValue("@Mobile", ss.Mobile);
                cmd.Parameters.AddWithValue("@Email", ss.Email);
                cmd.Parameters.AddWithValue("@Org", ss.Organization);
                cmd.Parameters.AddWithValue("@Topic", ss.Topic);
                cmd.Parameters.AddWithValue("@Request_Date", ss.Request_Date);
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

        #region file
        public string SaveFilePath(MemberDocumentModel doc)
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
                cmd.CommandText = "ProcSaveFilePath";

                cmd.Parameters.AddWithValue("@UserID", doc.UserID);
                cmd.Parameters.AddWithValue("@FilePath", doc.FilePath);

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

        #region User
        public string InsertUserDetails(MemberModel model)
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
                cmd.CommandText = "Proc_InsertDetails";
                cmd.Parameters.AddWithValue("@Salutation", model.Salutation);
                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Address", model.Address);
                cmd.Parameters.AddWithValue("@Country", model.Country);
                cmd.Parameters.AddWithValue("@CountryID", model.CountryID);
                cmd.Parameters.AddWithValue("@State", model.State);
                cmd.Parameters.AddWithValue("@StateID", model.StateID);
                cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Parameters.AddWithValue("@CityID", model.CityID);
                cmd.Parameters.AddWithValue("@Pincode", model.Pincode);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Mobile", model.Mobile);
                cmd.Parameters.AddWithValue("@Event", model.Event);
                cmd.Parameters.AddWithValue("@CSI_No", model.CSI_No);
                cmd.Parameters.AddWithValue("@IEEE_No", model.IEEE_No);
                cmd.Parameters.AddWithValue("@Organisation", model.OrganisationName);
                cmd.Parameters.AddWithValue("@Category", model.Category);
                cmd.Parameters.AddWithValue("@Password", model.Password);

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

        public long GetMemberID(string email, long mobile)
        {
           long memberID = 0;
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_GetMemberID";
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Mobile", mobile);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    memberID = (long)result;
                }
            }
            catch (Exception)
            {
                
                transaction.Rollback();

            }
            finally
            {
                DisposeConnection();
            }
            return memberID;
        }

        public DataTable checkCredentials(LoginUserModel model)
        {
            DataTable ds = new DataTable();
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_CheckCredentials";
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@MemberID", model.UserID);
                cmd.Parameters.AddWithValue("@Password", model.Password);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception)
            {
                ds = new DataTable();
                transaction.Rollback();
                
            }
            finally
            {
                DisposeConnection();
            }
            return ds;
        }
        #endregion

        #region Organisation
        public string AddOrganisation(MemberModel model)
        {
            string result = "";
            OpenConnection();
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = con.BeginTransaction();

            try
            {
                cmd.Connection = con;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Proc_SaveOrganisation";
                cmd.Parameters.AddWithValue("@OrgEmail", model.OrgEmail);
                cmd.Parameters.AddWithValue("@OrgName", model.OrgName);
                cmd.Parameters.AddWithValue("@OrgShortName", model.OrgShortName);
                cmd.Parameters.AddWithValue("@OrgAddress", model.OrgAddress);
                cmd.Parameters.AddWithValue("@OrgContactPerson", model.OrgContactPerson);
                cmd.Parameters.AddWithValue("@OrgPhone", model.OrgPhone);
                cmd.ExecuteNonQuery();
                transaction.Commit();
                result = "Success";
            }
            catch (Exception)
            {

                transaction.Rollback();
                result = "Something went wrong";

            }
            finally
            {
                DisposeConnection();
            }
            return result;
        }

        #endregion
    }
}
