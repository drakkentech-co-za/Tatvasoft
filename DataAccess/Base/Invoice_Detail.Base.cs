using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Invoice_Detail table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Invoice_Detail
    {
        #region Fields

        private int invoiceDetailId;
        private int invoiceId;
        private int teriffId;
        private string cReference;
        private string description;
        private decimal rAmountInc;
        private decimal sAmountEx;
        private decimal sVAT;
        private decimal sAmountInc;
        private string startReading;
        private string endReading;
        private string reading;
        private DateTime created_TS;
        private int created_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Invoice_Detail class.
        /// </summary>
        public Invoice_Detail()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Invoice_Detail class.
        /// </summary>
        public Invoice_Detail(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Invoice_Detail class.
        /// </summary>
        public Invoice_Detail(int invoiceDetailId)
        {
            DataSet ds = Select(invoiceDetailId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the InvoiceDetailId value.
        /// </summary>
        public virtual int InvoiceDetailId
        {
            get { return invoiceDetailId; }
            set { invoiceDetailId = value; }
        }

        /// <summary>
        /// Gets or sets the InvoiceId value.
        /// </summary>
        public virtual int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; }
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
        /// Gets or sets the CReference value.
        /// </summary>
        public virtual string CReference
        {
            get { return cReference; }
            set { cReference = value; }
        }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Gets or sets the RAmountInc value.
        /// </summary>
        public virtual decimal RAmountInc
        {
            get { return rAmountInc; }
            set { rAmountInc = value; }
        }

        /// <summary>
        /// Gets or sets the SAmountEx value.
        /// </summary>
        public virtual decimal SAmountEx
        {
            get { return sAmountEx; }
            set { sAmountEx = value; }
        }

        /// <summary>
        /// Gets or sets the SVAT value.
        /// </summary>
        public virtual decimal SVAT
        {
            get { return sVAT; }
            set { sVAT = value; }
        }

        /// <summary>
        /// Gets or sets the SAmountInc value.
        /// </summary>
        public virtual decimal SAmountInc
        {
            get { return sAmountInc; }
            set { sAmountInc = value; }
        }

        /// <summary>
        /// Gets or sets the StartReading value.
        /// </summary>
        public virtual string StartReading
        {
            get { return startReading; }
            set { startReading = value; }
        }

        /// <summary>
        /// Gets or sets the EndReading value.
        /// </summary>
        public virtual string EndReading
        {
            get { return endReading; }
            set { endReading = value; }
        }

        /// <summary>
        /// Gets or sets the Reading value.
        /// </summary>
        public virtual string Reading
        {
            get { return reading; }
            set { reading = value; }
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

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.invoiceId = 0;
            this.teriffId = 0;
            this.cReference = string.Empty;
            this.description = string.Empty;
            this.rAmountInc = 0;
            this.sAmountEx = 0;
            this.sVAT = 0;
            this.sAmountInc = 0;
            this.startReading = string.Empty;
            this.endReading = string.Empty;
            this.reading = string.Empty;
            this.created_TS = DateTime.MinValue;
            this.created_UserId = 0;
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
            if (dt.Columns.Contains("InvoiceDetailId"))
            {
                int.TryParse(Convert.ToString(dr["InvoiceDetailId"]), out invoiceDetailId);
            }
            if (dt.Columns.Contains("InvoiceId"))
            {
                int.TryParse(Convert.ToString(dr["InvoiceId"]), out invoiceId);
            }
            if (dt.Columns.Contains("TeriffId"))
            {
                int.TryParse(Convert.ToString(dr["TeriffId"]), out teriffId);
            }
            if (dt.Columns.Contains("cReference"))
            {
                cReference = Convert.ToString(dr["cReference"]);
            }
            if (dt.Columns.Contains("Description"))
            {
                description = Convert.ToString(dr["Description"]);
            }
            if (dt.Columns.Contains("RAmountInc"))
            {
                Decimal.TryParse(Convert.ToString(dr["RAmountInc"]), out rAmountInc);
            }
            if (dt.Columns.Contains("SAmountEx"))
            {
                Decimal.TryParse(Convert.ToString(dr["SAmountEx"]), out sAmountEx);
            }
            if (dt.Columns.Contains("SVAT"))
            {
                Decimal.TryParse(Convert.ToString(dr["SVAT"]), out sVAT);
            }
            if (dt.Columns.Contains("SAmountInc"))
            {
                Decimal.TryParse(Convert.ToString(dr["SAmountInc"]), out sAmountInc);
            }
            if (dt.Columns.Contains("StartReading"))
            {
                startReading = Convert.ToString(dr["StartReading"]);
            }
            if (dt.Columns.Contains("EndReading"))
            {
                endReading = Convert.ToString(dr["EndReading"]);
            }
            if (dt.Columns.Contains("Reading"))
            {
                reading = Convert.ToString(dr["Reading"]);
            }
            if (dt.Columns.Contains("Created_TS"))
            {
                DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS);
            }
            if (dt.Columns.Contains("Created_UserId"))
            {
                int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Invoice_Detail table.
        /// </summary>
        public void Save()
        {
            if (invoiceDetailId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Invoice_Detail table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailInsert"))
            {
                if (invoiceId > 0)
                    db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                db.AddInParameter(dbCommand, "cReference", DbType.String, cReference);
                db.AddInParameter(dbCommand, "Description", DbType.String, description);
                db.AddInParameter(dbCommand, "RAmountInc", DbType.Decimal, rAmountInc);
                db.AddInParameter(dbCommand, "SAmountEx", DbType.Decimal, sAmountEx);
                db.AddInParameter(dbCommand, "SVAT", DbType.Decimal, sVAT);
                db.AddInParameter(dbCommand, "SAmountInc", DbType.Decimal, sAmountInc);
                db.AddInParameter(dbCommand, "StartReading", DbType.String, startReading);
                db.AddInParameter(dbCommand, "EndReading", DbType.String, endReading);
                db.AddInParameter(dbCommand, "Reading", DbType.String, reading);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                invoiceDetailId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Invoice_Detail table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailUpdate"))
            {
                db.AddInParameter(dbCommand, "InvoiceDetailId", DbType.Int32, invoiceDetailId);
                if (invoiceId > 0)
                    db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                db.AddInParameter(dbCommand, "cReference", DbType.String, cReference);
                db.AddInParameter(dbCommand, "Description", DbType.String, description);
                db.AddInParameter(dbCommand, "RAmountInc", DbType.Decimal, rAmountInc);
                db.AddInParameter(dbCommand, "SAmountEx", DbType.Decimal, sAmountEx);
                db.AddInParameter(dbCommand, "SVAT", DbType.Decimal, sVAT);
                db.AddInParameter(dbCommand, "SAmountInc", DbType.Decimal, sAmountInc);
                db.AddInParameter(dbCommand, "StartReading", DbType.String, startReading);
                db.AddInParameter(dbCommand, "EndReading", DbType.String, endReading);
                db.AddInParameter(dbCommand, "Reading", DbType.String, reading);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Invoice_Detail table by a composite primary key.
        /// </summary>
        /// <param name="invoiceDetailId"></param>
        /// <returns>String</returns>
        public static string Delete(int invoiceDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailDelete"))
            {
                db.AddInParameter(dbCommand, "InvoiceDetailId", DbType.Int32, invoiceDetailId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Invoice_Detail is already in use. You can not delete this Invoice_Detail.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the Invoice_Detail table by a foreign key.
        /// </summary>
        /// <param name="invoiceId"></param>
        public static void DeleteByInvoiceId(int invoiceId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailDeleteByInvoiceId"))
            {
                db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Invoice_Detail table by a foreign key.
        /// </summary>
        /// <param name="teriffId"></param>
        public static void DeleteByTeriffId(int teriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailDeleteByTeriffId"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the Invoice_Detail table.
        /// </summary>
        /// <param name="invoiceDetailId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int invoiceDetailId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailSelect"))
            {
                db.AddInParameter(dbCommand, "InvoiceDetailId", DbType.Int32, invoiceDetailId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the Invoice_Detail table by a foreign key.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List</returns>
        public static Invoice_DetailList SelectByInvoiceId(int invoiceId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailSelectByInvoiceId"))
            {
                db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects records from the Invoice_Detail table by a foreign key.
        /// </summary>
        /// <param name="teriffId"></param>
        /// <returns>List</returns>
        public static Invoice_DetailList SelectByTeriffId(int teriffId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailSelectByTeriffId"))
            {
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the Invoice_Detail table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static Invoice_DetailList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Invoice_Detail table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public Invoice_DetailList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Invoice_DetailSearch"))
            {
                if (invoiceDetailId > 0)
                    db.AddInParameter(dbCommand, "InvoiceDetailId", DbType.Int32, invoiceDetailId);
                if (invoiceId > 0)
                    db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
                if (teriffId > 0)
                    db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, teriffId);
                if (cReference != string.Empty)
                    db.AddInParameter(dbCommand, "cReference", DbType.String, cReference);
                if (description != string.Empty)
                    db.AddInParameter(dbCommand, "Description", DbType.String, description);
                if (rAmountInc > 0)
                    db.AddInParameter(dbCommand, "RAmountInc", DbType.Decimal, rAmountInc);
                if (sAmountEx > 0)
                    db.AddInParameter(dbCommand, "SAmountEx", DbType.Decimal, sAmountEx);
                if (sVAT > 0)
                    db.AddInParameter(dbCommand, "SVAT", DbType.Decimal, sVAT);
                if (sAmountInc > 0)
                    db.AddInParameter(dbCommand, "SAmountInc", DbType.Decimal, sAmountInc);
                if (startReading != string.Empty)
                    db.AddInParameter(dbCommand, "StartReading", DbType.String, startReading);
                if (endReading != string.Empty)
                    db.AddInParameter(dbCommand, "EndReading", DbType.String, endReading);
                if (reading != string.Empty)
                    db.AddInParameter(dbCommand, "Reading", DbType.String, reading);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                if (created_UserId > 0)
                    db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
