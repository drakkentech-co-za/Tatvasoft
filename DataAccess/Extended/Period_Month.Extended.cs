using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Period_Month table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>19-Sep-2013</CreatedDate>
	public partial class Period_MonthList : List<Period_Month>
	{}

	/// <summary>
	/// Data access class for Period_Month table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>19-Sep-2013</CreatedDate>
	public partial class Period_Month
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Period_Month as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static Period_MonthList SelectList(DataSet ds)
		{
			Period_MonthList lstPeriod_Month = new Period_MonthList();
			Period_Month objPeriod_Month = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objPeriod_Month = new Period_Month();
					objPeriod_Month.MakeObject(dr);
					lstPeriod_Month.Add(objPeriod_Month);
				}
			}
			return lstPeriod_Month;
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
                dbCommand = db.GetStoredProcCommand("PeriodMonth_GetGridData");
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
        public static bool CheckConflict(Period_Month objPeriodMonth)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("PeriodMonth_CheckConflict");
                db.AddInParameter(dbCommand, "@PeriodMonthid", DbType.Int32, objPeriodMonth.periodMonthId);
                db.AddInParameter(dbCommand, "@Month", DbType.Int32, objPeriodMonth.Month);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, objPeriodMonth.Year);
                
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
