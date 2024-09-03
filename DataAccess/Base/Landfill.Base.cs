using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Landfill table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>21-Oct-2013</CreatedDate>
	public partial class Landfill
	{
		#region Fields

		private int landfillId;
		private int periodId;
		private decimal landfillAmount;
		private DateTime created_TS;
		private int created_UserId;
		private DateTime updated_TS;
		private int updated_UserId;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Landfill class.
		/// </summary>
		public Landfill()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Landfill class.
		/// </summary>
		public Landfill(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Landfill class.
		/// </summary>
		public Landfill(int landfillId)
		{
			DataSet ds = Select(landfillId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the LandfillId value.
		/// </summary>
		public virtual int LandfillId
		{
			get { return landfillId; }
			set { landfillId = value; }
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
		/// Gets or sets the LandfillAmount value.
		/// </summary>
		public virtual decimal LandfillAmount
		{
			get { return landfillAmount; }
			set { landfillAmount = value; }
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
			this.periodId = 0;
			this.landfillAmount = 0;
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
			if (dt.Columns.Contains("LandfillId"))
			{
				int.TryParse(Convert.ToString(dr["LandfillId"]), out landfillId );
			}
			if (dt.Columns.Contains("PeriodId"))
			{
				int.TryParse(Convert.ToString(dr["PeriodId"]), out periodId );
			}
			if (dt.Columns.Contains("LandfillAmount"))
			{
				Decimal.TryParse(Convert.ToString(dr["LandfillAmount"]), out landfillAmount );
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
		/// Saves a record to the tbl_Landfill table.
		/// </summary>
		public void Save()
		{
			if(landfillId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Landfill table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_LandfillInsert"))
			{
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "LandfillAmount", DbType.Decimal, landfillAmount);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				landfillId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Landfill table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_LandfillUpdate"))
			{
				db.AddInParameter(dbCommand, "LandfillId", DbType.Int32, landfillId);
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "LandfillAmount", DbType.Decimal, landfillAmount);
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
		/// Deletes a record from the Landfill table by a composite primary key.
		/// </summary>
		/// <param name="landfillId"></param>
		/// <returns>String</returns>
		public static string Delete(int landfillId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_LandfillDelete"))
			{
				db.AddInParameter(dbCommand, "LandfillId", DbType.Int32, landfillId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Landfill is already in use. You can not delete this Landfill.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the Landfill table.
		/// </summary>
		/// <param name="landfillId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int landfillId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_LandfillSelect"))
			{
				db.AddInParameter(dbCommand, "LandfillId", DbType.Int32, landfillId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the Landfill table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static LandfillList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_LandfillSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Landfill table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public LandfillList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_LandfillSearch"))
			{
				if(landfillId> 0) 
					db.AddInParameter(dbCommand, "LandfillId", DbType.Int32, landfillId);
				if(periodId> 0) 
					db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				if(landfillAmount > 0) 
					db.AddInParameter(dbCommand, "LandfillAmount", DbType.Decimal, landfillAmount);
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
