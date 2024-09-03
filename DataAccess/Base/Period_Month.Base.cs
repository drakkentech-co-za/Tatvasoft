using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Period_Month table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>19-Sep-2013</CreatedDate>
	public partial class Period_Month
	{
		#region Fields

		private int periodMonthId;
		private int periodId;
		private int month;
		private int year;
		private int created_UserId;
		private DateTime created_TS;
		private int updated_UserId;
		private DateTime updated_TS;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Period_Month class.
		/// </summary>
		public Period_Month()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Period_Month class.
		/// </summary>
		public Period_Month(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Period_Month class.
		/// </summary>
		public Period_Month(int periodMonthId)
		{
			DataSet ds = Select(periodMonthId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the PeriodMonthId value.
		/// </summary>
		public virtual int PeriodMonthId
		{
			get { return periodMonthId; }
			set { periodMonthId = value; }
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
		/// Gets or sets the Month value.
		/// </summary>
		public virtual int Month
		{
			get { return month; }
			set { month = value; }
		}

		/// <summary>
		/// Gets or sets the Year value.
		/// </summary>
		public virtual int Year
		{
			get { return year; }
			set { year = value; }
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

		/// <summary>
		/// Gets or sets the Updated_UserId value.
		/// </summary>
		public virtual int Updated_UserId
		{
			get { return updated_UserId; }
			set { updated_UserId = value; }
		}

		/// <summary>
		/// Gets or sets the Updated_TS value.
		/// </summary>
		public virtual DateTime Updated_TS
		{
			get { return updated_TS; }
			set { updated_TS = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initialize Variables
		/// </summary>
		public void InitVariables()
		{
			this.periodId = 0;
			this.month = 0;
			this.year = 0;
			this.created_UserId = 0;
			this.created_TS = DateTime.MinValue;
			this.updated_UserId = 0;
			this.updated_TS = DateTime.MinValue;
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
			if (dt.Columns.Contains("PeriodMonthId"))
			{
				int.TryParse(Convert.ToString(dr["PeriodMonthId"]), out periodMonthId );
			}
			if (dt.Columns.Contains("PeriodId"))
			{
				int.TryParse(Convert.ToString(dr["PeriodId"]), out periodId );
			}
			if (dt.Columns.Contains("Month"))
			{
				int.TryParse(Convert.ToString(dr["Month"]), out month );
			}
			if (dt.Columns.Contains("Year"))
			{
				int.TryParse(Convert.ToString(dr["Year"]), out year );
			}
			if (dt.Columns.Contains("Created_UserId"))
			{
				int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId );
			}
			if (dt.Columns.Contains("Created_TS"))
			{
				DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS );
			}
			if (dt.Columns.Contains("Updated_UserId"))
			{
				int.TryParse(Convert.ToString(dr["Updated_UserId"]), out updated_UserId );
			}
			if (dt.Columns.Contains("Updated_TS"))
			{
				DateTime.TryParse(Convert.ToString(dr["Updated_TS"]), out updated_TS );
			}
		}

		/// <summary>
		/// Saves a record to the tbl_Period_Month table.
		/// </summary>
		public void Save()
		{
			if(periodMonthId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Period_Month table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Period_MonthInsert"))
			{
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "Month", DbType.Int32, month);
				db.AddInParameter(dbCommand, "Year", DbType.Int32, year);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				periodMonthId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Period_Month table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Period_MonthUpdate"))
			{
				db.AddInParameter(dbCommand, "PeriodMonthId", DbType.Int32, periodMonthId);
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "Month", DbType.Int32, month);
				db.AddInParameter(dbCommand, "Year", DbType.Int32, year);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the Period_Month table by a composite primary key.
		/// </summary>
		/// <param name="periodMonthId"></param>
		/// <returns>String</returns>
		public static string Delete(int periodMonthId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Period_MonthDelete"))
			{
				db.AddInParameter(dbCommand, "PeriodMonthId", DbType.Int32, periodMonthId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Period_Month is already in use. You can not delete this Period_Month.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the Period_Month table.
		/// </summary>
		/// <param name="periodMonthId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int periodMonthId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Period_MonthSelect"))
			{
				db.AddInParameter(dbCommand, "PeriodMonthId", DbType.Int32, periodMonthId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the Period_Month table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static Period_MonthList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Period_MonthSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Period_Month table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public Period_MonthList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Period_MonthSearch"))
			{
				if(periodMonthId> 0) 
					db.AddInParameter(dbCommand, "PeriodMonthId", DbType.Int32, periodMonthId);
				if(periodId> 0) 
					db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				if(month> 0) 
					db.AddInParameter(dbCommand, "Month", DbType.Int32, month);
				if(year> 0) 
					db.AddInParameter(dbCommand, "Year", DbType.Int32, year);
				if(created_UserId> 0) 
					db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				if(updated_UserId> 0) 
					db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		#endregion
	}
}
