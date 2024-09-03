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
	public partial class HouseTypeList : List<HouseType>
	{}

	/// <summary>
	/// Data access class for HouseType table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>29-Aug-2013</CreatedDate>
	public partial class HouseType
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of HouseType as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static HouseTypeList SelectList(DataSet ds)
		{
			HouseTypeList lstHouseType = new HouseTypeList();
			HouseType objHouseType = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objHouseType = new HouseType();
					objHouseType.MakeObject(dr);
					lstHouseType.Add(objHouseType);
				}
			}
			return lstHouseType;
		}
		#endregion

        /// <summary>
        /// returns data for grid in housetype page
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
                dbCommand = db.GetStoredProcCommand("HouseType_GetGridData");
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
	}
}
