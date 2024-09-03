using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for ElectricUnitPrice table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>16-Sep-2013</CreatedDate>
    public partial class ElectricUnitPriceList : List<ElectricUnitPrice>
    { }

    /// <summary>
    /// Data access class for ElectricUnitPrice table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>16-Sep-2013</CreatedDate>
    public partial class ElectricUnitPrice
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of ElectricUnitPrice as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static ElectricUnitPriceList SelectList(DataSet ds)
        {
            ElectricUnitPriceList lstElectricUnitPrice = new ElectricUnitPriceList();
            ElectricUnitPrice objElectricUnitPrice = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objElectricUnitPrice = new ElectricUnitPrice();
                    objElectricUnitPrice.MakeObject(dr);
                    lstElectricUnitPrice.Add(objElectricUnitPrice);
                }
            }
            return lstElectricUnitPrice;
        }

        /// <summary>
        /// returns data for grid in electricunitprice page
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
                dbCommand = db.GetStoredProcCommand("ElectricUnitPrice_GetGridData");
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
