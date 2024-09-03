using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Role table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>01-Oct-2013</CreatedDate>
    public partial class Role
    {
        #region Fields

        private int roleId;
        private string roleName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Role class.
        /// </summary>
        public Role()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Role class.
        /// </summary>
        public Role(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Role class.
        /// </summary>
        public Role(int roleId)
        {
            DataSet ds = Select(roleId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the RoleId value.
        /// </summary>
        public virtual int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        /// <summary>
        /// Gets or sets the RoleName value.
        /// </summary>
        public virtual string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.roleName = string.Empty;
        }

        /// <summary>
        /// Create object by DataSet
        /// </summary>
        /// <param name="ds"></param>
        private void MakeObject(DataSet ds)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MakeObject(ds.Tables[0].Rows[0]);
            }
            else
                InitVariables();
        }

        /// <summary>
        /// Create object by DataRow
        /// </summary>
        /// <param name="dr"></param>
        private void MakeObject(DataRow dr)
        {
            DataTable dt = dr.Table;
            if (dt.Columns.Contains("RoleId"))
            {
                int.TryParse(Convert.ToString(dr["RoleId"]), out roleId);
            }
            if (dt.Columns.Contains("RoleName"))
            {
                roleName = Convert.ToString(dr["RoleName"]);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Role table.
        /// </summary>
        public void Save()
        {
            if (roleId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Role table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_RoleInsert"))
            {
                db.AddInParameter(dbCommand, "RoleName", DbType.String, roleName);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                roleId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Role table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_RoleUpdate"))
            {
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);
                db.AddInParameter(dbCommand, "RoleName", DbType.String, roleName);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Role table by a composite primary key.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>String</returns>
        public static string Delete(int roleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_RoleDelete"))
            {
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Role is already in use. You can not delete this Role.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the Role table.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int roleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_RoleSelect"))
            {
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the Role table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static RoleList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_RoleSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Role table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public RoleList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_RoleSearch"))
            {
                if (roleId > 0)
                    db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);
                if (roleName != string.Empty)
                    db.AddInParameter(dbCommand, "RoleName", DbType.String, roleName);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
