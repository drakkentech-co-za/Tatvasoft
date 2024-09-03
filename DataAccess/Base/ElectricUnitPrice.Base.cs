using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for ElectricUnitPrice table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>16-Sep-2013</CreatedDate>
    public partial class ElectricUnitPrice
    {
        #region Fields

        private int electricUnitPriceId;
        private DateTime startDate;
        private DateTime endDate;
        private decimal unitPrice;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ElectricUnitPrice class.
        /// </summary>
        public ElectricUnitPrice()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the ElectricUnitPrice class.
        /// </summary>
        public ElectricUnitPrice(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the ElectricUnitPrice class.
        /// </summary>
        public ElectricUnitPrice(int electricUnitPriceId)
        {
            DataSet ds = Select(electricUnitPriceId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ElectricUnitPriceId value.
        /// </summary>
        public virtual int ElectricUnitPriceId
        {
            get { return electricUnitPriceId; }
            set { electricUnitPriceId = value; }
        }

        /// <summary>
        /// Gets or sets the StartDate value.
        /// </summary>
        public virtual DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        /// <summary>
        /// Gets or sets the EndDate value.
        /// </summary>
        public virtual DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        /// <summary>
        /// Gets or sets the UnitPrice value.
        /// </summary>
        public virtual decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MinValue;
            this.unitPrice = 0;
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
            if (dt.Columns.Contains("ElectricUnitPriceId"))
            {
                int.TryParse(Convert.ToString(dr["ElectricUnitPriceId"]), out electricUnitPriceId);
            }
            if (dt.Columns.Contains("StartDate"))
            {
                DateTime.TryParse(Convert.ToString(dr["StartDate"]), out startDate);
            }
            if (dt.Columns.Contains("EndDate"))
            {
                DateTime.TryParse(Convert.ToString(dr["EndDate"]), out endDate);
            }
            if (dt.Columns.Contains("UnitPrice"))
            {
                Decimal.TryParse(Convert.ToString(dr["UnitPrice"]), out unitPrice);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_ElectricUnitPrice table.
        /// </summary>
        public void Save()
        {
            if (electricUnitPriceId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the ElectricUnitPrice table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_ElectricUnitPriceInsert"))
            {
                if (startDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
                if (endDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "UnitPrice", DbType.Decimal, unitPrice);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                electricUnitPriceId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the ElectricUnitPrice table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_ElectricUnitPriceUpdate"))
            {
                db.AddInParameter(dbCommand, "ElectricUnitPriceId", DbType.Int32, electricUnitPriceId);
                if (startDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
                if (endDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "UnitPrice", DbType.Decimal, unitPrice);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the ElectricUnitPrice table by a composite primary key.
        /// </summary>
        /// <param name="electricUnitPriceId"></param>
        /// <returns>String</returns>
        public static string Delete(int electricUnitPriceId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_ElectricUnitPriceDelete"))
            {
                db.AddInParameter(dbCommand, "ElectricUnitPriceId", DbType.Int32, electricUnitPriceId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "ElectricUnitPrice is already in use. You can not delete this ElectricUnitPrice.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the ElectricUnitPrice table.
        /// </summary>
        /// <param name="electricUnitPriceId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int electricUnitPriceId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_ElectricUnitPriceSelect"))
            {
                db.AddInParameter(dbCommand, "ElectricUnitPriceId", DbType.Int32, electricUnitPriceId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the ElectricUnitPrice table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static ElectricUnitPriceList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_ElectricUnitPriceSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the ElectricUnitPrice table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public ElectricUnitPriceList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_ElectricUnitPriceSearch"))
            {
                if (electricUnitPriceId > 0)
                    db.AddInParameter(dbCommand, "ElectricUnitPriceId", DbType.Int32, electricUnitPriceId);
                if (startDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
                if (endDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
                if (unitPrice > 0)
                    db.AddInParameter(dbCommand, "UnitPrice", DbType.Decimal, unitPrice);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}