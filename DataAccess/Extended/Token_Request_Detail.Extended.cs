using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.IO;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Token_Request_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Token_Request_DetailList : List<Token_Request_Detail>
    { }

    /// <summary>
    /// Data access class for Token_Request_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Token_Request_Detail
    {

        #region "Enum"

        /// <summary>
        /// Enum for token request type 
        /// </summary>
        public enum RequestTypes
        {
            Email = 0,
            SMS = 1,
            System = 2
        }

        #endregion

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Token_Request_Detail as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Token_Request_DetailList SelectList(DataSet ds)
        {
            Token_Request_DetailList lstToken_Request_Detail = new Token_Request_DetailList();
            Token_Request_Detail objToken_Request_Detail = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objToken_Request_Detail = new Token_Request_Detail();
                    objToken_Request_Detail.MakeObject(dr);
                    lstToken_Request_Detail.Add(objToken_Request_Detail);
                }
            }
            return lstToken_Request_Detail;
        }

        /// <summary>
        /// Insert Token Request
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="logFilePath"></param>
        public static DataSet InsertTokenRequestDetail(DataTable dt, string logFilePath, string connectionString)
        {
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(logFilePath))
                File.AppendAllText(logFilePath, "-----Inserting Token Request into Database.-----" + System.Environment.NewLine);

            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                cmd = new SqlCommand("TokenRequest_InsertTokenRequest", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramToken = new SqlParameter("@TokenRequest", SqlDbType.Structured);
                paramToken.Value = dt;
                paramToken.TypeName = "dbo.TokenRequest";
                cmd.Parameters.Add(paramToken);

                cmd.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(logFilePath))
                    File.AppendAllText(logFilePath, "----- Error occurs while inserting Token Requests -----" + ex.Message + System.Environment.NewLine);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                conn.Close();
            }

            return ds;
        }


        /// <summary>
        /// returns current token period
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataTable GetRequestedTokens(DateTime fromDate, DateTime toDate)
        {
            DataTable dt = null;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("TokenRequest_GetRequestedToken");
                db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, ConvertTo.SetDateDBNull(fromDate));
                db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ConvertTo.SetDateDBNull(toDate));

                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                    dbCommand = null;
                }
                if (db != null)
                    db = null;
            }

            return dt;
        }

        public static DataTable CheckTokenRequestCloseDays()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("CheckTokenRequestCloseDays");             

                DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                    dbCommand = null;
                }
                if (db != null)
                    db = null;
            }
        }

        public static DataTable CheckTokenRequestCloseDays(string logFilePath, string connectionString)
        {
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(logFilePath))
                File.AppendAllText(logFilePath, "-----Inserting Token Request into Database.-----" + System.Environment.NewLine);

            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                cmd = new SqlCommand("CheckTokenRequestCloseDays", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(logFilePath))
                    File.AppendAllText(logFilePath, "----- Error occurs while inserting Token Requests -----" + ex.Message + System.Environment.NewLine);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                conn.Close();
            }

            return ds.Tables[0];
        }

        public static DataTable CheckTokenRequestCloseDays(string logFilePath, string connectionString, string message, string sender, int requestType)
        {
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(logFilePath))
                File.AppendAllText(logFilePath, "-----Inserting Token Request into Database.-----" + System.Environment.NewLine);

            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                cmd = new SqlCommand("CheckTokenRequestCloseDays", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param1 = new SqlParameter("@Message", SqlDbType.VarChar);
                param1.Value = message;
                cmd.Parameters.Add(param1);

                SqlParameter param2 = new SqlParameter("@Sender", SqlDbType.VarChar);
                param2.Value = sender;
                cmd.Parameters.Add(param2);

                SqlParameter param3 = new SqlParameter("@RequestType", SqlDbType.Int);
                param3.Value = requestType;
                cmd.Parameters.Add(param3);

                cmd.Connection.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(logFilePath))
                    File.AppendAllText(logFilePath, "----- Error occurs while inserting Token Requests -----" + ex.Message + System.Environment.NewLine);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                conn.Close();
            }

            return ds.Tables[0];
        }
        #endregion
    }
}
