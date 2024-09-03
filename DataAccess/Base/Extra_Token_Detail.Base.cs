using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for tbl_Extra_Token_Details table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>31-Jan-2014</CreatedDate>
    public partial class Extra_Token_Detail
    {
        #region Fields

        private int? tokenRequestId;
        private int? houseId;
        private int? employeeId;
        private int? noOfUnits;
        private string tokenNumber;
        private string meterNumber;
        private int? periodId;
        private DateTime? dateIssue;
        private DateTime? created_TS;
        private int? created_UserId;
        private DateTime? updated_TS;
        private int? updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Tbl_Extra_Token_Detail class.
        /// </summary>
        public Extra_Token_Detail()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Tbl_Extra_Token_Detail class.
        /// </summary>
        public Extra_Token_Detail(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Tbl_Extra_Token_Detail class.
        /// </summary>
        public Extra_Token_Detail(int tokenRequestId)
        {
            DataSet ds = Select(tokenRequestId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TokenRequestId value.
        /// </summary>
        public virtual int? TokenRequestId
        {
            get { return tokenRequestId; }
            set { tokenRequestId = value; }
        }

        /// <summary>
        /// Gets or sets the HouseId value.
        /// </summary>
        public virtual int? HouseId
        {
            get { return houseId; }
            set { houseId = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeId value.
        /// </summary>
        public virtual int? EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        /// <summary>
        /// Gets or sets the NoOfUnits value.
        /// </summary>
        public virtual int? NoOfUnits
        {
            get { return noOfUnits; }
            set { noOfUnits = value; }
        }

        /// <summary>
        /// Gets or sets the TokenNumber value.
        /// </summary>
        public virtual string TokenNumber
        {
            get { return tokenNumber; }
            set { tokenNumber = value; }
        }

        /// <summary>
        /// Gets or sets the MeterNumber value.
        /// </summary>
        public virtual string MeterNumber
        {
            get { return meterNumber; }
            set { meterNumber = value; }
        }

        /// <summary>
        /// Gets or sets the PeriodId value.
        /// </summary>
        public virtual int? PeriodId
        {
            get { return periodId; }
            set { periodId = value; }
        }

        /// <summary>
        /// Gets or sets the DateIssue value.
        /// </summary>
        public virtual DateTime? DateIssue
        {
            get { return dateIssue; }
            set { dateIssue = value; }
        }

        /// <summary>
        /// Gets or sets the Created_TS value.
        /// </summary>
        public virtual DateTime? Created_TS
        {
            get { return created_TS; }
            set { created_TS = value; }
        }

        /// <summary>
        /// Gets or sets the Created_UserId value.
        /// </summary>
        public virtual int? Created_UserId
        {
            get { return created_UserId; }
            set { created_UserId = value; }
        }

        /// <summary>
        /// Gets or sets the Updated_TS value.
        /// </summary>
        public virtual DateTime? Updated_TS
        {
            get { return updated_TS; }
            set { updated_TS = value; }
        }

        /// <summary>
        /// Gets or sets the Updated_UserId value.
        /// </summary>
        public virtual int? Updated_UserId
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
            this.employeeId = 0;
            this.noOfUnits = 0;
            this.tokenNumber = string.Empty;
            this.meterNumber = string.Empty;
            this.periodId = 0;
            this.dateIssue = DateTime.MinValue;
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
            if (dt.Columns.Contains("TokenRequestId"))
            {
                tokenRequestId = ConvertTo.Integer(dr["TokenRequestId"]);
            }
            if (dt.Columns.Contains("HouseId"))
            {
                houseId = ConvertTo.Integer(dr["HouseId"]);
            }
            if (dt.Columns.Contains("EmployeeId"))
            {
                employeeId = ConvertTo.Integer(dr["EmployeeId"]);
            }
            if (dt.Columns.Contains("NoOfUnits"))
            {
                noOfUnits = ConvertTo.Integer(dr["NoOfUnits"]);
            }
            if (dt.Columns.Contains("TokenNumber"))
            {
                tokenNumber = Convert.ToString(dr["TokenNumber"]);
            }
            if (dt.Columns.Contains("MeterNumber"))
            {
                meterNumber = Convert.ToString(dr["MeterNumber"]);
            }
            if (dt.Columns.Contains("PeriodId"))
            {
                periodId = ConvertTo.Integer(dr["PeriodId"]);
            }
            if (dt.Columns.Contains("DateIssue"))
            {
                dateIssue = ConvertTo.Date(dr["DateIssue"]);
            }
            if (dt.Columns.Contains("Created_TS"))
            {
                created_TS = ConvertTo.Date(dr["Created_TS"]);
            }
            if (dt.Columns.Contains("Created_UserId"))
            {
                created_UserId = ConvertTo.Integer(dr["Created_UserId"]);
            }
            if (dt.Columns.Contains("Updated_TS"))
            {
                updated_TS = ConvertTo.Date(dr["Updated_TS"]);
            }
            if (dt.Columns.Contains("Updated_UserId"))
            {
                updated_UserId = ConvertTo.Integer(dr["Updated_UserId"]);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Extra_Token_Details table.
        /// </summary>
        public void Save()
        {
            if (tokenRequestId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the tbl_Extra_Token_Details table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_Extra_Token_DetailsInsert"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                db.AddInParameter(dbCommand, "NoOfUnits", DbType.Int32, noOfUnits);
                db.AddInParameter(dbCommand, "TokenNumber", DbType.String, tokenNumber);
                db.AddInParameter(dbCommand, "MeterNumber", DbType.String, meterNumber);
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
                if (dateIssue != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateIssue", DbType.DateTime, dateIssue);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                tokenRequestId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the tbl_Extra_Token_Details table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_Extra_Token_DetailsUpdate"))
            {
                db.AddInParameter(dbCommand, "TokenRequestId", DbType.Int32, tokenRequestId);
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                db.AddInParameter(dbCommand, "NoOfUnits", DbType.Int32, noOfUnits);
                db.AddInParameter(dbCommand, "TokenNumber", DbType.String, tokenNumber);
                db.AddInParameter(dbCommand, "MeterNumber", DbType.String, meterNumber);
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
                if (dateIssue != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateIssue", DbType.DateTime, dateIssue);
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
        /// Deletes a record from the tbl_Extra_Token_Details table by a composite primary key.
        /// </summary>
        /// <param name="tokenRequestId"></param>
        /// <returns>String</returns>
        public static string Delete(int tokenRequestId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_Extra_Token_DetailsDelete"))
            {
                db.AddInParameter(dbCommand, "TokenRequestId", DbType.Int32, tokenRequestId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "tbl_Extra_Token_Details is already in use. You can not delete this tbl_Extra_Token_Details.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the tbl_Extra_Token_Details table.
        /// </summary>
        /// <param name="tokenRequestId"></param>
        /// <returns>DataSet</returns>
        public static DataSet Select(int tokenRequestId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_Extra_Token_DetailsSelect"))
            {
                db.AddInParameter(dbCommand, "TokenRequestId", DbType.Int32, tokenRequestId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the tbl_Extra_Token_Details table.
        /// </summary>
        /// <returns>List</returns>
        public static DataSet SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_Extra_Token_DetailsSelectAll"))
            {
                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Search records from the tbl_Extra_Token_Details table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>List</returns>
        public DataSet Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_Extra_Token_DetailsSearch"))
            {
                if (tokenRequestId > 0)
                    db.AddInParameter(dbCommand, "TokenRequestId", DbType.Int32, tokenRequestId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (employeeId > 0)
                    db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                if (noOfUnits > 0)
                    db.AddInParameter(dbCommand, "NoOfUnits", DbType.Int32, noOfUnits);
                if (tokenNumber != string.Empty)
                    db.AddInParameter(dbCommand, "TokenNumber", DbType.String, tokenNumber);
                if (meterNumber != string.Empty)
                    db.AddInParameter(dbCommand, "MeterNumber", DbType.String, meterNumber);
                if (periodId > 0)
                    db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
                if (dateIssue != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "DateIssue", DbType.DateTime, dateIssue);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                if (created_UserId > 0)
                    db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                if (updated_UserId > 0)
                    db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        #endregion
    }
}
