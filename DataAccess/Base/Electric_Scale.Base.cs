using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Electric_Scale table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>05-Sep-2013</CreatedDate>
    public partial class Electric_Scale
    {
        #region Fields

        private int electricScaleId;
        private float limitFrom;
        private float limitTo;
        private float multiplicationFactor;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Electric_Scale class.
        /// </summary>
        public Electric_Scale()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Electric_Scale class.
        /// </summary>
        public Electric_Scale(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Electric_Scale class.
        /// </summary>
        public Electric_Scale(int electricScaleId)
        {
            DataSet ds = Select(electricScaleId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the ElectricScaleId value.
        /// </summary>
        public virtual int ElectricScaleId
        {
            get { return electricScaleId; }
            set { electricScaleId = value; }
        }

        /// <summary>
        /// Gets or sets the LimitFrom value.
        /// </summary>
        public virtual float LimitFrom
        {
            get { return limitFrom; }
            set { limitFrom = value; }
        }

        /// <summary>
        /// Gets or sets the LimitTo value.
        /// </summary>
        public virtual float LimitTo
        {
            get { return limitTo; }
            set { limitTo = value; }
        }

        /// <summary>
        /// Gets or sets the MultiplicationFactor value.
        /// </summary>
        public virtual float MultiplicationFactor
        {
            get { return multiplicationFactor; }
            set { multiplicationFactor = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.limitFrom = 0;
            this.limitTo = 0;
            this.multiplicationFactor = 0;
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
            if (dt.Columns.Contains("ElectricScaleId"))
            {
                int.TryParse(Convert.ToString(dr["ElectricScaleId"]), out electricScaleId);
            }
            if (dt.Columns.Contains("LimitFrom"))
            {
                float.TryParse(Convert.ToString(dr["LimitFrom"]), out limitFrom);
            }
            if (dt.Columns.Contains("LimitTo"))
            {
                float.TryParse(Convert.ToString(dr["LimitTo"]), out limitTo);
            }
            if (dt.Columns.Contains("MultiplicationFactor"))
            {
                float.TryParse(Convert.ToString(dr["MultiplicationFactor"]), out multiplicationFactor);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Electric_Scale table.
        /// </summary>
        public void Save()
        {
            if (electricScaleId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Electric_Scale table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Electric_ScaleInsert"))
            {
                db.AddInParameter(dbCommand, "LimitFrom", DbType.Double, limitFrom);
                db.AddInParameter(dbCommand, "LimitTo", DbType.Double, limitTo);
                db.AddInParameter(dbCommand, "MultiplicationFactor", DbType.Double, multiplicationFactor);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                electricScaleId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Electric_Scale table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Electric_ScaleUpdate"))
            {
                db.AddInParameter(dbCommand, "ElectricScaleId", DbType.Int32, electricScaleId);
                db.AddInParameter(dbCommand, "LimitFrom", DbType.Double, limitFrom);
                db.AddInParameter(dbCommand, "LimitTo", DbType.Double, limitTo);
                db.AddInParameter(dbCommand, "MultiplicationFactor", DbType.Double, multiplicationFactor);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Electric_Scale table by a composite primary key.
        /// </summary>
        /// <param name="electricScaleId"></param>
        /// <returns>String</returns>
        public static string Delete(int electricScaleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Electric_ScaleDelete"))
            {
                db.AddInParameter(dbCommand, "ElectricScaleId", DbType.Int32, electricScaleId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Electric_Scale is already in use. You can not delete this Electric_Scale.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the Electric_Scale table.
        /// </summary>
        /// <param name="electricScaleId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int electricScaleId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Electric_ScaleSelect"))
            {
                db.AddInParameter(dbCommand, "ElectricScaleId", DbType.Int32, electricScaleId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the Electric_Scale table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static Electric_ScaleList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Electric_ScaleSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Electric_Scale table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public Electric_ScaleList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Electric_ScaleSearch"))
            {
                if (electricScaleId > 0)
                    db.AddInParameter(dbCommand, "ElectricScaleId", DbType.Int32, electricScaleId);
                if (limitFrom > 0)
                    db.AddInParameter(dbCommand, "LimitFrom", DbType.Double, limitFrom);
                if (limitTo > 0)
                    db.AddInParameter(dbCommand, "LimitTo", DbType.Double, limitTo);
                if (multiplicationFactor > 0)
                    db.AddInParameter(dbCommand, "MultiplicationFactor", DbType.Double, multiplicationFactor);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
