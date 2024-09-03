using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace DataAccess
{
    public class clsReport
    {
        #region Methods
        public static DataSet GetPayrollSubmission(int PeriodId, int EmpType)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("rptPayrollSubmission");
                db.AddInParameter(dbCommand, "@PeriodId", DbType.Int32, PeriodId);
                db.AddInParameter(dbCommand, "@EmployeeType", DbType.Int32, EmpType);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds;
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

        public static DataTable GetMainFrontChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_mainFrontChart");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable GetWaterElecOusideChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_WaterElecOutsideChart");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable ElecConsumptionChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_ElecConsumptionChart");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable ElectricUnitPriceChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_ElectricUnitPriceChart");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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


        public static DataTable WaterChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_WaterCharges");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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


        public static DataTable ElectricityChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_ElectricCharges");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable OutsideChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_OutsideCharges");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable ElectricBasicChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_BasicElectric");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable WaterBasicChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_BasicWater");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable ElectricConsumptionChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_ConsumptionElectric");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable WaterConsumptionChargesChart(int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_ConsumptionWater");
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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

        public static DataTable ExcessWaterUsage(int EmployeeId, int year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("chart_ExcessWaterUsage");
                db.AddInParameter(dbCommand, "@EmployeeId", DbType.Int32, EmployeeId);
                db.AddInParameter(dbCommand, "@year", DbType.Int32, year);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds.Tables[0];
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
