using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for House table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class HouseList : List<House>
    { }

    /// <summary>
    /// Data access class for House table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class House
    {

        #region Enums
        public enum AssetTypeNames
        {
            NonCore = 2,
            Core = 1
        }

        public enum ElectricityTypes
        {
            PostPaid,
            PrePaid
        }

        #endregion

        #region Methods/Functions

        /// <summary>
        /// Give the List object of House as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static HouseList SelectList(DataSet ds)
        {
            HouseList lstHouse = new HouseList();
            House objHouse = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objHouse = new House();
                    objHouse.MakeObject(dr);
                    lstHouse.Add(objHouse);
                }
            }
            return lstHouse;
        }

        /// <summary>
        /// returns Accounts
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetAccounts(string searchText)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetAccounts");
                db.AddInParameter(dbCommand, "searchText", DbType.String, searchText);
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
        /// returns Accounts
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetAllAccounts(string searchText)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetAllAccounts");
                db.AddInParameter(dbCommand, "searchText", DbType.String, searchText);
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

        public static DataTable GetAccountView(bool IsRental)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetAccountView");
                db.AddInParameter(dbCommand, "IsRental", DbType.Boolean, IsRental);
                return db.ExecuteDataSet(dbCommand).Tables[0];
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

        public static DataTable GetAccountViewforToken()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GETAccountsForTokenSheet");
                return db.ExecuteDataSet(dbCommand).Tables[0];
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
        /// returns all reords
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAll()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("House_GetAll");

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataTable dt = null;
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
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

        public static DataTable InvoiceCompare(int HouseID, int month, int Year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("InvoiceCompare");
                db.AddInParameter(dbCommand, "@HouseID", DbType.Int32, HouseID);
                db.AddInParameter(dbCommand, "@Month", DbType.Int32, month);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, Year);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataTable dt = null;
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
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

        /// <summary>
        /// Get all data by month and year for specific prepaid condition
        /// </summary>
        /// <param name="month"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public static DataTable GetDataForTokenSheet()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("House_DataForTokenSheet");

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataTable dt = null;
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
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

        /// <summary>
        /// Get accounts by employee number
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static DataTable GetAccountsByEmployeeNumber(string employeeNo)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("House_GetAccountByEmployeeNumber");
                db.AddInParameter(dbCommand, "EmployeeNo", DbType.String, employeeNo);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataTable dt = null;
                if (ds != null)
                {
                    dt = ds.Tables[0];
                }
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

        public static int GetForRelatedEmployee(int HouseId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_House_CheckForEmployeeByID");
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, HouseId);
                int EmployeeId = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                return EmployeeId;
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
        public static string DeActivate(int houseId, bool IsActive)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseDeActive"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "IsActive", DbType.Boolean, IsActive);
                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "House is already in use. You can not delete this House.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }
        #endregion
    }


}
