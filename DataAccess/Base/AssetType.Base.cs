using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for AssetType table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>13-Sep-2013</CreatedDate>
    public partial class AssetType
    {
        #region Fields

        private int assetTypeId;
        private string assetTypeName;
		private bool isDefault;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AssetType class.
        /// </summary>
        public AssetType()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the AssetType class.
        /// </summary>
        public AssetType(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the AssetType class.
        /// </summary>
        public AssetType(int assetTypeId)
        {
            DataSet ds = Select(assetTypeId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the AssetTypeId value.
        /// </summary>
        public virtual int AssetTypeId
        {
            get { return assetTypeId; }
            set { assetTypeId = value; }
        }

        /// <summary>
        /// Gets or sets the AssetTypeName value.
        /// </summary>
        public virtual string AssetTypeName
        {
            get { return assetTypeName; }
            set { assetTypeName = value; }
        }

        /// <summary>
		/// Gets or sets the IsDefault value.
		/// </summary>
		public virtual bool IsDefault
		{
			get { return isDefault; }
			set { isDefault = value; }
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
            this.assetTypeName = string.Empty;
			this.isDefault = false;
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
            if (dt.Columns.Contains("AssetTypeId"))
            {
                int.TryParse(Convert.ToString(dr["AssetTypeId"]), out assetTypeId);
            }
            if (dt.Columns.Contains("AssetTypeName"))
            {
                assetTypeName = Convert.ToString(dr["AssetTypeName"]);
			}
			if (dt.Columns.Contains("IsDefault"))
			{
				bool.TryParse(Convert.ToString(dr["IsDefault"]), out isDefault );
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
        /// Saves a record to the tbl_AssetType table.
        /// </summary>
        public void Save()
        {
            if (assetTypeId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the AssetType table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_AssetTypeInsert"))
            {
                db.AddInParameter(dbCommand, "AssetTypeName", DbType.String, assetTypeName);
				db.AddInParameter(dbCommand, "IsDefault", DbType.Boolean, isDefault);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                assetTypeId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the AssetType table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_AssetTypeUpdate"))
            {
                db.AddInParameter(dbCommand, "AssetTypeId", DbType.Int32, assetTypeId);
                db.AddInParameter(dbCommand, "AssetTypeName", DbType.String, assetTypeName);
				db.AddInParameter(dbCommand, "IsDefault", DbType.Boolean, isDefault);
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
        /// Deletes a record from the AssetType table by a composite primary key.
        /// </summary>
        /// <param name="assetTypeId"></param>
        /// <returns>String</returns>
        public static string Delete(int assetTypeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_AssetTypeDelete"))
            {
                db.AddInParameter(dbCommand, "AssetTypeId", DbType.Int32, assetTypeId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "AssetType is already in use. You can not delete this AssetType.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the AssetType table.
        /// </summary>
        /// <param name="assetTypeId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int assetTypeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_AssetTypeSelect"))
            {
                db.AddInParameter(dbCommand, "AssetTypeId", DbType.Int32, assetTypeId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the AssetType table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static AssetTypeList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_AssetTypeSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the AssetType table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public AssetTypeList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_AssetTypeSearch"))
            {
                if (assetTypeId > 0)
                    db.AddInParameter(dbCommand, "AssetTypeId", DbType.Int32, assetTypeId);
                if (assetTypeName != string.Empty)
                    db.AddInParameter(dbCommand, "AssetTypeName", DbType.String, assetTypeName);
				if(isDefault != null) 
					db.AddInParameter(dbCommand, "IsDefault", DbType.Boolean, isDefault);
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
