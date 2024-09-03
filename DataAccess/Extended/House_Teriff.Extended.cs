using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for House_Teriff table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>02-Sep-2013</CreatedDate>
	public partial class House_TeriffList : List<House_Teriff>
	{}

	/// <summary>
	/// Data access class for House_Teriff table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>02-Sep-2013</CreatedDate>
	public partial class House_Teriff
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of House_Teriff as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static House_TeriffList SelectList(DataSet ds)
		{
			House_TeriffList lstHouse_Teriff = new House_TeriffList();
			House_Teriff objHouse_Teriff = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objHouse_Teriff = new House_Teriff();
					objHouse_Teriff.MakeObject(dr);
					lstHouse_Teriff.Add(objHouse_Teriff);
				}
			}
			return lstHouse_Teriff;
		}

        public static DataTable BindHouseTariffMapping(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            DbCommand dbCommand = db.GetStoredProcCommand("BindHouseTariffMapping");
            db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }


        public static DataTable BindBasicHouseTariffMapping(int houseId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            DbCommand dbCommand = db.GetStoredProcCommand("BindBasicHouseTariffMapping");
            db.AddInParameter(dbCommand, "HouseId", DbType.Int32, houseId);

            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        public static void updateMapping(int HouseId, int userId, string teriffIds, string additionalTariffs)
        {

            Database db = DatabaseFactory.CreateDatabase();

            using (DbCommand dbCommand = db.GetStoredProcCommand("UpdateHouseTariffMapping"))
            {
                db.AddInParameter(dbCommand, "@HouseId", DbType.Int32, HouseId);
                db.AddInParameter(dbCommand, "@UserId", DbType.Int32, userId);
                db.AddInParameter(dbCommand, "@TarriffIds", DbType.String, teriffIds);
                db.AddInParameter(dbCommand, "@AdditionalTariff", DbType.String, additionalTariffs);
                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }
		#endregion
	}
}
