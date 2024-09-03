using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Teriff_Rates table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>09-Sep-2013</CreatedDate>
    public partial class Teriff_Rate
    {
        #region Fields

        private int teriffRateId;
        private int teriffId;
        private float teriffRate;
        private DateTime dateFrom;
        private DateTime dateTo;
        private int created_UserId;
        private DateTime created_TS;
        private int updated_UserId;
        private DateTime updated_TS;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Teriff_Rate class.
        /// </summary>
        public Teriff_Rate()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Teriff_Rate class.
        /// </summary>
        public Teriff_Rate(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Teriff_Rate class.
        /// </summary>
        public Teriff_Rate(int teriffRateId)
        {
            DataSet ds = Select(teriffRateId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TeriffRateId value.
        /// </summary>
        public virtual int TeriffRateId
        {
            get { return teriffRateId; }
            set { teriffRateId = value; }
        }

        /// <summary>
        /// Gets or sets the TeriffId value.
        /// </summary>
        public virtual int TeriffId
        {
            get { return teriffId; }
            set { teriffId = value; }
        }

        /// <summary>
        /// Gets or sets the TeriffRate value.
        /// </summary>
        public virtual float TeriffRate
        {
            get { return teriffRate; }
            set { teriffRate = value; }
        }

        /// <summary>
        /// Gets or sets the DateFrom value.
        /// </summary>
        public virtual DateTime DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        /// <summary>
        /// Gets or sets the DateTo value.
        /// </summary>
        public virtual DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
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
        /// Gets or sets the Created_TS value.
        /// </summary>
        public virtual DateTime Created_TS
        {
            get { return created_TS; }
            set { created_TS = value; }
        }

        /// <summary>
        /// Gets or sets the Updated_UserId value.
        /// </summary>
        public virtual int Updated_UserId
        {
            get { return updated_UserId; }
            set { updated_UserId = value; }
        }

        /// <summary>
        /// Gets or sets the Updated_TS value.
        /// </summary>
        public virtual DateTime Updated_TS
        {
            get { return updated_TS; }
            set { updated_TS = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.teriffId = 0;
            this.teriffRate = 0;
            this.dateFrom = DateTime.MinValue;
            this.dateTo = DateTime.MinValue;
            this.created_UserId = 0;
            this.created_TS = DateTime.MinValue;
            this.updated_UserId = 0;
            this.updated_TS = DateTime.MinValue;
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
            if (dt.Columns.Contains("TeriffRateId"))
            {
                int.TryParse(Convert.ToString(dr["TeriffRateId"]), out teriffRateId);
            }
            if (dt.Columns.Contains("TeriffId"))
            {
                int.TryParse(Convert.ToString(dr["TeriffId"]), out teriffId);
            }
            if (dt.Columns.Contains("TeriffRate"))
            {
                float.TryParse(Convert.ToString(dr["TeriffRate"]), out teriffRate);
            }
            if (dt.Columns.Contains("DateFrom"))
            {
                DateTime.TryParse(Convert.ToString(dr["DateFrom"]), out dateFrom);
            }
            if (dt.Columns.Contains("DateTo"))
            {
                DateTime.TryParse(Convert.ToString(dr["DateTo"]), out dateTo);
            }
            if (dt.Columns.Contains("Created_UserId"))
            {
                int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId);
            }
            if (dt.Columns.Contains("Created_TS"))
            {
                DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS);
            }
            if (dt.Columns.Contains("Updated_UserId"))
            {
                int.TryParse(Convert.ToString(dr["Updated_UserId"]), out updated_UserId);
            }
            if (dt.Columns.Contains("Updated_TS"))
            {
                DateTime.TryParse(Convert.ToString(dr["Updated_TS"]), out updated_TS);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Teriff_Rates table.
        /// </summary>
        public void Save()
        {
            if (teriffRateId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Teriff_Rates table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesInsert"))
            {
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                db.AddInParameter(dbCommand, "TeriffRate", DbType.Double, teriffRate);
                if (dateFrom != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, dateFrom);
                if (dateTo != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, dateTo);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                teriffRateId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Teriff_Rates table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesUpdate"))
            {
                db.AddInParameter(dbCommand, "TeriffRateId", DbType.Int32, teriffRateId);
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                db.AddInParameter(dbCommand, "TeriffRate", DbType.Double, teriffRate);
                if (dateFrom != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, dateFrom);
                if (dateTo != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, dateTo);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Teriff_Rates table by a composite primary key.
        /// </summary>
        /// <param name="teriffRateId"></param>
        /// <returns>String</returns>
        public static string Delete(int teriffRateId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesDelete"))
            {
                db.AddInParameter(dbCommand, "TeriffRateId", DbType.Int32, teriffRateId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Tariff_Rates is already in use. You can not delete this Teriff_Rates.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the Teriff_Rates table by a foreign key.
        /// </summary>
        /// <param name="teriffId"></param>
        public static void DeleteByTeriffId(int teriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesDeleteByTeriffId"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the Teriff_Rates table.
        /// </summary>
        /// <param name="teriffRateId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int teriffRateId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesSelect"))
            {
                db.AddInParameter(dbCommand, "TeriffRateId", DbType.Int32, teriffRateId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the Teriff_Rates table by a foreign key.
        /// </summary>
        /// <param name="teriffId"></param>
        /// <returns>List</returns>
        public static Teriff_RateList SelectByTeriffId(int teriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesSelectByTeriffId"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the Teriff_Rates table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static Teriff_RateList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Teriff_Rates table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public Teriff_RateList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Teriff_RatesSearch"))
            {
                if (teriffRateId > 0)
                    db.AddInParameter(dbCommand, "TeriffRateId", DbType.Int32, teriffRateId);
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                if (teriffRate > 0)
                    db.AddInParameter(dbCommand, "TeriffRate", DbType.Double, teriffRate);
                if (dateFrom != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, dateFrom);
                if (dateTo != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, dateTo);
                if (created_UserId > 0)
                    db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                if (updated_UserId > 0)
                    db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
