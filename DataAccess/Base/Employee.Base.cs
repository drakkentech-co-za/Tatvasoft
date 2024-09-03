using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Employee table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>16-Sep-2013</CreatedDate>
    public partial class Employee
    {
        #region Fields

        private int employeeId;
        private string employeeNo;
        private string employeeName;
        private string employeeSurname;
        private string employeeEmail;
        private string employeePassword;
		private int employeeType;
        private DateTime created_TS;
        private int created_UserId;
        private DateTime updated_TS;
        private int updated_UserId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        public Employee()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        public Employee(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the Employee class.
        /// </summary>
        public Employee(int employeeId)
        {
            DataSet ds = Select(employeeId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the EmployeeId value.
        /// </summary>
        public virtual int EmployeeId
        {
            get { return employeeId; }
            set { employeeId = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeNo value.
        /// </summary>
        public virtual string EmployeeNo
        {
            get { return employeeNo; }
            set { employeeNo = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeName value.
        /// </summary>
        public virtual string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeSurname value.
        /// </summary>
        public virtual string EmployeeSurname
        {
            get { return employeeSurname; }
            set { employeeSurname = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeeEmail value.
        /// </summary>
        public virtual string EmployeeEmail
        {
            get { return employeeEmail; }
            set { employeeEmail = value; }
        }

        /// <summary>
        /// Gets or sets the EmployeePassword value.
        /// </summary>
        public virtual string EmployeePassword
        {
            get { return employeePassword; }
            set { employeePassword = value; }
        }

        /// <summary>
		/// Gets or sets the EmployeeType value.
		/// </summary>
		public virtual int EmployeeType
		{
			get { return employeeType; }
			set { employeeType = value; }
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
            this.employeeNo = string.Empty;
            this.employeeName = string.Empty;
            this.employeeSurname = string.Empty;
            this.employeeEmail = string.Empty;
            this.employeePassword = string.Empty;
			this.employeeType = 0;
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
            if (dt.Columns.Contains("EmployeeId"))
            {
                int.TryParse(Convert.ToString(dr["EmployeeId"]), out employeeId);
            }
            if (dt.Columns.Contains("EmployeeNo"))
            {
                employeeNo = Convert.ToString(dr["EmployeeNo"]);
            }
            if (dt.Columns.Contains("EmployeeName"))
            {
                employeeName = Convert.ToString(dr["EmployeeName"]);
            }
            if (dt.Columns.Contains("EmployeeSurname"))
            {
                employeeSurname = Convert.ToString(dr["EmployeeSurname"]);
            }
            if (dt.Columns.Contains("EmployeeEmail"))
            {
                employeeEmail = Convert.ToString(dr["EmployeeEmail"]);
            }
            if (dt.Columns.Contains("EmployeePassword"))
            {
                employeePassword = Convert.ToString(dr["EmployeePassword"]);
            }
			if (dt.Columns.Contains("EmployeeType"))
			{
				int.TryParse(Convert.ToString(dr["EmployeeType"]), out employeeType );
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
        /// Saves a record to the tbl_Employee table.
        /// </summary>
        public void Save()
        {
            if (employeeId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Employee table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeInsert"))
            {
                db.AddInParameter(dbCommand, "EmployeeNo", DbType.String, employeeNo);
                db.AddInParameter(dbCommand, "EmployeeName", DbType.String, employeeName);
                db.AddInParameter(dbCommand, "EmployeeSurname", DbType.String, employeeSurname);
                db.AddInParameter(dbCommand, "EmployeeEmail", DbType.String, employeeEmail);
                db.AddInParameter(dbCommand, "EmployeePassword", DbType.String, employeePassword);
				db.AddInParameter(dbCommand, "EmployeeType", DbType.Int16, employeeType);
                if (created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
                if (updated_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
                db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                employeeId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Employee table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeUpdate"))
            {
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                db.AddInParameter(dbCommand, "EmployeeNo", DbType.String, employeeNo);
                db.AddInParameter(dbCommand, "EmployeeName", DbType.String, employeeName);
                db.AddInParameter(dbCommand, "EmployeeSurname", DbType.String, employeeSurname);
                db.AddInParameter(dbCommand, "EmployeeEmail", DbType.String, employeeEmail);
                db.AddInParameter(dbCommand, "EmployeePassword", DbType.String, employeePassword);
				db.AddInParameter(dbCommand, "EmployeeType", DbType.Int16, employeeType);
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
        /// Deletes a record from the Employee table by a composite primary key.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>String</returns>
        public static string Delete(int employeeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeDelete"))
            {
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Employee is already in use. You can not delete this Employee.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the Employee table.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int employeeId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeSelect"))
            {
                db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the Employee table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static EmployeeList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Employee table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public EmployeeList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_EmployeeSearch"))
            {
                if (employeeId > 0)
                    db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
                if (employeeNo != string.Empty)
                    db.AddInParameter(dbCommand, "EmployeeNo", DbType.String, employeeNo);
                if (employeeName != string.Empty)
                    db.AddInParameter(dbCommand, "EmployeeName", DbType.String, employeeName);
                if (employeeSurname != string.Empty)
                    db.AddInParameter(dbCommand, "EmployeeSurname", DbType.String, employeeSurname);
                if (employeeEmail != string.Empty)
                    db.AddInParameter(dbCommand, "EmployeeEmail", DbType.String, employeeEmail);
                if (employeePassword != string.Empty)
                    db.AddInParameter(dbCommand, "EmployeePassword", DbType.String, employeePassword);
				if(employeeType > 0) 
					db.AddInParameter(dbCommand, "EmployeeType", DbType.Int16, employeeType);
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
