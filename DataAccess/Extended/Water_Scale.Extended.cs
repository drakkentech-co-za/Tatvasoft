using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Water_Scale table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class Water_ScaleList : List<Water_Scale>
    { }

    /// <summary>
    /// Data access class for Water_Scale table.
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>29-Aug-2013</CreatedDate>
    public partial class Water_Scale
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Water_Scale as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Water_ScaleList SelectList(DataSet ds)
        {
            Water_ScaleList lstWater_Scale = new Water_ScaleList();
            Water_Scale objWater_Scale = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objWater_Scale = new Water_Scale();
                    objWater_Scale.MakeObject(dr);
                    lstWater_Scale.Add(objWater_Scale);
                }
            }
            return lstWater_Scale;
        }
        #endregion

        /// <summary>
        /// returns data for grid in waterscale page
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
                dbCommand = db.GetStoredProcCommand("WaterScale_GetGridData");
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
        /// returns last record of water scale
        /// </summary>
        /// <returns></returns>
        public static DataSet GetLastWaterScale(int periodId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("WaterScale_GetLastRecord");
                db.AddInParameter(dbCommand, "PeriodId", DbType.Int32, periodId);
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
        /// Delete water scale and adjust other water scale limit
        /// </summary>
        /// <returns></returns>
        public static string DeleteWaterScale(int waterScaleId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            using (DbCommand dbCommand = db.GetStoredProcCommand("WaterScale_Delete"))
            {
                db.AddInParameter(dbCommand, "WaterScaleId", DbType.Int32, waterScaleId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Water_Scale is already in use. You can not delete this Water_Scale.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }
    }
}
