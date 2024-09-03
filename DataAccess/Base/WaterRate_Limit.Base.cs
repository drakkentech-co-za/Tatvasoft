using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for WaterRate_Limit table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>16-Sep-2013</CreatedDate>
	public partial class WaterRate_Limit
	{
		#region Fields

		private int waterRateLimitId;
		private DateTime startDate;
		private DateTime endDate;
		private decimal rate1;
		private decimal rate2;
		private decimal rate3;
        private int houseTypeId;
       
		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the WaterRate_Limit class.
		/// </summary>
		public WaterRate_Limit()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the WaterRate_Limit class.
		/// </summary>
		public WaterRate_Limit(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the WaterRate_Limit class.
		/// </summary>
		public WaterRate_Limit(int waterRateLimitId)
		{
			DataSet ds = Select(waterRateLimitId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the WaterRateLimitId value.
		/// </summary>
		public virtual int WaterRateLimitId
		{
			get { return waterRateLimitId; }
			set { waterRateLimitId = value; }
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
		/// Gets or sets the Rate1 value.
		/// </summary>
		public virtual decimal Rate1
		{
			get { return rate1; }
			set { rate1 = value; }
		}

		/// <summary>
		/// Gets or sets the Rate2 value.
		/// </summary>
		public virtual decimal Rate2
		{
			get { return rate2; }
			set { rate2 = value; }
		}

		/// <summary>
		/// Gets or sets the Rate3 value.
		/// </summary>
		public virtual decimal Rate3
		{
			get { return rate3; }
			set { rate3 = value; }
		}

        /// <summary>
        /// Gets or sets the HouseTypeId value.
        /// </summary>
        public int HouseTypeId
        {
            get { return houseTypeId; }
            set { houseTypeId = value; }
        }
		#endregion

		#region Methods

		/// <summary>
		/// Initialize Variables
		/// </summary>
		public void InitVariables()
		{
			this.startDate = DateTime.MinValue;
			this.endDate = DateTime.MinValue;
			this.rate1 = 0;
			this.rate2 = 0;
			this.rate3 = 0;
            this.houseTypeId = 0;
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
			if (dt.Columns.Contains("WaterRateLimitId"))
			{
				int.TryParse(Convert.ToString(dr["WaterRateLimitId"]), out waterRateLimitId );
			}
			if (dt.Columns.Contains("StartDate"))
			{
				DateTime.TryParse(Convert.ToString(dr["StartDate"]), out startDate );
			}
			if (dt.Columns.Contains("EndDate"))
			{
				DateTime.TryParse(Convert.ToString(dr["EndDate"]), out endDate );
			}
			if (dt.Columns.Contains("Rate1"))
			{
				Decimal.TryParse(Convert.ToString(dr["Rate1"]), out rate1 );
			}
			if (dt.Columns.Contains("Rate2"))
			{
				Decimal.TryParse(Convert.ToString(dr["Rate2"]), out rate2 );
			}
			if (dt.Columns.Contains("Rate3"))
			{
				Decimal.TryParse(Convert.ToString(dr["Rate3"]), out rate3 );
			}
            if (dt.Columns.Contains("HouseTypeId"))
            {                
                int.TryParse(Convert.ToString(dr["HouseTypeId"]), out houseTypeId);
            }
		}

		/// <summary>
		/// Saves a record to the tbl_WaterRate_Limit table.
		/// </summary>
		public void Save()
		{
			if(waterRateLimitId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the WaterRate_Limit table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_WaterRate_LimitInsert"))
			{
				if(startDate != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
				if(endDate != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
				db.AddInParameter(dbCommand, "Rate1", DbType.Decimal, rate1);
				db.AddInParameter(dbCommand, "Rate2", DbType.Decimal, rate2);
				db.AddInParameter(dbCommand, "Rate3", DbType.Decimal, rate3);
                db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				waterRateLimitId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the WaterRate_Limit table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_WaterRate_LimitUpdate"))
			{
				db.AddInParameter(dbCommand, "WaterRateLimitId", DbType.Int32, waterRateLimitId);
				if(startDate != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
				if(endDate != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
				db.AddInParameter(dbCommand, "Rate1", DbType.Decimal, rate1);
				db.AddInParameter(dbCommand, "Rate2", DbType.Decimal, rate2);
				db.AddInParameter(dbCommand, "Rate3", DbType.Decimal, rate3);
                db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the WaterRate_Limit table by a composite primary key.
		/// </summary>
		/// <param name="waterRateLimitId"></param>
		/// <returns>String</returns>
		public static string Delete(int waterRateLimitId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_WaterRate_LimitDelete"))
			{
				db.AddInParameter(dbCommand, "WaterRateLimitId", DbType.Int32, waterRateLimitId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "WaterRate_Limit is already in use. You can not delete this WaterRate_Limit.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the WaterRate_Limit table.
		/// </summary>
		/// <param name="waterRateLimitId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int waterRateLimitId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_WaterRate_LimitSelect"))
			{
				db.AddInParameter(dbCommand, "WaterRateLimitId", DbType.Int32, waterRateLimitId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the WaterRate_Limit table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static WaterRate_LimitList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_WaterRate_LimitSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the WaterRate_Limit table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public WaterRate_LimitList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_WaterRate_LimitSearch"))
			{
				if(waterRateLimitId> 0) 
					db.AddInParameter(dbCommand, "WaterRateLimitId", DbType.Int32, waterRateLimitId);
				if(startDate != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, startDate);
				if(endDate != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, endDate);
				if(rate1 > 0) 
					db.AddInParameter(dbCommand, "Rate1", DbType.Decimal, rate1);
				if(rate2 > 0) 
					db.AddInParameter(dbCommand, "Rate2", DbType.Decimal, rate2);
				if(rate3 > 0) 
					db.AddInParameter(dbCommand, "Rate3", DbType.Decimal, rate3);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		#endregion
	}
}
