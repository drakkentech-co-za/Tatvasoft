using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Landfill table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>21-Oct-2013</CreatedDate>
	public partial class LandfillList : List<Landfill>
	{}

	/// <summary>
	/// Data access class for Landfill table.
	/// </summary>
	/// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>21-Oct-2013</CreatedDate>
	public partial class Landfill
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Landfill as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static LandfillList SelectList(DataSet ds)
		{
			LandfillList lstLandfill = new LandfillList();
			Landfill objLandfill = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objLandfill = new Landfill();
					objLandfill.MakeObject(dr);
					lstLandfill.Add(objLandfill);
				}
			}
			return lstLandfill;
		}

        /// <summary>
        /// returns Landfills
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataSet GetLandfill()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("USP_GetLandfill");                
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
