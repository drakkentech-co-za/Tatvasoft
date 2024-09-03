using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Token_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Token_Detail
    {
        #region Fields

        private int tokenDetailId;
        private int houseId;
        private int noOfUnits;
        private string tokenNumber;
        private DateTime uploadDate;
        private DateTime startDate;
        private DateTime endDate;
        private bool blnAssigned;
        private bool blnCarryForward;
        private bool blnCurrent;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Token_Detail class.
        /// </summary>
        public Token_Detail()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Token_Detail class.
        /// </summary>
        public Token_Detail(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Token_Detail class.
        /// </summary>
        public Token_Detail(int tokenDetailId)
        {
            DataSet ds = Select(tokenDetailId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TokenDetailId value.
        /// </summary>
        public virtual int TokenDetailId
        {
            get { return tokenDetailId; }
            set { tokenDetailId = value; }
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
        /// Gets or sets the NoOfUnits value.
        /// </summary>
        public virtual int NoOfUnits
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
        /// Gets or sets the UploadDate value.
        /// </summary>
        public virtual DateTime UploadDate
        {
            get { return uploadDate; }
            set { uploadDate = value; }
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
        /// Gets or sets the BlnAssigned value.
        /// </summary>
        public virtual bool BlnAssigned
        {
            get { return blnAssigned; }
            set { blnAssigned = value; }
        }

        /// <summary>
        /// Gets or sets the BlnCarryForward value.
        /// </summary>
        public virtual bool BlnCarryForward
        {
            get { return blnCarryForward; }
            set { blnCarryForward = value; }
        }

        /// <summary>
        /// Gets or sets the BlnCurrent value.
        /// </summary>
        public virtual bool BlnCurrent
        {
            get { return blnCurrent; }
            set { blnCurrent = value; }
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
            this.noOfUnits = 0;
            this.tokenNumber = string.Empty;
            this.uploadDate = DateTime.MinValue;
            this.startDate = DateTime.MinValue;
            this.endDate = DateTime.MinValue;
            this.blnAssigned = false;
            this.blnCarryForward = false;
            this.blnCurrent = false;
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
            if (dt.Columns.Contains("TokenDetailId"))
            {
                int.TryParse(Convert.ToString(dr["TokenDetailId"]), out tokenDetailId);
            }
            if (dt.Columns.Contains("HouseId"))
            {
                int.TryParse(Convert.ToString(dr["HouseId"]), out houseId);
            }
            if (dt.Columns.Contains("NoOfUnits"))
            {
                int.TryParse(Convert.ToString(dr["NoOfUnits"]), out noOfUnits);
            }
            if (dt.Columns.Contains("TokenNumber"))
            {
                tokenNumber = Convert.ToString(dr["TokenNumber"]);
            }
            if (dt.Columns.Contains("UploadDate"))
            {
                DateTime.TryParse(Convert.ToString(dr["UploadDate"]), out uploadDate);
            }
            if (dt.Columns.Contains("StartDate"))
            {
                DateTime.TryParse(Convert.ToString(dr["StartDate"]), out startDate);
            }
            if (dt.Columns.Contains("EndDate"))
            {
                DateTime.TryParse(Convert.ToString(dr["EndDate"]), out endDate);
            }
            if (dt.Columns.Contains("blnAssigned"))
            {
                bool.TryParse(Convert.ToString(dr["blnAssigned"]), out blnAssigned);
            }
            if (dt.Columns.Contains("blnCarryForward"))
            {
                bool.TryParse(Convert.ToString(dr["blnCarryForward"]), out blnCarryForward);
            }
            if (dt.Columns.Contains("blnCurrent"))
            {
                bool.TryParse(Convert.ToString(dr["blnCurrent"]), out blnCurrent);
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
        /// Saves a record to the tbl_Token_Detail table.
        /// </summary>
        public void Save()
        {
            if (tokenDetailId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Token_Detail table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailInsert"))
            {
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "NoOfUnits", DbType.Int32, noOfUnits);
                db.AddInParameter(dbCommand, "TokenNumber", DbType.String, tokenNumber);
                if (uploadDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, uploadDate);
                if (startDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
                if (endDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "blnAssigned", DbType.Boolean, blnAssigned);
                db.AddInParameter(dbCommand, "blnCarryForward", DbType.Boolean, blnCarryForward);
                db.AddInParameter(dbCommand, "blnCurrent", DbType.Boolean, blnCurrent);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                tokenDetailId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Token_Detail table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailUpdate"))
            {
                db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "NoOfUnits", DbType.Int32, noOfUnits);
                db.AddInParameter(dbCommand, "TokenNumber", DbType.String, tokenNumber);
                if (uploadDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, uploadDate);
                if (startDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
                if (endDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
                db.AddInParameter(dbCommand, "blnAssigned", DbType.Boolean, blnAssigned);
                db.AddInParameter(dbCommand, "blnCarryForward", DbType.Boolean, blnCarryForward);
                db.AddInParameter(dbCommand, "blnCurrent", DbType.Boolean, blnCurrent);
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
        /// Deletes a record from the Token_Detail table by a composite primary key.
        /// </summary>
        /// <param name="tokenDetailId"></param>
        /// <returns>String</returns>
        public static string Delete(int tokenDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailDelete"))
            {
                db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Token_Detail is already in use. You can not delete this Token_Detail.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the Token_Detail table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        public static void DeleteByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailDeleteByHouseId"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the Token_Detail table.
        /// </summary>
        /// <param name="tokenDetailId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int tokenDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailSelect"))
            {
                db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the Token_Detail table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>List</returns>
        public static Token_DetailList SelectByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailSelectByHouseId"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the Token_Detail table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static Token_DetailList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Token_Detail table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public Token_DetailList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_DetailSearch"))
            {
                if (tokenDetailId > 0)
                    db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (noOfUnits > 0)
                    db.AddInParameter(dbCommand, "NoOfUnits", DbType.Int32, noOfUnits);
                if (tokenNumber != string.Empty)
                    db.AddInParameter(dbCommand, "TokenNumber", DbType.String, tokenNumber);
                if (uploadDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "UploadDate", DbType.DateTime, uploadDate);
                if (startDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
                if (endDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
                if (blnAssigned != null)
                    db.AddInParameter(dbCommand, "blnAssigned", DbType.Boolean, blnAssigned);
                if (blnCarryForward != null)
                    db.AddInParameter(dbCommand, "blnCarryForward", DbType.Boolean, blnCarryForward);
                if (blnCurrent != null)
                    db.AddInParameter(dbCommand, "blnCurrent", DbType.Boolean, blnCurrent);
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
