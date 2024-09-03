using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Employee table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class EmployeeList : List<Employee>
    { }

    /// <summary>
    /// Data access class for Employee table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class Employee
    {
        #region "Enum"

        /// <summary>
        /// enum for employeetype
        /// </summary>
        public enum EmployeeTypeName
        {
            Package = 1,
            Bargaining = 2,
            Exco = 3
        }

        #endregion

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Employee as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static EmployeeList SelectList(DataSet ds)
        {
            EmployeeList lstEmployee = new EmployeeList();
            Employee objEmployee = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objEmployee = new Employee();
                    objEmployee.MakeObject(dr);
                    lstEmployee.Add(objEmployee);
                }
            }
            return lstEmployee;
        }

        /// <summary>
        /// returns Employees
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetEmployees(string searchText)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetEmployees");
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
        /// returns Employees
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetAllEmployees(string searchText)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetAllEmployees");
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

        public static DataTable GetEmployeeView()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetEmployeeView");
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
        /// Returns all aemployee
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectAllEmployee()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeSelectAll");
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
        /// Return employee profile
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeDetails(string username)
        {
            DataTable dt = null;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("Employee_GetDetails");
                db.AddInParameter(dbCommand, "UserName", DbType.String, username);

                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds != null && ds.Tables.Count > 0)
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
        /// Get accounts by employee number
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static DataTable GetEmployeeNumber(string employeeNo)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("Employee_GetEmployeeNumber");
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

        /// <summary>
        /// Get employee name
        /// </summary>
        /// <param name="employeeNo"></param>
        /// <returns></returns>
        public static string GetEmployeeName(int employeeid)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("Employee_GetEmployeeName");
                db.AddInParameter(dbCommand, "employeeid", DbType.Int32, employeeid);
                string employeename = Convert.ToString(db.ExecuteScalar(dbCommand));
                return employeename;
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
        /// deactivate a record from the Employee table by a composite primary key.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>String</returns>
        public static string DeActivate(int employeeId, bool IsActive)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeDeActivate"))
            {
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                db.AddInParameter(dbCommand, "IsActive", DbType.Boolean, IsActive);
                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Employee is already in use. You can not delete this Employee.";
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
