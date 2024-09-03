using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Employee_House table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>05-Sep-2013</CreatedDate>
	public partial class Employee_House
	{
		#region Fields

		private int employeeHouseId;
		private int employeeId;
		private int houseId;
		private DateTime created_TS;
		private int created_UserId;
		private DateTime updated_TS;
		private int updated_UserId;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Employee_House class.
		/// </summary>
		public Employee_House()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Employee_House class.
		/// </summary>
		public Employee_House(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Employee_House class.
		/// </summary>
		public Employee_House(int employeeHouseId)
		{
			DataSet ds = Select(employeeHouseId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the EmployeeHouseId value.
		/// </summary>
		public virtual int EmployeeHouseId
		{
			get { return employeeHouseId; }
			set { employeeHouseId = value; }
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
		/// Gets or sets the HouseId value.
		/// </summary>
		public virtual int HouseId
		{
			get { return houseId; }
			set { houseId = value; }
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
			this.employeeId = 0;
			this.houseId = 0;
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
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
			if (dt.Columns.Contains("EmployeeHouseId"))
			{
				int.TryParse(Convert.ToString(dr["EmployeeHouseId"]), out employeeHouseId );
			}
			if (dt.Columns.Contains("EmployeeId"))
			{
				int.TryParse(Convert.ToString(dr["EmployeeId"]), out employeeId );
			}
			if (dt.Columns.Contains("HouseId"))
			{
				int.TryParse(Convert.ToString(dr["HouseId"]), out houseId );
			}
			if (dt.Columns.Contains("Created_TS"))
			{
				DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS );
			}
			if (dt.Columns.Contains("Created_UserId"))
			{
				int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId );
			}
			if (dt.Columns.Contains("Updated_TS"))
			{
				DateTime.TryParse(Convert.ToString(dr["Updated_TS"]), out updated_TS );
			}
			if (dt.Columns.Contains("Updated_UserId"))
			{
				int.TryParse(Convert.ToString(dr["Updated_UserId"]), out updated_UserId );
			}
		}

		/// <summary>
		/// Saves a record to the tbl_Employee_House table.
		/// </summary>
		public void Save()
		{
			if(employeeHouseId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Employee_House table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseInsert"))
			{
				if(employeeId > 0) 
					db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
				if(houseId > 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				employeeHouseId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Employee_House table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseUpdate"))
			{
				db.AddInParameter(dbCommand, "EmployeeHouseId", DbType.Int32, employeeHouseId);
				if(employeeId > 0) 
					db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
				if(houseId > 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the Employee_House table by a composite primary key.
		/// </summary>
		/// <param name="employeeHouseId"></param>
		/// <returns>String</returns>
		public static string Delete(int employeeHouseId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseDelete"))
			{
				db.AddInParameter(dbCommand, "EmployeeHouseId", DbType.Int32, employeeHouseId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Employee_House is already in use. You can not delete this Employee_House.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Deletes a record from the Employee_House table by a foreign key.
		/// </summary>
		/// <param name="houseId"></param>
		public static void DeleteByHouseId(int houseId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseDeleteByHouseId"))
			{
				db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the Employee_House table by a foreign key.
		/// </summary>
		/// <param name="employeeId"></param>
		public static void DeleteByEmployeeId(int employeeId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseDeleteByEmployeeId"))
			{
				db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Selects a single record from the Employee_House table.
		/// </summary>
		/// <param name="employeeHouseId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int employeeHouseId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseSelect"))
			{
				db.AddInParameter(dbCommand, "EmployeeHouseId", DbType.Int32, employeeHouseId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects records from the Employee_House table by a foreign key.
		/// </summary>
		/// <param name="houseId"></param>
		/// <returns>List</returns>
		public static Employee_HouseList SelectByHouseId(int houseId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseSelectByHouseId"))
			{
				db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Selects records from the Employee_House table by a foreign key.
		/// </summary>
		/// <param name="employeeId"></param>
		/// <returns>List</returns>
		public static Employee_HouseList SelectByEmployeeId(int employeeId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseSelectByEmployeeId"))
			{
				db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Selects all records from the Employee_House table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static Employee_HouseList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Employee_House table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public Employee_HouseList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Employee_HouseSearch"))
			{
				if(employeeHouseId> 0) 
					db.AddInParameter(dbCommand, "EmployeeHouseId", DbType.Int32, employeeHouseId);
				if(employeeId> 0) 
					db.AddInParameter(dbCommand, "EmployeeId", DbType.Int32, employeeId);
				if(houseId> 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				if(created_UserId> 0) 
					db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
				if(updated_UserId> 0) 
					db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		#endregion
	}
}
