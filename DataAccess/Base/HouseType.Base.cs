using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for HouseType table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>29-Aug-2013</CreatedDate>
	public partial class HouseType
	{
		#region Fields

		private int houseTypeId;
		private string houseTypeName;
		private DateTime created_TS;
		private int created_UserId;
		private DateTime updated_TS;
		private int updated_UserId;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HouseType class.
		/// </summary>
		public HouseType()
		{
				InitVariables();
		}

		/// <summary>
		/// Initializes a new instance of the HouseType class.
		/// </summary>
		public HouseType(DataSet ds)
		{
			MakeObject(ds);
		}

		/// <summary>
		/// Initializes a new instance of the HouseType class.
		/// </summary>
		public HouseType(int houseTypeId)
		{
			DataSet ds = Select(houseTypeId);
			MakeObject(ds);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the HouseTypeId value.
		/// </summary>
		public virtual int HouseTypeId
		{
			get { return houseTypeId; }
			set { houseTypeId = value; }
		}

		/// <summary>
		/// Gets or sets the HouseTypeName value.
		/// </summary>
		public virtual string HouseTypeName
		{
			get { return houseTypeName; }
			set { houseTypeName = value; }
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
			this.houseTypeName = string.Empty;
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
			if (dt.Columns.Contains("HouseTypeId"))
			{
				int.TryParse(Convert.ToString(dr["HouseTypeId"]), out houseTypeId );
			}
			if (dt.Columns.Contains("HouseTypeName"))
			{
				houseTypeName = Convert.ToString(dr["HouseTypeName"]);
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
		/// Saves a record to the tbl_HouseType table.
		/// </summary>
		public void Save()
		{
			if(houseTypeId > 0)
				Update();
			else
				Insert();
		}

		/// <summary>
		/// Inserts a record into the HouseType table.
		/// </summary>
		public void Insert()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseTypeInsert"))
			{
				db.AddInParameter(dbCommand, "HouseTypeName", DbType.String, houseTypeName);
				if(created_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, created_TS);
				db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, created_UserId);
				if(updated_TS != DateTime.MinValue) 
					db.AddInParameter(dbCommand, "Updated_TS", DbType.DateTime, updated_TS);
				db.AddInParameter(dbCommand, "Updated_UserId", DbType.Int32, updated_UserId);

				// Execute the query and return the new identity value
				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

				houseTypeId= returnValue;
			}
			db = null;
		}

		/// <summary>
		/// Updates a record in the HouseType table.
		/// </summary>
		public void Update()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseTypeUpdate"))
			{
				db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
				db.AddInParameter(dbCommand, "HouseTypeName", DbType.String, houseTypeName);
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
		/// Deletes a record from the HouseType table by a composite primary key.
		/// </summary>
		/// <param name="houseTypeId"></param>
		/// <returns>String</returns>
		public static string Delete(int houseTypeId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseTypeDelete"))
			{
				db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);

				try
				{
					db.ExecuteNonQuery(dbCommand);
				}
				catch (System.Data.SqlClient.SqlException sqlEx)
				{
					if (sqlEx.Number == 547)
						return "HouseType is already in use. You can not delete this HouseType.";
					else
						throw sqlEx;
				}
			}
			db = null;
			return string.Empty;
		}

		/// <summary>
		/// Selects a single record from the HouseType table.
		/// </summary>
		/// <param name="houseTypeId"></param>
		/// <returns>DataSet</returns>
		private static DataSet Select(int houseTypeId)
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseTypeSelect"))
			{
				db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);

				return db.ExecuteDataSet(dbCommand);
			}
		}

		/// <summary>
		/// Selects all records from the HouseType table.
		/// </summary>
		/// <returns>DataSet</returns>
		public static HouseTypeList SelectAll()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseTypeSelectAll"))
			{
				return SelectList(db.ExecuteDataSet(dbCommand));
			}
		}

		/// <summary>
		/// Search records from the HouseType table as per criteria
		/// </summary>
		/// <param name="searchCriteria"></param>
		/// <returns>DataSet</returns>
		public HouseTypeList Search()
		{
			// Create Database object
			Database db = DatabaseFactory.CreateDatabase();
			// Create a suitable command type and add the required parameter
			using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_HouseTypeSearch"))
			{
				if(houseTypeId> 0) 
					db.AddInParameter(dbCommand, "HouseTypeId", DbType.Int32, houseTypeId);
				if(houseTypeName != string.Empty) 
					db.AddInParameter(dbCommand, "HouseTypeName", DbType.String, houseTypeName);
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
