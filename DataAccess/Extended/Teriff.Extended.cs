using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Teriff table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>30-Aug-2013</CreatedDate>
	public partial class TeriffList : List<Teriff>
	{}

	/// <summary>
	/// Data access class for Teriff table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>30-Aug-2013</CreatedDate>
	public partial class Teriff
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Teriff as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static TeriffList SelectList(DataSet ds)
		{
			TeriffList lstTeriff = new TeriffList();
			Teriff objTeriff = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objTeriff = new Teriff();
					objTeriff.MakeObject(dr);
					lstTeriff.Add(objTeriff);
				}
			}
			return lstTeriff;
		}

        /// <summary>
        /// returns data for grid in teriff page
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
                dbCommand = db.GetStoredProcCommand("Teriff_GetGridData");
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
       
        #endregion
	}
}
