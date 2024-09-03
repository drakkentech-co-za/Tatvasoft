using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Token_Period table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>18-Sep-2013</CreatedDate>
	public partial class Token_PeriodList : List<Token_Period>
	{}

	/// <summary>
	/// Data access class for Token_Period table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>18-Sep-2013</CreatedDate>
	public partial class Token_Period
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Token_Period as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static Token_PeriodList SelectList(DataSet ds)
		{
			Token_PeriodList lstToken_Period = new Token_PeriodList();
			Token_Period objToken_Period = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objToken_Period = new Token_Period();
					objToken_Period.MakeObject(dr);
					lstToken_Period.Add(objToken_Period);
				}
			}
			return lstToken_Period;
		}

        /// <summary>
        /// Insert Token Period
        /// </summary>
        /// <param name="objTokenPeriod"></param>
        public static void InsertTokenPeriod(Token_Period objTokenPeriod)
		{
			Database db = DatabaseFactory.CreateDatabase();
			using (DbCommand dbCommand = db.GetStoredProcCommand("TokenPeriod_InsertTokenPeriod"))
			{
                db.AddInParameter(dbCommand, "StartDay", DbType.Int32, objTokenPeriod.StartDay);
                db.AddInParameter(dbCommand, "EndDay", DbType.Int32, objTokenPeriod.EndDay);
                if (objTokenPeriod.Created_TS != DateTime.MinValue)
                    db.AddInParameter(dbCommand, "Created_TS", DbType.DateTime, objTokenPeriod.Created_TS);
                db.AddInParameter(dbCommand, "Created_UserId", DbType.Int32, objTokenPeriod.Created_UserId);

				int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                objTokenPeriod.PeriodId  = returnValue;
			}
			db = null;
		}

        /// <summary>
        /// returns current token period
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public static DataTable  GetCurrentTokenPeriod()
        {
            DataTable dt = null;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("TokenPeriod_GetCurrentTokenPeriod");

                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
                
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

            return dt;
        }

		#endregion
	}
}
