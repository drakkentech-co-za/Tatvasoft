using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for AssetType table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>11-Sep-2013</CreatedDate>
    public partial class AssetTypeList : List<AssetType>
    { }

    /// <summary>
    /// Data access class for AssetType table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>11-Sep-2013</CreatedDate>
    public partial class AssetType
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of AssetType as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static AssetTypeList SelectList(DataSet ds)
        {
            AssetTypeList lstAssetType = new AssetTypeList();
            AssetType objAssetType = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objAssetType = new AssetType();
                    objAssetType.MakeObject(dr);
                    lstAssetType.Add(objAssetType);
                }
            }
            return lstAssetType;
        }

        /// <summary>
        /// returns data for grid in assettype page
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
                dbCommand = db.GetStoredProcCommand("AssetType_GetGridData");
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
