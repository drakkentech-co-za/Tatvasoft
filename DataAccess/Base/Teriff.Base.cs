using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Teriff table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>11-Sep-2013</CreatedDate>
    public partial class Teriff
    {
        #region Fields

        private int teriffId;
        private string categoryName;
        private string categoryValue;
        private bool bIncludeVAT;
        private int categoryType;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Teriff class.
        /// </summary>
        public Teriff()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Teriff class.
        /// </summary>
        public Teriff(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Teriff class.
        /// </summary>
        public Teriff(int teriffId)
        {
            DataSet ds = Select(teriffId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TeriffId value.
        /// </summary>
        public virtual int TeriffId
        {
            get { return teriffId; }
            set { teriffId = value; }
        }

        /// <summary>
        /// Gets or sets the CategoryName value.
        /// </summary>
        public virtual string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        /// <summary>
        /// Gets or sets the CategoryValue value.
        /// </summary>
        public virtual string CategoryValue
        {
            get { return categoryValue; }
            set { categoryValue = value; }
        }

        /// <summary>
        /// Gets or sets the BIncludeVAT value.
        /// </summary>
        public virtual bool BIncludeVAT
        {
            get { return bIncludeVAT; }
            set { bIncludeVAT = value; }
        }

        /// <summary>
        /// Gets or sets the CategoryType value.
        /// </summary>
        public virtual int CategoryType
        {
            get { return categoryType; }
            set { categoryType = value; }
        }

        /// <summary>
        /// Gets or sets the Created_TS value.
        /// </summary>
        public virtual DateTime Created_TS
        {
            get { return created_TS; }
            set { created_TS = value; }
        }

        /// <summary>
        /// Gets or sets the Created_UserId value.
        /// </summary>
        public virtual int Created_UserId
        {
            get { return created_UserId; }
            set { created_UserId = value; }
        }

        /// <summary>
        /// Gets or sets the Updated_TS value.
        /// </summary>
        public virtual DateTime Updated_TS
        {
            get { return updated_TS; }
            set { updated_TS = value; }
        }

        /// <summary>
        /// Gets or sets the Updated_UserId value.
        /// </summary>
        public virtual int Updated_UserId
        {
            get { return updated_UserId; }
            set { updated_UserId = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.categoryName = string.Empty;
            this.categoryValue = string.Empty;
            this.bIncludeVAT = false;
            this.categoryType = 0;
            this.created_TS = DateTime.MinValue;
            this.created_UserId = 0;
            this.updated_TS = DateTime.MinValue;
            this.updated_UserId = 0;
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
            if (dt.Columns.Contains("TeriffId"))
            {
                int.TryParse(Convert.ToString(dr["TeriffId"]), out teriffId);
            }
            if (dt.Columns.Contains("CategoryName"))
            {
                categoryName = Convert.ToString(dr["CategoryName"]);
            }
            if (dt.Columns.Contains("CategoryValue"))
            {
                categoryValue = Convert.ToString(dr["CategoryValue"]);
            }
            if (dt.Columns.Contains("bIncludeVAT"))
            {
                bool.TryParse(Convert.ToString(dr["bIncludeVAT"]), out bIncludeVAT);
            }
            if (dt.Columns.Contains("CategoryType"))
            {
                int.TryParse(Convert.ToString(dr["CategoryType"]), out categoryType);
            }
            if (dt.Columns.Contains("Created_TS"))
            {
                DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS);
            }
            if (dt.Columns.Contains("Created_UserId"))
            {
                int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId);
            }
            if (dt.Columns.Contains("Updated_TS"))
            {
                DateTime.TryParse(Convert.ToString(dr["Updated_TS"]), out updated_TS);
            }
            if (dt.Columns.Contains("Updated_UserId"))
            {
                int.TryParse(Convert.ToString(dr["Updated_UserId"]), out updated_UserId);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Teriff table.
        /// </summary>
        public void Save()
        {
            if (teriffId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Teriff table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_TeriffInsert"))
            {
                db.AddInParameter(dbCommand, "CategoryName", DbType.String, categoryName);
                db.AddInParameter(dbCommand, "CategoryValue", DbType.String, categoryValue);
                db.AddInParameter(dbCommand, "bIncludeVAT", DbType.Boolean, bIncludeVAT);
                db.AddInParameter(dbCommand, "CategoryType", DbType.Int32, categoryType);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                teriffId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Teriff table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_TeriffUpdate"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                db.AddInParameter(dbCommand, "CategoryName", DbType.String, categoryName);
                db.AddInParameter(dbCommand, "CategoryValue", DbType.String, categoryValue);
                db.AddInParameter(dbCommand, "bIncludeVAT", DbType.Boolean, bIncludeVAT);
                db.AddInParameter(dbCommand, "CategoryType", DbType.Int32, categoryType);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Teriff table by a composite primary key.
        /// </summary>
        /// <param name="teriffId"></param>
        /// <returns>String</returns>
        public static string Delete(int teriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_TeriffDelete"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Teriff is already in use. You can not delete this Teriff.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the Teriff table.
        /// </summary>
        /// <param name="teriffId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int teriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_TeriffSelect"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the Teriff table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static TeriffList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_TeriffSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Teriff table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public TeriffList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_TeriffSearch"))
            {
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                if (categoryName != string.Empty)
                    db.AddInParameter(dbCommand, "CategoryName", DbType.String, categoryName);
                if (categoryValue != string.Empty)
                    db.AddInParameter(dbCommand, "CategoryValue", DbType.String, categoryValue);
                if (bIncludeVAT != null)
                    db.AddInParameter(dbCommand, "bIncludeVAT", DbType.Boolean, bIncludeVAT);
                if (categoryType > 0)
                    db.AddInParameter(dbCommand, "CategoryType", DbType.Int32, categoryType);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                if (created_UserId > 0)
                    db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                if (updated_UserId > 0)
                    db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
