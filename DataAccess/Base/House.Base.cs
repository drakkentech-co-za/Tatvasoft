using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for House table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>12-Sep-2013</CreatedDate>
    public partial class House
    {
        #region Fields

        private int houseId;
        private string accountNo;
        private string eRFNo;
        private float eRFSize;
        private int houseTypeId;
        private string address1;
        private string address2;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;
        private int assetType;
        private bool electricityType;
        private bool prepaidAllowed;
        private string reason;
        private int electricUnits;
        private string meterNo;
        private bool isRental;
        private decimal rentalAmount;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the House class.
        /// </summary>
        public House()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the House class.
        /// </summary>
        public House(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the House class.
        /// </summary>
        public House(int houseId)
        {
            DataSet ds = Select(houseId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the HouseId value.
        /// </summary>
        public virtual int HouseId
        {
            get { return houseId; }
            set { houseId = value; }
        }

        /// <summary>
        /// Gets or sets the AccountNo value.
        /// </summary>
        public virtual string AccountNo
        {
            get { return accountNo; }
            set { accountNo = value; }
        }

        /// <summary>
        /// Gets or sets the ERFNo value.
        /// </summary>
        public virtual string ERFNo
        {
            get { return eRFNo; }
            set { eRFNo = value; }
        }

        /// <summary>
        /// Gets or sets the ERFSize value.
        /// </summary>
        public virtual float ERFSize
        {
            get { return eRFSize; }
            set { eRFSize = value; }
        }

        /// <summary>
        /// Gets or sets the HouseTypeId value.
        /// </summary>
        public virtual int HouseTypeId
        {
            get { return houseTypeId; }
            set { houseTypeId = value; }
        }

        /// <summary>
        /// Gets or sets the Address1 value.
        /// </summary>
        public virtual string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }

        /// <summary>
        /// Gets or sets the Address2 value.
        /// </summary>
        public virtual string Address2
        {
            get { return address2; }
            set { address2 = value; }
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

        /// <summary>
        /// Gets or sets the AssetType value.
        /// </summary>
        public virtual int AssetType
        {
            get { return assetType; }
            set { assetType = value; }
        }

        /// <summary>
        /// Gets or sets the ElectricityType value.
        /// </summary>
        public virtual bool ElectricityType
        {
            get { return electricityType; }
            set { electricityType = value; }
        }

        /// <summary>
        /// Gets or sets the PrepaidAllowed value.
        /// </summary>
        public virtual bool PrepaidAllowed
        {
            get { return prepaidAllowed; }
            set { prepaidAllowed = value; }
        }

        /// <summary>
        /// Gets or sets the Reason value.
        /// </summary>
        public virtual string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        /// <summary>
        /// Gets or sets the ElectricUnits value.
        /// </summary>
        public virtual int ElectricUnits
        {
            get { return electricUnits; }
            set { electricUnits = value; }
        }

        /// <summary>
        /// Gets or Sets the Meter Number
        /// </summary>
        public virtual string MeterNo
        {
            get { return meterNo; }
            set { meterNo = value; }
        }

        /// <summary>
        /// Gets or sets the IsRental value.
        /// </summary>
        public virtual bool IsRental
        {
            get { return isRental; }
            set { isRental = value; }
        }

        /// <summary>
        /// Gets or sets the RentalAmount value.
        /// </summary>
        public virtual decimal RentalAmount
        {
            get { return rentalAmount; }
            set { rentalAmount = value; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.accountNo = string.Empty;
            this.eRFNo = string.Empty;
            this.eRFSize = 0;
            this.houseTypeId = 0;
            this.address1 = string.Empty;
            this.address2 = string.Empty;
            this.created_TS = DateTime.MinValue;
            this.created_UserId = 0;
            this.updated_TS = DateTime.MinValue;
            this.updated_UserId = 0;
            this.assetType = 0;
            this.electricityType = false;
            this.prepaidAllowed = false;
            this.reason = string.Empty;
            this.electricUnits = 0;
            this.meterNo = string.Empty;
            this.isRental = false;
            this.rentalAmount = 0;
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
            if (dt.Columns.Contains("HouseId"))
            {
                int.TryParse(Convert.ToString(dr["HouseId"]), out houseId);
            }
            if (dt.Columns.Contains("AccountNo"))
            {
                accountNo = Convert.ToString(dr["AccountNo"]);
            }
            if (dt.Columns.Contains("ERFNo"))
            {
                eRFNo = Convert.ToString(dr["ERFNo"]);
            }
            if (dt.Columns.Contains("ERFSize"))
            {
                float.TryParse(Convert.ToString(dr["ERFSize"]), out eRFSize);
            }
            if (dt.Columns.Contains("HouseTypeId"))
            {
                int.TryParse(Convert.ToString(dr["HouseTypeId"]), out houseTypeId);
            }
            if (dt.Columns.Contains("Address1"))
            {
                address1 = Convert.ToString(dr["Address1"]);
            }
            if (dt.Columns.Contains("Address2"))
            {
                address2 = Convert.ToString(dr["Address2"]);
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
            if (dt.Columns.Contains("AssetType"))
            {
                int.TryParse(Convert.ToString(dr["AssetType"]), out assetType);
            }
            if (dt.Columns.Contains("ElectricityType"))
            {
                bool.TryParse(Convert.ToString(dr["ElectricityType"]), out electricityType);
            }
            if (dt.Columns.Contains("PrepaidAllowed"))
            {
                bool.TryParse(Convert.ToString(dr["PrepaidAllowed"]), out prepaidAllowed);
            }
            if (dt.Columns.Contains("Reason"))
            {
                reason = Convert.ToString(dr["Reason"]);
            }
            if (dt.Columns.Contains("ElectricUnits"))
            {
                int.TryParse(Convert.ToString(dr["ElectricUnits"]), out electricUnits);
            }
            if (dt.Columns.Contains("MeterNo"))
            {
                meterNo = Convert.ToString(dr["MeterNo"]);
            }
            if (dt.Columns.Contains("IsRental"))
            {
                bool.TryParse(Convert.ToString(dr["IsRental"]), out isRental);
            }
            if (dt.Columns.Contains("RentalAmount"))
            {
                Decimal.TryParse(Convert.ToString(dr["RentalAmount"]), out rentalAmount);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_House table.
        /// </summary>
        public void Save()
        {
            if (houseId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the House table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseInsert"))
            {
                db.AddInParameter(dbCommand, "AccountNo", DbType.String, accountNo);
                db.AddInParameter(dbCommand, "ERFNo", DbType.String, eRFNo);
                db.AddInParameter(dbCommand, "ERFSize", DbType.Double, eRFSize);
                if (houseTypeId > 0)
                    db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
                db.AddInParameter(dbCommand, "Address1", DbType.String, address1);
                db.AddInParameter(dbCommand, "Address2", DbType.String, address2);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
                db.AddInParameter(dbCommand, "AssetType", DbType.Int32, assetType);
                db.AddInParameter(dbCommand, "ElectricityType", DbType.Boolean, electricityType);
                db.AddInParameter(dbCommand, "PrepaidAllowed", DbType.Boolean, prepaidAllowed);
                db.AddInParameter(dbCommand, "Reason", DbType.String, reason);
                db.AddInParameter(dbCommand, "ElectricUnits", DbType.Int32, electricUnits);
                db.AddInParameter(dbCommand, "MeterNo", DbType.String, meterNo);
                db.AddInParameter(dbCommand, "IsRental", DbType.Boolean, isRental);
                db.AddInParameter(dbCommand, "RentalAmount", DbType.Currency, rentalAmount);
                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                houseId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the House table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseUpdate"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                db.AddInParameter(dbCommand, "AccountNo", DbType.String, accountNo);
                db.AddInParameter(dbCommand, "ERFNo", DbType.String, eRFNo);
                db.AddInParameter(dbCommand, "ERFSize", DbType.Double, eRFSize);
                if (houseTypeId > 0)
                    db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
                db.AddInParameter(dbCommand, "Address1", DbType.String, address1);
                db.AddInParameter(dbCommand, "Address2", DbType.String, address2);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
                db.AddInParameter(dbCommand, "AssetType", DbType.Int32, assetType);
                db.AddInParameter(dbCommand, "ElectricityType", DbType.Boolean, electricityType);
                db.AddInParameter(dbCommand, "PrepaidAllowed", DbType.Boolean, prepaidAllowed);
                db.AddInParameter(dbCommand, "Reason", DbType.String, reason);
                db.AddInParameter(dbCommand, "ElectricUnits", DbType.Int32, electricUnits);
                db.AddInParameter(dbCommand, "MeterNo", DbType.String, meterNo);
                db.AddInParameter(dbCommand, "IsRental", DbType.Boolean, isRental);
                db.AddInParameter(dbCommand, "RentalAmount", DbType.Currency, rentalAmount);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the House table by a composite primary key.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>String</returns>
        public static string Delete(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseDelete"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "House is already in use. You can not delete this House.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the House table by a foreign key.
        /// </summary>
        /// <param name="houseTypeId"></param>
        public static void DeleteByHouseTypeId(int houseTypeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseDeleteByHouseTypeId"))
            {
                db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the House table.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseSelect"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the House table by a foreign key.
        /// </summary>
        /// <param name="houseTypeId"></param>
        /// <returns>List</returns>
        public static HouseList SelectByHouseTypeId(int houseTypeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseSelectByHouseTypeId"))
            {
                db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the House table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static HouseList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the House table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public HouseList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseSearch"))
            {
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (accountNo != string.Empty)
                    db.AddInParameter(dbCommand, "AccountNo", DbType.String, accountNo);
                if (eRFNo != string.Empty)
                    db.AddInParameter(dbCommand, "ERFNo", DbType.String, eRFNo);
                if (eRFSize > 0)
                    db.AddInParameter(dbCommand, "ERFSize", DbType.Double, eRFSize);
                if (houseTypeId > 0)
                    db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
                if (address1 != string.Empty)
                    db.AddInParameter(dbCommand, "Address1", DbType.String, address1);
                if (address2 != string.Empty)
                    db.AddInParameter(dbCommand, "Address2", DbType.String, address2);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                if (created_UserId > 0)
                    db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                if (updated_UserId > 0)
                    db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
                if (assetType > 0)
                    db.AddInParameter(dbCommand, "AssetType", DbType.Int32, assetType);
                if (electricityType != null)
                    db.AddInParameter(dbCommand, "ElectricityType", DbType.Boolean, electricityType);
                if (prepaidAllowed != null)
                    db.AddInParameter(dbCommand, "PrepaidAllowed", DbType.Boolean, prepaidAllowed);
                if (reason != string.Empty)
                    db.AddInParameter(dbCommand, "Reason", DbType.String, reason);
                if (electricUnits > 0)
                    db.AddInParameter(dbCommand, "ElectricUnits", DbType.Int32, electricUnits);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
