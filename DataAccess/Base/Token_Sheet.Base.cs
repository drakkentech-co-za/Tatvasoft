using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Token_Sheet table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>13-Sep-2013</CreatedDate>
	public partial class Token_Sheet
	{
		#region Fields

		private int tokenId;
		private int houseId;
		private int accountNo;
		private int electricityUnit;
		private string tokenNo;
		private int months;
		private int years;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Token_Sheet class.
		/// </summary>
		public Token_Sheet()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the Token_Sheet class.
		/// </summary>
		public Token_Sheet(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the Token_Sheet class.
		/// </summary>
		public Token_Sheet(int tokenId)
		{
			DataSet ds = Select(tokenId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the TokenId value.
		/// </summary>
		public virtual int TokenId
		{
			get { return tokenId; }
			set { tokenId = value; }
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
		/// Gets or sets the AccountNo value.
		/// </summary>
		public virtual int AccountNo
		{
			get { return accountNo; }
			set { accountNo = value; }
		}

		/// <summary>
		/// Gets or sets the ElectricityUnit value.
		/// </summary>
		public virtual int ElectricityUnit
		{
			get { return electricityUnit; }
			set { electricityUnit = value; }
		}

		/// <summary>
		/// Gets or sets the TokenNo value.
		/// </summary>
		public virtual string TokenNo
		{
			get { return tokenNo; }
			set { tokenNo = value; }
		}

		/// <summary>
		/// Gets or sets the Months value.
		/// </summary>
		public virtual int Months
		{
			get { return months; }
			set { months = value; }
		}

		/// <summary>
		/// Gets or sets the Years value.
		/// </summary>
		public virtual int Years
		{
			get { return years; }
			set { years = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initialize Variables
		/// </summary>
		public void InitVariables()
		{
			this.houseId = 0;
			this.accountNo = 0;
			this.electricityUnit = 0;
			this.tokenNo = string.Empty;
			this.months = 0;
			this.years = 0;
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
			if (dt.Columns.Contains("TokenId"))
			{
				int.TryParse(Convert.ToString(dr["TokenId"]), out tokenId );
			}
			if (dt.Columns.Contains("HouseId"))
			{
				int.TryParse(Convert.ToString(dr["HouseId"]), out houseId );
			}
			if (dt.Columns.Contains("AccountNo"))
			{
				int.TryParse(Convert.ToString(dr["AccountNo"]), out accountNo );
			}
			if (dt.Columns.Contains("ElectricityUnit"))
			{
				int.TryParse(Convert.ToString(dr["ElectricityUnit"]), out electricityUnit );
			}
			if (dt.Columns.Contains("TokenNo"))
			{
				tokenNo = Convert.ToString(dr["TokenNo"]);
			}
			if (dt.Columns.Contains("Months"))
			{
				int.TryParse(Convert.ToString(dr["Months"]), out months );
			}
			if (dt.Columns.Contains("Years"))
			{
				int.TryParse(Convert.ToString(dr["Years"]), out years );
			}
		}

		/// <summary>
		/// Saves a record to the tbl_Token_Sheet table.
		/// </summary>
		public void Save()
		{
			if(tokenId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the Token_Sheet table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_SheetInsert"))
			{
				db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				db.AddInParameter(dbCommand, "AccountNo", DbType.Int32, accountNo);
				db.AddInParameter(dbCommand, "ElectricityUnit", DbType.Int32, electricityUnit);
				db.AddInParameter(dbCommand, "TokenNo", DbType.String, tokenNo);
				db.AddInParameter(dbCommand, "Months", DbType.Int32, months);
				db.AddInParameter(dbCommand, "Years", DbType.Int32, years);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				tokenId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the Token_Sheet table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_SheetUpdate"))
			{
				db.AddInParameter(dbCommand, "TokenId", DbType.Int32, tokenId);
				db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				db.AddInParameter(dbCommand, "AccountNo", DbType.Int32, accountNo);
				db.AddInParameter(dbCommand, "ElectricityUnit", DbType.Int32, electricityUnit);
				db.AddInParameter(dbCommand, "TokenNo", DbType.String, tokenNo);
				db.AddInParameter(dbCommand, "Months", DbType.Int32, months);
				db.AddInParameter(dbCommand, "Years", DbType.Int32, years);

				db.ExecuteNonQuery(dbCommand);
			}
			db = null;
		}

		/// <summary>
		/// Deletes a record from the Token_Sheet table by a composite primary key.
		/// </summary>
		/// <param name="tokenId"></param>
		/// <returns>String</returns>
		public static string Delete(int tokenId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_SheetDelete"))
			{
				db.AddInParameter(dbCommand, "TokenId", DbType.Int32, tokenId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "Token_Sheet is already in use. You can not delete this Token_Sheet.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the Token_Sheet table.
		/// </summary>
		/// <param name="tokenId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int tokenId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_SheetSelect"))
			{
				db.AddInParameter(dbCommand, "TokenId", DbType.Int32, tokenId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the Token_Sheet table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static Token_SheetList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_SheetSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the Token_Sheet table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public Token_SheetList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_Token_SheetSearch"))
			{
				if(tokenId> 0) 
					db.AddInParameter(dbCommand, "TokenId", DbType.Int32, tokenId);
				if(houseId> 0) 
					db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);
				if(accountNo> 0) 
					db.AddInParameter(dbCommand, "AccountNo", DbType.Int32, accountNo);
				if(electricityUnit> 0) 
					db.AddInParameter(dbCommand, "ElectricityUnit", DbType.Int32, electricityUnit);
				if(tokenNo != string.Empty) 
					db.AddInParameter(dbCommand, "TokenNo", DbType.String, tokenNo);
				if(months> 0) 
					db.AddInParameter(dbCommand, "Months", DbType.Int32, months);
				if(years> 0) 
					db.AddInParameter(dbCommand, "Years", DbType.Int32, years);

				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		#endregion
	}
}
