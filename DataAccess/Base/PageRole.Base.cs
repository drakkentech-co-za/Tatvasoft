using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for PageRole table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>27-Sep-2013</CreatedDate>
    public partial class PageRole
    {
        #region Fields

        private int pageRoleId;
        private int pageId;
        private int roleId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PageRole class.
        /// </summary>
        public PageRole()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the PageRole class.
        /// </summary>
        public PageRole(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the PageRole class.
        /// </summary>
        public PageRole(int pageRoleId)
        {
            DataSet ds = Select(pageRoleId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PageRoleId value.
        /// </summary>
        public virtual int PageRoleId
        {
            get { return pageRoleId; }
            set { pageRoleId = value; }
        }

        /// <summary>
        /// Gets or sets the PageId value.
        /// </summary>
        public virtual int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        /// <summary>
        /// Gets or sets the RoleId value.
        /// </summary>
        public virtual int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.pageId = 0;
            this.roleId = 0;
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
            if (dt.Columns.Contains("PageRoleId"))
            {
                int.TryParse(Convert.ToString(dr["PageRoleId"]), out pageRoleId);
            }
            if (dt.Columns.Contains("PageId"))
            {
                int.TryParse(Convert.ToString(dr["PageId"]), out pageId);
            }
            if (dt.Columns.Contains("RoleId"))
            {
                int.TryParse(Convert.ToString(dr["RoleId"]), out roleId);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_PageRole table.
        /// </summary>
        public void Save()
        {
            if (pageRoleId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the PageRole table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleInsert"))
            {
                db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);
                if (roleId > 0)
                    db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                pageRoleId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the PageRole table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleUpdate"))
            {
                db.AddInParameter(dbCommand, "PageRoleId", DbType.Int32, pageRoleId);
                db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);
                if (roleId > 0)
                    db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the PageRole table by a composite primary key.
        /// </summary>
        /// <param name="pageRoleId"></param>
        /// <returns>String</returns>
        public static string Delete(int pageRoleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleDelete"))
            {
                db.AddInParameter(dbCommand, "PageRoleId", DbType.Int32, pageRoleId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "PageRole is already in use. You can not delete this PageRole.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the PageRole table by a foreign key.
        /// </summary>
        /// <param name="roleId"></param>
        public static void DeleteByRoleId(int roleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleDeleteByRoleId"))
            {
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the PageRole table.
        /// </summary>
        /// <param name="pageRoleId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int pageRoleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleSelect"))
            {
                db.AddInParameter(dbCommand, "PageRoleId", DbType.Int32, pageRoleId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the PageRole table by a foreign key.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>List</returns>
        public static PageRoleList SelectByRoleId(int roleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleSelectByRoleId"))
            {
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the PageRole table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static PageRoleList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the PageRole table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public PageRoleList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageRoleSearch"))
            {
                if (pageRoleId > 0)
                    db.AddInParameter(dbCommand, "PageRoleId", DbType.Int32, pageRoleId);
                if (pageId > 0)
                    db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);
                if (roleId > 0)
                    db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
