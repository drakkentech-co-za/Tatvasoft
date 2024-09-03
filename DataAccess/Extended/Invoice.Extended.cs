using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Invoice table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>10-Sep-2013</CreatedDate>
    public partial class InvoiceList : List<Invoice>
    { }

    /// <summary>
    /// Data access class for Invoice table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>10-Sep-2013</CreatedDate>
    public partial class Invoice
    {
        #region "Enum"

        /// <summary>
        /// used in invoicecompare page for filter parameter
        /// </summary>
        public enum InvoiceFilterParameter
        {
            Matched = 1,
            Unmatched = 2,
            Accepted = 3,
            Pending = 4,
            Rental = 5
        }

        #endregion

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Invoice as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static InvoiceList SelectList(DataSet ds)
        {
            InvoiceList lstInvoice = new InvoiceList();
            Invoice objInvoice = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objInvoice = new Invoice();
                    objInvoice.MakeObject(dr);
                    lstInvoice.Add(objInvoice);
                }
            }
            return lstInvoice;
        }

        /// <summary>
        /// Returns invoice compare
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static DataSet GetInvoiceCompare(int? houseId, int period, int filterParameter, int startRowIndex, int MaxRows, ref int TotalCount)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("InvoiceCompare");
                db.AddInParameter(dbCommand, "HouseID", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "Period", DbType.Int32, period);
                db.AddInParameter(dbCommand, "MatchFilter", DbType.Int32, filterParameter);
                db.AddInParameter(dbCommand, "StartRowIndex", DbType.Int32, startRowIndex);
                db.AddInParameter(dbCommand, "maximumRows", DbType.Int32, MaxRows);
                db.AddOutParameter(dbCommand, "totalcount", DbType.Int32, 0);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                TotalCount = ConvertTo.Integer(dbCommand.Parameters[5].Value);
                return ds;
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

        /// <summary>
        /// Returns invoice
        /// </summary>
        /// <param name="houseId"></param>
        /// <param name="period"></param>
        /// <param name="filterParameter"></param>
        /// <returns></returns>
        public static DataSet GetInvoice(int? houseId, int period, int filterParameter)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("Invoice_GetInvoice");
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, period);
                db.AddInParameter(dbCommand, "MatchFilter", DbType.Int32, filterParameter);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds;
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
