using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Employee_House table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class Employee_HouseList : List<Employee_House>
    { }

    /// <summary>
    /// Data access class for Employee_House table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class Employee_House
    {
        #region Methods/Functions

        /// <summary>
        /// Give the List object of Employee_House as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Employee_HouseList SelectList(DataSet ds)
        {
            Employee_HouseList lstEmployee_House = new Employee_HouseList();
            Employee_House objEmployee_House = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objEmployee_House = new Employee_House();
                    objEmployee_House.MakeObject(dr);
                    lstEmployee_House.Add(objEmployee_House);
                }
            }
            return lstEmployee_House;
        }

        /// <summary>
        /// Insert records
        /// </summary>
        /// <param name="dtMain"></param>
        public static void SaveMapRecords(DataTable dtMapRecords, int employeeId, int createdBy)
        {
            SqlCommand cmd = null;
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            try
            {
                dtMapRecords.Columns.Add("EmployeeId");
                dtMapRecords.Columns.Add("CreatedBy");
                dtMapRecords.Columns.Add("CreatedDate");
                dtMapRecords.Columns.Add("UpdatedBy");
                dtMapRecords.Columns.Add("UpdatedDate");


                foreach (DataRow dr in dtMapRecords.Rows)
                {
                    dr["EmployeeId"] = employeeId;
                    dr["CreatedDate"] = DateTime.Now;
                    dr["CreatedBy"] = createdBy;
                    dr["UpdatedDate"] = null;
                    dr["UpdatedBy"] = null;
                }

                cmd = new SqlCommand("EmployeeHouse_SaveMapRecords", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] param = new SqlParameter[6];

                param[0] = cmd.Parameters.Add("@EmployeeId", SqlDbType.Int);
                param[0].SourceColumn = "EmployeeId";
                param[1] = cmd.Parameters.Add("@HouseId", SqlDbType.Int);
                param[1].SourceColumn = "HouseId";
                param[2] = cmd.Parameters.Add("@CreatedBy", SqlDbType.Int);
                param[2].SourceColumn = "CreatedBy";
                param[3] = cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime);
                param[3].SourceColumn = "CreatedDate";
                param[4] = cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime);
                param[4].SourceColumn = "UpdatedDate";
                param[5] = cmd.Parameters.Add("@UpdatedBy", SqlDbType.Int);
                param[5].SourceColumn = "UpdatedBy";

                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.UpdateCommand = cmd;
                da.Update(dtMapRecords);
                dtMapRecords.Dispose();
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
        }

        /// <summary>
        /// returns record by employee id
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataTable GetByEmployeeId(int employeeId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("EmployeeHouse_GetByEmployeeId");
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataTable dt = null;

                if (ds != null)
                    dt = ds.Tables[0];
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
        /// returns record by employee number
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataTable GetByEmployeeNumber(string employeeNumber)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("EmployeeHouse_GetByEmployeeNumber");
                db.AddInParameter(dbCommand, "EmployeeNumber", DbType.String, employeeNumber);

                DataSet ds = db.ExecuteDataSet(dbCommand);
                DataTable dt = null;

                if (ds != null)
                    dt = ds.Tables[0];
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

        #endregion
    }
}
