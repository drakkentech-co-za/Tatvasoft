using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Token_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Token_DetailList : List<Token_Detail>
    { }

    /// <summary>
    /// Data access class for Token_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Token_Detail
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Token_Detail as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Token_DetailList SelectList(DataSet ds)
        {
            Token_DetailList lstToken_Detail = new Token_DetailList();
            Token_Detail objToken_Detail = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objToken_Detail = new Token_Detail();
                    objToken_Detail.MakeObject(dr);
                    lstToken_Detail.Add(objToken_Detail);
                }
            }
            return lstToken_Detail;
        }

        /// <summary>
        /// Inserts Bulk Insert into TokenDetail
        /// </summary>
        /// <param name="dtTokenData"></param>
        /// <param name="createdBy"></param>
        public static void BulkInsertToken(DataTable dtTokenData, int createdBy)
        {
            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            try
            {
                cmd = new SqlCommand("TokenDetail_Insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramToken = new SqlParameter("@Tokens", SqlDbType.Structured);
                paramToken.Value = dtTokenData;
                paramToken.TypeName = "dbo.TokenDetails";
                cmd.Parameters.Add(paramToken);

                SqlParameter paramCreatedBy = new SqlParameter("@CreatedBy", SqlDbType.Int);
                paramCreatedBy.Value = createdBy;
                cmd.Parameters.Add(paramCreatedBy);

                cmd.Connection.Open();
                int i = ConvertTo.Integer(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                conn.Close();
            }
        }

        public static int InsertManualToken(int HouseId, int TokenUnits, string TokenNumber, int UserId,string MeterNo)
        {
            int retValue = 0;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_InsertManualToken");
                db.AddInParameter(dbCommand, "@HouseId", DbType.Int32, HouseId);
                db.AddInParameter(dbCommand, "@TokenUnits", DbType.Int32, TokenUnits);
                db.AddInParameter(dbCommand, "@TokenNumber", DbType.String, TokenNumber);
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, UserId);
                db.AddInParameter(dbCommand, "@MeterNo", DbType.String, MeterNo);
                retValue = ConvertTo.Integer(db.ExecuteScalar(dbCommand));
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
            return retValue;
        }
        #endregion
    }
}
