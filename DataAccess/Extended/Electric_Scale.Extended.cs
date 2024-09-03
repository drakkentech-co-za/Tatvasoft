using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Electric_Scale table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>29-Aug-2013</CreatedDate>
	public partial class Electric_ScaleList : List<Electric_Scale>
	{}

	/// <summary>
	/// Data access class for Electric_Scale table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>29-Aug-2013</CreatedDate>
	public partial class Electric_Scale
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Electric_Scale as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static Electric_ScaleList SelectList(DataSet ds)
		{
			Electric_ScaleList lstElectric_Scale = new Electric_ScaleList();
			Electric_Scale objElectric_Scale = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objElectric_Scale = new Electric_Scale();
					objElectric_Scale.MakeObject(dr);
					lstElectric_Scale.Add(objElectric_Scale);
				}
			}
			return lstElectric_Scale;
		}
		#endregion

        /// <summary>
        /// returns data for grid in electricscale page
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
                dbCommand = db.GetStoredProcCommand("ElectricScale_GetGridData");
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
        /// returns last record of electric scale
        /// </summary>
        /// <returns></returns>
        public static DataSet GetLastElectricScale()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("ElectricScale_GetLastRecord");
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
