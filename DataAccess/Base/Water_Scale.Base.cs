using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Water_Scale table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>10-Sep-2013</CreatedDate>
	public partial class Water_Scale
	{
		#region Fields

		private int waterScaleId;
		private DateTime dateFrom;
		private DateTime dateTo;
		private float limit;
		private float multiplicationFactor;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Water_Scale class.
		/// </summary>
		public Water_Scale()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Water_Scale class.
		/// </summary>
		public Water_Scale(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Water_Scale class.
		/// </summary>
		public Water_Scale(int waterScaleId)
		{
			DataSet ds = Select(waterScaleId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the WaterScaleId value.
		/// </summary>
		public virtual int WaterScaleId
		{
			get { return waterScaleId; }
			set { waterScaleId = value; }
		}

		/// <summary>
		/// Gets or sets the DateFrom value.
		/// </summary>
		public virtual DateTime DateFrom
		{
			get { return dateFrom; }
			set { dateFrom = value; }
		}

		/// <summary>
		/// Gets or sets the DateTo value.
		/// </summary>
		public virtual DateTime DateTo
		{
			get { return dateTo; }
			set { dateTo = value; }
		}

		/// <summary>
		/// Gets or sets the Limit value.
		/// </summary>
		public virtual float Limit
		{
			get { return limit; }
			set { limit = value; }
		}

		/// <summary>
		/// Gets or sets the MultiplicationFactor value.
		/// </summary>
		public virtual float MultiplicationFactor
		{
			get { return multiplicationFactor; }
			set { multiplicationFactor = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initialize Variables
		/// </summary>
		public void InitVariables()
		{
			this.dateFrom = DateTime.MinValue;
			this.dateTo = DateTime.MinValue;
			this.limit = 0;
			this.multiplicationFactor = 0;
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
			if (dt.Columns.Contains("WaterScaleId"))
			{
				int.TryParse(Convert.ToString(dr["WaterScaleId"]), out waterScaleId );
			}
			if (dt.Columns.Contains("DateFrom"))
			{
				DateTime.TryParse(Convert.ToString(dr["DateFrom"]), out dateFrom );
			}
			if (dt.Columns.Contains("DateTo"))
			{
				DateTime.TryParse(Convert.ToString(dr["DateTo"]), out dateTo );
			}
			if (dt.Columns.Contains("Limit"))
			{
				float.TryParse(Convert.ToString(dr["Limit"]), out limit );
			}
			if (dt.Columns.Contains("MultiplicationFactor"))
			{
				float.TryParse(Convert.ToString(dr["MultiplicationFactor"]), out multiplicationFactor );
			}
		}

		/// <summary>
		/// Saves a record to the tbl_Water_Scale table.
		/// </summary>
		public void Save()
		{
			if(waterScaleId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Water_Scale table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Water_ScaleInsert"))
			{
				if(dateFrom != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, dateFrom);
				if(dateTo != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, dateTo);
				db.AddInParameter(dbCommand, "Limit", DbType.Double, limit);
				db.AddInParameter(dbCommand, "MultiplicationFactor", DbType.Double, multiplicationFactor);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				waterScaleId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Water_Scale table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Water_ScaleUpdate"))
			{
				db.AddInParameter(dbCommand, "WaterScaleId", DbType.Int32, waterScaleId);
				if(dateFrom != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, dateFrom);
				if(dateTo != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, dateTo);
				db.AddInParameter(dbCommand, "Limit", DbType.Double, limit);
				db.AddInParameter(dbCommand, "MultiplicationFactor", DbType.Double, multiplicationFactor);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the Water_Scale table by a composite primary key.
		/// </summary>
		/// <param name="waterScaleId"></param>
		/// <returns>String</returns>
		public static string Delete(int waterScaleId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Water_ScaleDelete"))
			{
				db.AddInParameter(dbCommand, "WaterScaleId", DbType.Int32, waterScaleId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Water_Scale is already in use. You can not delete this Water_Scale.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the Water_Scale table.
		/// </summary>
		/// <param name="waterScaleId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int waterScaleId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Water_ScaleSelect"))
			{
				db.AddInParameter(dbCommand, "WaterScaleId", DbType.Int32, waterScaleId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the Water_Scale table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static Water_ScaleList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Water_ScaleSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Water_Scale table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public Water_ScaleList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Water_ScaleSearch"))
			{
				if(waterScaleId> 0) 
					db.AddInParameter(dbCommand, "WaterScaleId", DbType.Int32, waterScaleId);
				if(dateFrom != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, dateFrom);
				if(dateTo != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, dateTo);
				if(limit > 0) 
					db.AddInParameter(dbCommand, "Limit", DbType.Double, limit);
				if(multiplicationFactor > 0) 
					db.AddInParameter(dbCommand, "MultiplicationFactor", DbType.Double, multiplicationFactor);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		#endregion
	}
}
