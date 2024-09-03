using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Teriff_Rates table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>30-Aug-2013</CreatedDate>
	public partial class Teriff_RateList : List<Teriff_Rate>
	{}

	/// <summary>
	/// Data access class for Teriff_Rates table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>30-Aug-2013</CreatedDate>
	public partial class Teriff_Rate
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Teriff_Rates as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static Teriff_RateList SelectList(DataSet ds)
		{
			Teriff_RateList lstTeriff_Rates = new Teriff_RateList();
			Teriff_Rate objTeriff_Rates = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objTeriff_Rates = new Teriff_Rate();
					objTeriff_Rates.MakeObject(dr);
					lstTeriff_Rates.Add(objTeriff_Rates);
				}
			}
			return lstTeriff_Rates;
		}
		#endregion

        /// <summary>
        /// returns data for grid in teriff rate page
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
                dbCommand = db.GetStoredProcCommand("TeriffRate_GetGridData");
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
        public static bool  CheckTimeConflict(Teriff_Rate objTeriffRate)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("TeriffRate_CheckTimeConflict");
                db.AddInParameter(dbCommand, "TeriffId", DbType.Int32, objTeriffRate.TeriffId);
                db.AddInParameter(dbCommand, "DateFrom", DbType.DateTime, ConvertTo.SetDateDBNull(objTeriffRate.DateFrom));
                db.AddInParameter(dbCommand, "DateTo", DbType.DateTime, ConvertTo.SetDateDBNull(objTeriffRate.DateTo));
                db.AddInParameter(dbCommand, "TeriffRateId", DbType.Int32, objTeriffRate.TeriffRateId);
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
	}
}
