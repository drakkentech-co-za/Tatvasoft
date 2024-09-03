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
	/// <CreatedDate>13-Sep-2013</CreatedDate>
	public partial class WaterRate_LimitList : List<WaterRate_Limit>
	{}

	/// <summary>
	/// Data access class for WaterRate_Limit table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>13-Sep-2013</CreatedDate>
	public partial class WaterRate_Limit
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of WaterRate_Limit as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static WaterRate_LimitList SelectList(DataSet ds)
		{
			WaterRate_LimitList lstWaterRate_Limit = new WaterRate_LimitList();
			WaterRate_Limit objWaterRate_Limit = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objWaterRate_Limit = new WaterRate_Limit();
					objWaterRate_Limit.MakeObject(dr);
					lstWaterRate_Limit.Add(objWaterRate_Limit);
				}
			}
			return lstWaterRate_Limit;
		}

        /// <summary>
        /// returns data for grid in waterrate page
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetGridData(string searchText)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("WaterRateLimit_GetGridData");
                db.AddInParameter(dbCommand, "searchText", DbType.String, searchText);

                return db.ExecuteDataSet(dbCommand);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                    dbCommand = null;
                }
                if (db != null)
                    db = null;
            }
        }

        /// <summary>
        /// returns time conflict
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static bool CheckTimeConflict(WaterRate_Limit objTeriffRate)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("WaterLimitRate_CheckTimeConflict");
                db.AddInParameter(dbCommand, "@HouseTypeId", DbType.Int32, objTeriffRate.HouseTypeId);
                db.AddInParameter(dbCommand, "@DateFrom", DbType.DateTime, ConvertTo.SetDateDBNull(objTeriffRate.StartDate));
                db.AddInParameter(dbCommand, "@DateTo", DbType.DateTime, ConvertTo.SetDateDBNull(objTeriffRate.EndDate));
                db.AddInParameter(dbCommand, "@WaterRateLimitId", DbType.Int32, objTeriffRate.WaterRateLimitId);
                int duplicateRecord = ConvertTo.Integer(db.ExecuteScalar(dbCommand));

                if (duplicateRecord > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbCommand != null)
                {
                    dbCommand.Dispose();
                    dbCommand = null;
                }
                if (db != null)
                    db = null;
            }
        }

		#endregion
	}
}
