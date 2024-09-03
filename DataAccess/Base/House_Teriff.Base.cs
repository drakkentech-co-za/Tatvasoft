using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for House_Teriff table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>02-Sep-2013</CreatedDate>
    public partial class House_Teriff
    {
        #region Fields

        private int houseTeriffId;
        private int houseId;
        private int teriffId;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the House_Teriff class.
        /// </summary>
        public House_Teriff()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the House_Teriff class.
        /// </summary>
        public House_Teriff(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the House_Teriff class.
        /// </summary>
        public House_Teriff(int houseTeriffId)
        {
            DataSet ds = Select(houseTeriffId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HouseTeriffId value.
        /// </summary>
        public virtual int HouseTeriffId
        {
            get { return houseTeriffId; }
            set { houseTeriffId = value; }
        }

        /// <summary>
        /// Gets or sets the HouseId value.
        /// </summary>
        public virtual int HouseId
        {
            get { return houseId; }
            set { houseId = value; }
        }

        /// <summary>
        /// Gets or sets the TeriffIds value.
        /// </summary>
        public virtual int TeriffId
        {
            get { return teriffId; }
            set { teriffId = value; }
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
            this.houseId = 0;
            this.teriffId = 0;
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
            if (dt.Columns.Contains("HouseTeriffId"))
            {
                int.TryParse(Convert.ToString(dr["HouseTeriffId"]), out houseTeriffId);
            }
            if (dt.Columns.Contains("HouseId"))
            {
                int.TryParse(Convert.ToString(dr["HouseId"]), out houseId);
            }
            if (dt.Columns.Contains("TeriffId"))
            {
                teriffId = Convert.ToInt32(dr["TeriffId"]);
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
        /// Updates a record in the House_Teriff table.
        /// </summary>
        public void Update(string teriffIds)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_House_TeriffUpdate"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "TeriffIds", DbType.String, teriffIds);
                db.AddInParameter(dbCommand, "UserId", DbType.Int32, created_UserId);
                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the House_Teriff table.
        /// </summary>
        /// <param name="houseTeriffId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int houseTeriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_House_TeriffSelect"))
            {
                db.AddInParameter(dbCommand, "HouseTeriffId", DbType.Int32, houseTeriffId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the House_Teriff table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>List</returns>
        public static DataTable SelectByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_House_TeriffSelectByHouseId");
            db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        /// <summary>
        /// Selects all records from the House_Teriff table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static House_TeriffList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_House_TeriffSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }
        #endregion
    }
}
