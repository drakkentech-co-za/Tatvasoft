using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Invoice table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>18-Sep-2013</CreatedDate>
	public partial class Invoice
	{
		#region Fields

		private int invoiceId;
		private int houseId;
		private int periodId;
		private decimal rTotal;
		private decimal sTotal;
		private bool isDifferent;
		private decimal difference;
		private bool isAccepted;
		private string reason;
		private DateTime created_TS;
		private int created_UserId;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Invoice class.
		/// </summary>
		public Invoice()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Invoice class.
		/// </summary>
		public Invoice(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Invoice class.
		/// </summary>
		public Invoice(int invoiceId)
		{
			DataSet ds = Select(invoiceId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the InvoiceId value.
		/// </summary>
		public virtual int InvoiceId
		{
			get { return invoiceId; }
			set { invoiceId = value; }
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
		/// Gets or sets the PeriodId value.
		/// </summary>
		public virtual int PeriodId
		{
			get { return periodId; }
			set { periodId = value; }
		}

		/// <summary>
		/// Gets or sets the RTotal value.
		/// </summary>
		public virtual decimal RTotal
		{
			get { return rTotal; }
			set { rTotal = value; }
		}

		/// <summary>
		/// Gets or sets the STotal value.
		/// </summary>
		public virtual decimal STotal
		{
			get { return sTotal; }
			set { sTotal = value; }
		}

		/// <summary>
		/// Gets or sets the IsDifferent value.
		/// </summary>
		public virtual bool IsDifferent
		{
			get { return isDifferent; }
			set { isDifferent = value; }
		}

		/// <summary>
		/// Gets or sets the Difference value.
		/// </summary>
		public virtual decimal Difference
		{
			get { return difference; }
			set { difference = value; }
		}

		/// <summary>
		/// Gets or sets the IsAccepted value.
		/// </summary>
		public virtual bool IsAccepted
		{
			get { return isAccepted; }
			set { isAccepted = value; }
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
			this.houseId = 0;
			this.periodId = 0;
			this.rTotal = 0;
			this.sTotal = 0;
			this.isDifferent = false;
			this.difference = 0;
			this.isAccepted = false;
			this.reason = string.Empty;
			this.created_TS = DateTime.MinValue;
			this.created_UserId = 0;
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
			if (dt.Columns.Contains("InvoiceId"))
			{
				int.TryParse(Convert.ToString(dr["InvoiceId"]), out invoiceId );
			}
			if (dt.Columns.Contains("HouseId"))
			{
				int.TryParse(Convert.ToString(dr["HouseId"]), out houseId );
			}
			if (dt.Columns.Contains("PeriodId"))
			{
				int.TryParse(Convert.ToString(dr["PeriodId"]), out periodId );
			}
			if (dt.Columns.Contains("RTotal"))
			{
				Decimal.TryParse(Convert.ToString(dr["RTotal"]), out rTotal );
			}
			if (dt.Columns.Contains("STotal"))
			{
				Decimal.TryParse(Convert.ToString(dr["STotal"]), out sTotal );
			}
			if (dt.Columns.Contains("IsDifferent"))
			{
				bool.TryParse(Convert.ToString(dr["IsDifferent"]), out isDifferent );
			}
			if (dt.Columns.Contains("Difference"))
			{
				Decimal.TryParse(Convert.ToString(dr["Difference"]), out difference );
			}
			if (dt.Columns.Contains("IsAccepted"))
			{
				bool.TryParse(Convert.ToString(dr["IsAccepted"]), out isAccepted );
			}
			if (dt.Columns.Contains("Reason"))
			{
				reason = Convert.ToString(dr["Reason"]);
			}
			if (dt.Columns.Contains("Created_TS"))
			{
				DateTime.TryParse(Convert.ToString(dr["Created_TS"]), out created_TS );
			}
			if (dt.Columns.Contains("Created_UserId"))
			{
				int.TryParse(Convert.ToString(dr["Created_UserId"]), out created_UserId );
			}
		}

		/// <summary>
		/// Saves a record to the tbl_Invoice table.
		/// </summary>
		public void Save()
		{
			if(invoiceId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Invoice table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceInsert"))
			{
				if(houseId > 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "RTotal", DbType.Decimal, rTotal);
				db.AddInParameter(dbCommand, "STotal", DbType.Decimal, sTotal);
				db.AddInParameter(dbCommand, "IsDifferent", DbType.Boolean, isDifferent);
				db.AddInParameter(dbCommand, "Difference", DbType.Decimal, difference);
				db.AddInParameter(dbCommand, "IsAccepted", DbType.Boolean, isAccepted);
				db.AddInParameter(dbCommand, "Reason", DbType.String, reason);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				invoiceId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Invoice table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceUpdate"))
			{
				db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
				if(houseId > 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				db.AddInParameter(dbCommand, "RTotal", DbType.Decimal, rTotal);
				db.AddInParameter(dbCommand, "STotal", DbType.Decimal, sTotal);
				db.AddInParameter(dbCommand, "IsDifferent", DbType.Boolean, isDifferent);
				db.AddInParameter(dbCommand, "Difference", DbType.Decimal, difference);
				db.AddInParameter(dbCommand, "IsAccepted", DbType.Boolean, isAccepted);
				db.AddInParameter(dbCommand, "Reason", DbType.String, reason);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the Invoice table by a composite primary key.
		/// </summary>
		/// <param name="invoiceId"></param>
		/// <returns>String</returns>
		public static string Delete(int invoiceId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceDelete"))
			{
				db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Invoice is already in use. You can not delete this Invoice.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Deletes a record from the Invoice table by a foreign key.
		/// </summary>
		/// <param name="houseId"></param>
		public static void DeleteByHouseId(int houseId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceDeleteByHouseId"))
			{
				db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Selects a single record from the Invoice table.
		/// </summary>
		/// <param name="invoiceId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int invoiceId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceSelect"))
			{
				db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects records from the Invoice table by a foreign key.
		/// </summary>
		/// <param name="houseId"></param>
		/// <returns>List</returns>
		public static InvoiceList SelectByHouseId(int houseId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceSelectByHouseId"))
			{
				db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Selects all records from the Invoice table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static InvoiceList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Invoice table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public InvoiceList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_InvoiceSearch"))
			{
				if(invoiceId> 0) 
					db.AddInParameter(dbCommand, "InvoiceId", DbType.Int32, invoiceId);
				if(houseId> 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				if(periodId> 0) 
					db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
				if(rTotal > 0) 
					db.AddInParameter(dbCommand, "RTotal", DbType.Decimal, rTotal);
				if(sTotal > 0) 
					db.AddInParameter(dbCommand, "STotal", DbType.Decimal, sTotal);
				if(isDifferent != null) 
					db.AddInParameter(dbCommand, "IsDifferent", DbType.Boolean, isDifferent);
				if(difference > 0) 
					db.AddInParameter(dbCommand, "Difference", DbType.Decimal, difference);
				if(isAccepted != null) 
					db.AddInParameter(dbCommand, "IsAccepted", DbType.Boolean, isAccepted);
				if(reason != string.Empty) 
					db.AddInParameter(dbCommand, "Reason", DbType.String, reason);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				if(created_UserId> 0) 
					db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		#endregion
	}
}
