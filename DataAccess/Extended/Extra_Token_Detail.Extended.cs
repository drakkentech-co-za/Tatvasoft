using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for tbl_Extra_Token_Details table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>31-Jan-2014</CreatedDate>
    public partial class Extra_Token_DetailList : List<Extra_Token_Detail>
    { }

    /// <summary>
    /// Data access class for tbl_Extra_Token_Details table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>31-Jan-2014</CreatedDate>
    public partial class Extra_Token_Detail
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of tbl_Extra_Token_Details as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Extra_Token_DetailList SelectList(DataSet ds)
        {
            Extra_Token_DetailList lsttbl_Extra_Token_Details = new Extra_Token_DetailList();
            Extra_Token_Detail objtbl_Extra_Token_Details = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objtbl_Extra_Token_Details = new Extra_Token_Detail();
                    objtbl_Extra_Token_Details.MakeObject(dr);
                    lsttbl_Extra_Token_Details.Add(objtbl_Extra_Token_Details);
                }
            }
            return lsttbl_Extra_Token_Details;
        }

        public static int InsertExtraTokenDetails(int HouseId, int EmployeeId, int NoOfUnits, string TokenNumber, int PeriodId, string MeterNumber, DateTime DateIssue, int created_UserId, int TokenRequestId)
        {
            int retValue = 0;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_InsertExtraTokenDetails");
                db.AddInParameter(dbCommand, "@HouseId", DbType.Int32, HouseId);
                db.AddInParameter(dbCommand, "@EmployeeId", DbType.Int32, EmployeeId);
                db.AddInParameter(dbCommand, "@NoOfUnits", DbType.Int32, NoOfUnits);
                db.AddInParameter(dbCommand, "@TokenNumber", DbType.String, TokenNumber);
                db.AddInParameter(dbCommand, "@MeterNumber", DbType.String, MeterNumber);
                db.AddInParameter(dbCommand, "@PeriodId", DbType.Int32, PeriodId);
                if (DateIssue != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "@DateIssue", DbType.DateTime, DateIssue);
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, created_UserId);
                db.AddInParameter(dbCommand, "@TokenRequestId", DbType.Int32, TokenRequestId);
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

        /// <summary>
        /// returns data for grid in extra token detail page
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetGridData(int intPeriodId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_ExtraToken_GetGridData");
                db.AddInParameter(dbCommand, "intPeriodId", DbType.Int32, intPeriodId);

                return db.ExecuteDataSet(dbCommand);
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
        #endregion
    }
}
