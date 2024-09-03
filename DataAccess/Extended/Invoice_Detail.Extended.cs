using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Invoice_Detail table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>10-Sep-2013</CreatedDate>
    public partial class Invoice_DetailList : List<Invoice_Detail>
    { }

    /// <summary>
    /// Data access class for Invoice_Detail table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>10-Sep-2013</CreatedDate>
    public partial class Invoice_Detail
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Invoice_Detail as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Invoice_DetailList SelectList(DataSet ds)
        {
            Invoice_DetailList lstInvoice_Detail = new Invoice_DetailList();
            Invoice_Detail objInvoice_Detail = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objInvoice_Detail = new Invoice_Detail();
                    objInvoice_Detail.MakeObject(dr);
                    lstInvoice_Detail.Add(objInvoice_Detail);
                }
            }
            return lstInvoice_Detail;
        }

        public static void InvoiceDetails(DataTable dtDetails, bool isAccepted, int userid, int periodId, bool isRAmountAccepted, string reason = "")
        {
            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            try
            {
                cmd = new SqlCommand("InsertInvoiceDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter invDetail = cmd.Parameters.AddWithValue("InvoiceDetails", dtDetails);
                invDetail.SqlDbType = SqlDbType.Structured;
                invDetail.TypeName = "dbo.InvoiceFields";

                cmd.Parameters.AddWithValue("IsAccepted", isAccepted);
                cmd.Parameters.AddWithValue("UserId", userid);
                cmd.Parameters.AddWithValue("PeriodId", periodId);
                cmd.Parameters.AddWithValue("Reason", reason);
                cmd.Parameters.AddWithValue("IsRAmountAccepted", isRAmountAccepted);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                conn.Close();
            }


            //Database db = null;
            //DbCommand dbCommand = null;
            //try
            //{
            //    db = DatabaseFactory.CreateDatabase();
            //    dbCommand = db.GetStoredProcCommand("InsertInvoiceDetails");
            //    db.AddInParameter(dbCommand, "@InvoiceDetails",SqlDbType.Structured,dtDetails);

            //    return db.ExecuteDataSet(dbCommand);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    if (dbCommand != null)
            //    {
            //        dbCommand.Dispose();
            //        dbCommand = null;
            //    }
            //    if (db != null)
            //        db = null;
            //}
        }

        /// <summary>
        /// GetInvoice detail for invoiceid in particular month
        /// </summary>
        /// <param name="month"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public static DataSet GetInvoiceDetail(int PeriodId, int HouseId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("InvoiceDetail_GetInvoiceDetail");
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, PeriodId);
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, HouseId);

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

        /// <summary>
        /// GetInvoice detail for PrePaid Elec and Other
        /// </summary>
        /// <param name="month"></param>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public static DataSet GetPrePaidElecAndOther(int PeriodId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("InvoiceDetail_PrepaidElecAndOther");
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, PeriodId);

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
