using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Token_Period table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>18-Sep-2013</CreatedDate>
	public partial class Token_Period
	{
		#region Fields

		private int periodId;
		private int startDay;
		private int endDay;
		private bool isCurrent;
		private DateTime created_TS;
		private int created_UserId;
		private DateTime updated_TS;
		private int updated_UserId;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Token_Period class.
		/// </summary>
		public Token_Period()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Token_Period class.
		/// </summary>
		public Token_Period(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Token_Period class.
		/// </summary>
		public Token_Period(int periodId)
		{
			DataSet ds = Select(periodId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the PeriodId value.
		/// </summary>
		public virtual int PeriodId
		{
			get { return periodId; }
			set { periodId = value; }
		}

		/// <summary>
		/// Gets or sets the StartDay value.
		/// </summary>
		public virtual int StartDay
		{
			get { return startDay; }
			set { startDay = value; }
		}

		/// <summary>
		/// Gets or sets the EndDay value.
		/// </summary>
		public virtual int EndDay
		{
			get { return endDay; }
			set { endDay = value; }
		}

		/// <summary>
		/// Gets or sets the IsCurrent value.
		/// </summary>
		public virtual bool IsCurrent
		{
			get { return isCurrent; }
			set { isCurrent = value; }
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
			this.startDay = 0;
			this.endDay = 0;
			this.isCurrent = false;
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
			if (dt.Columns.Contains("PeriodId"))
			{
				int.TryParse(Convert.ToString(dr["PeriodId"]), out periodId );
			}
			if (dt.Columns.Contains("StartDay"))
			{
				int.TryParse(Convert.ToString(dr["StartDay"]), out startDay );
			}
			if (dt.Columns.Contains("EndDay"))
			{
				int.TryParse(Convert.ToString(dr["EndDay"]), out endDay );
			}
			if (dt.Columns.Contains("IsCurrent"))
			{
				bool.TryParse(Convert.ToString(dr["IsCurrent"]), out isCurrent );
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
		/// Saves a record to the tbl_Token_Period table.
		/// </summary>
		public void Save()
		{
			if(periodId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Token_Period table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_PeriodInsert"))
			{
				db.AddInParameter(dbCommand, "StartDay", DbType.Int32, startDay);
				db.AddInParameter(dbCommand, "EndDay", DbType.Int32, endDay);
				db.AddInParameter(dbCommand, "IsCurrent", DbType.Boolean, isCurrent);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				periodId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Token_Period table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_PeriodUpdate"))
			{
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "StartDay", DbType.Int32, startDay);
				db.AddInParameter(dbCommand, "EndDay", DbType.Int32, endDay);
				db.AddInParameter(dbCommand, "IsCurrent", DbType.Boolean, isCurrent);
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
		/// Deletes a record from the Token_Period table by a composite primary key.
		/// </summary>
		/// <param name="periodId"></param>
		/// <returns>String</returns>
		public static string Delete(int periodId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_PeriodDelete"))
			{
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Token_Period is already in use. You can not delete this Token_Period.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the Token_Period table.
		/// </summary>
		/// <param name="periodId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int periodId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_PeriodSelect"))
			{
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the Token_Period table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static Token_PeriodList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_PeriodSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Token_Period table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public Token_PeriodList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_PeriodSearch"))
			{
				if(periodId> 0) 
					db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				if(startDay> 0) 
					db.AddInParameter(dbCommand, "StartDay", DbType.Int32, startDay);
				if(endDay> 0) 
					db.AddInParameter(dbCommand, "EndDay", DbType.Int32, endDay);
				if(isCurrent != null) 
					db.AddInParameter(dbCommand, "IsCurrent", DbType.Boolean, isCurrent);
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
