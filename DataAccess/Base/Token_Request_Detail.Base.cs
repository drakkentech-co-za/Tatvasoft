using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Token_Request_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>23-Sep-2013</CreatedDate>
    public partial class Token_Request_Detail
    {
        #region Fields

        private int tokenRequestDetailId;
        private int houseId;
        private int tokenDetailId;
        private string fromAddress;
        private DateTime requestDate;
        private int requestType;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Token_Request_Detail class.
        /// </summary>
        public Token_Request_Detail()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Token_Request_Detail class.
        /// </summary>
        public Token_Request_Detail(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Token_Request_Detail class.
        /// </summary>
        public Token_Request_Detail(int tokenRequestDetailId)
        {
            DataSet ds = Select(tokenRequestDetailId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the TokenRequestDetailId value.
        /// </summary>
        public virtual int TokenRequestDetailId
        {
            get { return tokenRequestDetailId; }
            set { tokenRequestDetailId = value; }
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
        /// Gets or sets the TokenDetailId value.
        /// </summary>
        public virtual int TokenDetailId
        {
            get { return tokenDetailId; }
            set { tokenDetailId = value; }
        }

        /// <summary>
        /// Gets or sets the FromAddress value.
        /// </summary>
        public virtual string FromAddress
        {
            get { return fromAddress; }
            set { fromAddress = value; }
        }

        /// <summary>
        /// Gets or sets the RequestDate value.
        /// </summary>
        public virtual DateTime RequestDate
        {
            get { return requestDate; }
            set { requestDate = value; }
        }

        /// <summary>
        /// Gets or sets the RequestType value.
        /// </summary>
        public virtual int RequestType
        {
            get { return requestType; }
            set { requestType = value; }
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
            this.tokenDetailId = 0;
            this.fromAddress = string.Empty;
            this.requestDate = DateTime.MinValue;
            this.requestType = 0;
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
            if (dt.Columns.Contains("TokenRequestDetailId"))
            {
                int.TryParse(Convert.ToString(dr["TokenRequestDetailId"]), out tokenRequestDetailId);
            }
            if (dt.Columns.Contains("HouseId"))
            {
                int.TryParse(Convert.ToString(dr["HouseId"]), out houseId);
            }
            if (dt.Columns.Contains("TokenDetailId"))
            {
                int.TryParse(Convert.ToString(dr["TokenDetailId"]), out tokenDetailId);
            }
            if (dt.Columns.Contains("FromAddress"))
            {
                fromAddress = Convert.ToString(dr["FromAddress"]);
            }
            if (dt.Columns.Contains("RequestDate"))
            {
                DateTime.TryParse(Convert.ToString(dr["RequestDate"]), out requestDate);
            }
            if (dt.Columns.Contains("RequestType"))
            {
                int.TryParse(Convert.ToString(dr["RequestType"]), out requestType);
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
        /// Saves a record to the tbl_Token_Request_Detail table.
        /// </summary>
        public void Save()
        {
            if (tokenRequestDetailId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Token_Request_Detail table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailInsert"))
            {
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (tokenDetailId > 0)
                    db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);
                db.AddInParameter(dbCommand, "FromAddress", DbType.String, fromAddress);
                if (requestDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "RequestDate", DbType.DateTime, requestDate);
                db.AddInParameter(dbCommand, "RequestType", DbType.Int32, requestType);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                tokenRequestDetailId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Token_Request_Detail table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailUpdate"))
            {
                db.AddInParameter(dbCommand, "TokenRequestDetailId", DbType.Int32, tokenRequestDetailId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (tokenDetailId > 0)
                    db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);
                db.AddInParameter(dbCommand, "FromAddress", DbType.String, fromAddress);
                if (requestDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "RequestDate", DbType.DateTime, requestDate);
                db.AddInParameter(dbCommand, "RequestType", DbType.Int32, requestType);
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
        /// Deletes a record from the Token_Request_Detail table by a composite primary key.
        /// </summary>
        /// <param name="tokenRequestDetailId"></param>
        /// <returns>String</returns>
        public static string Delete(int tokenRequestDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailDelete"))
            {
                db.AddInParameter(dbCommand, "TokenRequestDetailId", DbType.Int32, tokenRequestDetailId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Token_Request_Detail is already in use. You can not delete this Token_Request_Detail.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the Token_Request_Detail table by a foreign key.
        /// </summary>
        /// <param name="tokenDetailId"></param>
        public static void DeleteByTokenDetailId(int tokenDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailDeleteByTokenDetailId"))
            {
                db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Token_Request_Detail table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        public static void DeleteByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailDeleteByHouseId"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the Token_Request_Detail table.
        /// </summary>
        /// <param name="tokenRequestDetailId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int tokenRequestDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailSelect"))
            {
                db.AddInParameter(dbCommand, "TokenRequestDetailId", DbType.Int32, tokenRequestDetailId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the Token_Request_Detail table by a foreign key.
        /// </summary>
        /// <param name="tokenDetailId"></param>
        /// <returns>List</returns>
        public static Token_Request_DetailList SelectByTokenDetailId(int tokenDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailSelectByTokenDetailId"))
            {
                db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects records from the Token_Request_Detail table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>List</returns>
        public static Token_Request_DetailList SelectByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailSelectByHouseId"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the Token_Request_Detail table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static Token_Request_DetailList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Token_Request_Detail table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public Token_Request_DetailList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_Request_DetailSearch"))
            {
                if (tokenRequestDetailId > 0)
                    db.AddInParameter(dbCommand, "TokenRequestDetailId", DbType.Int32, tokenRequestDetailId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (tokenDetailId > 0)
                    db.AddInParameter(dbCommand, "TokenDetailId", DbType.Int32, tokenDetailId);
                if (fromAddress != string.Empty)
                    db.AddInParameter(dbCommand, "FromAddress", DbType.String, fromAddress);
                if (requestDate != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "RequestDate", DbType.DateTime, requestDate);
                if (requestType > 0)
                    db.AddInParameter(dbCommand, "RequestType", DbType.Int32, requestType);
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
