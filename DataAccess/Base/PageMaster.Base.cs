using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for PageMaster table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>26-Sep-2013</CreatedDate>
    public partial class PageMaster
    {
        #region Fields

        private int pageId;
        private string pageUrl;
        private string pageAlias;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PageMaster class.
        /// </summary>
        public PageMaster()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the PageMaster class.
        /// </summary>
        public PageMaster(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the PageMaster class.
        /// </summary>
        public PageMaster(int pageId)
        {
            DataSet ds = Select(pageId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PageId value.
        /// </summary>
        public virtual int PageId
        {
            get { return pageId; }
            set { pageId = value; }
        }

        /// <summary>
        /// Gets or sets the PageUrl value.
        /// </summary>
        public virtual string PageUrl
        {
            get { return pageUrl; }
            set { pageUrl = value; }
        }

        /// <summary>
        /// Gets or sets the PageAlias value.
        /// </summary>
        public virtual string PageAlias
        {
            get { return pageAlias; }
            set { pageAlias = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.pageUrl = string.Empty;
            this.pageAlias = string.Empty;
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
            if (dt.Columns.Contains("PageId"))
            {
                int.TryParse(Convert.ToString(dr["PageId"]), out pageId);
            }
            if (dt.Columns.Contains("PageUrl"))
            {
                pageUrl = Convert.ToString(dr["PageUrl"]);
            }
            if (dt.Columns.Contains("PageAlias"))
            {
                pageAlias = Convert.ToString(dr["PageAlias"]);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_PageMaster table.
        /// </summary>
        public void Save()
        {
            if (pageId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the PageMaster table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageMasterInsert"))
            {
                db.AddInParameter(dbCommand, "PageUrl", DbType.String, pageUrl);
                db.AddInParameter(dbCommand, "PageAlias", DbType.String, pageAlias);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                pageId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the PageMaster table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageMasterUpdate"))
            {
                db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);
                db.AddInParameter(dbCommand, "PageUrl", DbType.String, pageUrl);
                db.AddInParameter(dbCommand, "PageAlias", DbType.String, pageAlias);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the PageMaster table by a composite primary key.
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>String</returns>
        public static string Delete(int pageId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageMasterDelete"))
            {
                db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "PageMaster is already in use. You can not delete this PageMaster.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the PageMaster table.
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int pageId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageMasterSelect"))
            {
                db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the PageMaster table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static PageMasterList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageMasterSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the PageMaster table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public PageMasterList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_PageMasterSearch"))
            {
                if (pageId > 0)
                    db.AddInParameter(dbCommand, "PageId", DbType.Int32, pageId);
                if (pageUrl != string.Empty)
                    db.AddInParameter(dbCommand, "PageUrl", DbType.String, pageUrl);
                if (pageAlias != string.Empty)
                    db.AddInParameter(dbCommand, "PageAlias", DbType.String, pageAlias);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
