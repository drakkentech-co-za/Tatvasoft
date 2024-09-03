using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Payroll_Submission table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>18-Sep-2013</CreatedDate>
    public partial class Payroll_Submission
    {
        #region Fields

        private int payrollSubmissionId;
        private int houseId;
        private int employeeId;
        private int invoiceId;
        private int periodId;
        private decimal actualElecAmount;
        private decimal quotaElecAmount;
        private decimal waterMonthlyLimit;
        private int difference;
        private decimal excessWT4210;
        private decimal quotaWaterAmount;
        private decimal actualWaterAmount;
        private decimal fBVWT1552;
        private decimal wEAllowanceMakeUp;
        private int created_UserId;
        private DateTime created_TS;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Payroll_Submission class.
        /// </summary>
        public Payroll_Submission()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Payroll_Submission class.
        /// </summary>
        public Payroll_Submission(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Payroll_Submission class.
        /// </summary>
        public Payroll_Submission(int payrollSubmissionId)
        {
            DataSet ds = Select(payrollSubmissionId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the PayrollSubmissionId value.
        /// </summary>
        public virtual int PayrollSubmissionId
        {
            get { return payrollSubmissionId; }
            set { payrollSubmissionId = value; }
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
        /// Gets or sets the EmployeeId value.
        /// </summary>
        public virtual int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
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
        /// Gets or sets the PeriodId value.
        /// </summary>
        public virtual int PeriodId
        {
            get { return periodId; }
            set { periodId = value; }
        }

        /// <summary>
        /// Gets or sets the ActualElecAmount value.
        /// </summary>
        public virtual decimal ActualElecAmount
        {
            get { return actualElecAmount; }
            set { actualElecAmount = value; }
        }

        /// <summary>
        /// Gets or sets the QuotaElecAmount value.
        /// </summary>
        public virtual decimal QuotaElecAmount
        {
            get { return quotaElecAmount; }
            set { quotaElecAmount = value; }
        }

        /// <summary>
        /// Gets or sets the WaterMonthlyLimit value.
        /// </summary>
        public virtual decimal WaterMonthlyLimit
        {
            get { return waterMonthlyLimit; }
            set { waterMonthlyLimit = value; }
        }

        /// <summary>
        /// Gets or sets the Difference value.
        /// </summary>
        public virtual int Difference
        {
            get { return difference; }
            set { difference = value; }
        }

        /// <summary>
        /// Gets or sets the ExcessWT4210 value.
        /// </summary>
        public virtual decimal ExcessWT4210
        {
            get { return excessWT4210; }
            set { excessWT4210 = value; }
        }

        /// <summary>
        /// Gets or sets the QuotaWaterAmount value.
        /// </summary>
        public virtual decimal QuotaWaterAmount
        {
            get { return quotaWaterAmount; }
            set { quotaWaterAmount = value; }
        }

        /// <summary>
        /// Gets or sets the ActualWaterAmount value.
        /// </summary>
        public virtual decimal ActualWaterAmount
        {
            get { return actualWaterAmount; }
            set { actualWaterAmount = value; }
        }

        /// <summary>
        /// Gets or sets the FBVWT1552 value.
        /// </summary>
        public virtual decimal FBVWT1552
        {
            get { return fBVWT1552; }
            set { fBVWT1552 = value; }
        }

        /// <summary>
        /// Gets or sets the WEAllowanceMakeUp value.
        /// </summary>
        public virtual decimal WEAllowanceMakeUp
        {
            get { return wEAllowanceMakeUp; }
            set { wEAllowanceMakeUp = value; }
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

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.houseId = 0;
            this.employeeId = 0;
            this.invoiceId = 0;
            this.periodId = 0;
            this.actualElecAmount = 0;
            this.quotaElecAmount = 0;
            this.waterMonthlyLimit = 0;
            this.difference = 0;
            this.excessWT4210 = 0;
            this.quotaWaterAmount = 0;
            this.actualWaterAmount = 0;
            this.fBVWT1552 = 0;
            this.wEAllowanceMakeUp = 0;
            this.created_UserId = 0;
            this.created_TS = DateTime.MinValue;
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
            if (dt.Columns.Contains("PayrollSubmissionId"))
            {
                int.TryParse(Convert.ToString(dr["PayrollSubmissionId"]), out payrollSubmissionId);
            }
            if (dt.Columns.Contains("HouseId"))
            {
                int.TryParse(Convert.ToString(dr["HouseId"]), out houseId);
            }
            if (dt.Columns.Contains("EmployeeId"))
            {
                int.TryParse(Convert.ToString(dr["EmployeeId"]), out employeeId);
            }
            if (dt.Columns.Contains("InvoiceId"))
            {
                int.TryParse(Convert.ToString(dr["InvoiceId"]), out invoiceId);
            }
            if (dt.Columns.Contains("PeriodId"))
            {
                int.TryParse(Convert.ToString(dr["PeriodId"]), out periodId);
            }
            if (dt.Columns.Contains("ActualElecAmount"))
            {
                Decimal.TryParse(Convert.ToString(dr["ActualElecAmount"]), out actualElecAmount);
            }
            if (dt.Columns.Contains("QuotaElecAmount"))
            {
                Decimal.TryParse(Convert.ToString(dr["QuotaElecAmount"]), out quotaElecAmount);
            }
            if (dt.Columns.Contains("WaterMonthlyLimit"))
            {
                Decimal.TryParse(Convert.ToString(dr["WaterMonthlyLimit"]), out waterMonthlyLimit);
            }
            if (dt.Columns.Contains("Difference"))
            {
                int.TryParse(Convert.ToString(dr["Difference"]), out difference);
            }
            if (dt.Columns.Contains("ExcessWT4210"))
            {
                Decimal.TryParse(Convert.ToString(dr["ExcessWT4210"]), out excessWT4210);
            }
            if (dt.Columns.Contains("QuotaWaterAmount"))
            {
                Decimal.TryParse(Convert.ToString(dr["QuotaWaterAmount"]), out quotaWaterAmount);
            }
            if (dt.Columns.Contains("ActualWaterAmount"))
            {
                Decimal.TryParse(Convert.ToString(dr["ActualWaterAmount"]), out actualWaterAmount);
            }
            if (dt.Columns.Contains("FBVWT1552"))
            {
                Decimal.TryParse(Convert.ToString(dr["FBVWT1552"]), out fBVWT1552);
            }
            if (dt.Columns.Contains("WEAllowanceMakeUp"))
            {
                Decimal.TryParse(Convert.ToString(dr["WEAllowanceMakeUp"]), out wEAllowanceMakeUp);
            }
            if (dt.Columns.Contains("Created_UserId"))
            {
                int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId);
            }
            if (dt.Columns.Contains("Created_TS"))
            {
                DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Payroll_Submission table.
        /// </summary>
        public void Save()
        {
            if (payrollSubmissionId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Payroll_Submission table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionInsert"))
            {
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (employeeId > 0)
                    db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                if (invoiceId > 0)
                    db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
                db.AddInParameter(dbCommand, "ActualElecAmount", DbType.Decimal, actualElecAmount);
                db.AddInParameter(dbCommand, "QuotaElecAmount", DbType.Decimal, quotaElecAmount);
                db.AddInParameter(dbCommand, "WaterMonthlyLimit", DbType.Decimal, waterMonthlyLimit);
                db.AddInParameter(dbCommand, "Difference", DbType.Int32, difference);
                db.AddInParameter(dbCommand, "ExcessWT4210", DbType.Decimal, excessWT4210);
                db.AddInParameter(dbCommand, "QuotaWaterAmount", DbType.Decimal, quotaWaterAmount);
                db.AddInParameter(dbCommand, "ActualWaterAmount", DbType.Decimal, actualWaterAmount);
                db.AddInParameter(dbCommand, "FBVWT1552", DbType.Decimal, fBVWT1552);
                db.AddInParameter(dbCommand, "WEAllowanceMakeUp", DbType.Decimal, wEAllowanceMakeUp);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                payrollSubmissionId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Payroll_Submission table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionUpdate"))
            {
                db.AddInParameter(dbCommand, "PayrollSubmissionId", DbType.Int32, payrollSubmissionId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (employeeId > 0)
                    db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                if (invoiceId > 0)
                    db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
                db.AddInParameter(dbCommand, "ActualElecAmount", DbType.Decimal, actualElecAmount);
                db.AddInParameter(dbCommand, "QuotaElecAmount", DbType.Decimal, quotaElecAmount);
                db.AddInParameter(dbCommand, "WaterMonthlyLimit", DbType.Decimal, waterMonthlyLimit);
                db.AddInParameter(dbCommand, "Difference", DbType.Int32, difference);
                db.AddInParameter(dbCommand, "ExcessWT4210", DbType.Decimal, excessWT4210);
                db.AddInParameter(dbCommand, "QuotaWaterAmount", DbType.Decimal, quotaWaterAmount);
                db.AddInParameter(dbCommand, "ActualWaterAmount", DbType.Decimal, actualWaterAmount);
                db.AddInParameter(dbCommand, "FBVWT1552", DbType.Decimal, fBVWT1552);
                db.AddInParameter(dbCommand, "WEAllowanceMakeUp", DbType.Decimal, wEAllowanceMakeUp);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Payroll_Submission table by a composite primary key.
        /// </summary>
        /// <param name="payrollSubmissionId"></param>
        /// <returns>String</returns>
        public static string Delete(int payrollSubmissionId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionDelete"))
            {
                db.AddInParameter(dbCommand, "PayrollSubmissionId", DbType.Int32, payrollSubmissionId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Payroll_Submission is already in use. You can not delete this Payroll_Submission.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Deletes a record from the Payroll_Submission table by a foreign key.
        /// </summary>
        /// <param name="invoiceId"></param>
        public static void DeleteByInvoiceId(int invoiceId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionDeleteByInvoiceId"))
            {
                db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Payroll_Submission table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        public static void DeleteByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionDeleteByHouseId"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Payroll_Submission table by a foreign key.
        /// </summary>
        /// <param name="employeeId"></param>
        public static void DeleteByEmployeeId(int employeeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionDeleteByEmployeeId"))
            {
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Selects a single record from the Payroll_Submission table.
        /// </summary>
        /// <param name="payrollSubmissionId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int payrollSubmissionId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionSelect"))
            {
                db.AddInParameter(dbCommand, "PayrollSubmissionId", DbType.Int32, payrollSubmissionId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects records from the Payroll_Submission table by a foreign key.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>List</returns>
        public static Payroll_SubmissionList SelectByInvoiceId(int invoiceId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionSelectByInvoiceId"))
            {
                db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects records from the Payroll_Submission table by a foreign key.
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns>List</returns>
        public static Payroll_SubmissionList SelectByHouseId(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionSelectByHouseId"))
            {
                db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects records from the Payroll_Submission table by a foreign key.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>List</returns>
        public static Payroll_SubmissionList SelectByEmployeeId(int employeeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionSelectByEmployeeId"))
            {
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Selects all records from the Payroll_Submission table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static Payroll_SubmissionList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Payroll_Submission table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public Payroll_SubmissionList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Payroll_SubmissionSearch"))
            {
                if (payrollSubmissionId > 0)
                    db.AddInParameter(dbCommand, "PayrollSubmissionId", DbType.Int32, payrollSubmissionId);
                if (houseId > 0)
                    db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
                if (employeeId > 0)
                    db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                if (invoiceId > 0)
                    db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
                if (periodId > 0)
                    db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
                if (actualElecAmount > 0)
                    db.AddInParameter(dbCommand, "ActualElecAmount", DbType.Decimal, actualElecAmount);
                if (quotaElecAmount > 0)
                    db.AddInParameter(dbCommand, "QuotaElecAmount", DbType.Decimal, quotaElecAmount);
                if (waterMonthlyLimit > 0)
                    db.AddInParameter(dbCommand, "WaterMonthlyLimit", DbType.Decimal, waterMonthlyLimit);
                if (difference > 0)
                    db.AddInParameter(dbCommand, "Difference", DbType.Int32, difference);
                if (excessWT4210 > 0)
                    db.AddInParameter(dbCommand, "ExcessWT4210", DbType.Decimal, excessWT4210);
                if (quotaWaterAmount > 0)
                    db.AddInParameter(dbCommand, "QuotaWaterAmount", DbType.Decimal, quotaWaterAmount);
                if (actualWaterAmount > 0)
                    db.AddInParameter(dbCommand, "ActualWaterAmount", DbType.Decimal, actualWaterAmount);
                if (fBVWT1552 > 0)
                    db.AddInParameter(dbCommand, "FBVWT1552", DbType.Decimal, fBVWT1552);
                if (wEAllowanceMakeUp > 0)
                    db.AddInParameter(dbCommand, "WEAllowanceMakeUp", DbType.Decimal, wEAllowanceMakeUp);
                if (created_UserId > 0)
                    db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
