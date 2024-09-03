using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// Used to define some common functions which are used in whole application
    /// <DesignedBy>Kaushik Patel : 20 June 2013</DesignedBy>
    /// <CodeBy>Kaushik Patel</CodeBy>
    /// </summary>
    public class General
    {
        #region Variable/Property Declaration
        public static string ApplicationPath { get; set; }

        public static string ConnectionString { get; set; }
        #endregion

        #region Methods/Functions

        /// <summary>
        /// Get Connection String For Excel CSV
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileFormat"></param>
        /// <returns></returns>
        public static string GetConnectionStringForExcelCSV(string fileName, string fileType)
        {
            string conString = string.Empty;

            if (fileType.ToLower() == SystemEnum.FileType.XLS.ToString().ToLower())
                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;";
            else if (fileType.ToLower() == SystemEnum.FileType.XLSX.ToString().ToLower())
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=Excel 12.0 Xml;";
            else if (fileType.ToLower() == SystemEnum.FileType.CSV.ToString().ToLower())
                conString = "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + fileName + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited(,)\"";

            return conString;
        }

        /// <summary>
        /// Return Excel DataBase
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static DataSet GetExcelDataSet(string fileName)
        {
            // Create connection string variable. Modify the "Data Source"
            // parameter as appropriate for your environment.
            String sConnectionString = GetConnectionStringForExcelCSV(fileName, fileName.Substring(fileName.LastIndexOf('.') + 1));//"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFileName + ";Extended Properties=Excel 12.0";
            //String sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName + ";Extended Properties=Excel 8.0;";

            // Create connection object by using the preceding connection string.
            OleDbConnection objConn = new OleDbConnection(sConnectionString);

            DataSet objDataset1 = new DataSet();

            try
            {
                // Open connection with the database.
                objConn.Open();

                // The code to follow uses a SQL SELECT command to display the data from the worksheet.
                DataTable dtSheets = objConn.GetSchema("Tables");
                string sheetName = string.Empty;
                if (dtSheets.Rows.Count > 0)
                    sheetName = string.IsNullOrEmpty(ConvertTo.String(dtSheets.Rows[0]["TABLE_NAME"])) ? "[Sheet1$]" : "[" + ConvertTo.String(dtSheets.Rows[0]["TABLE_NAME"]) + "]";
                else
                    sheetName = "[Sheet1$]";

                // Create new OleDbCommand to return data from worksheet.
                OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM " + sheetName, objConn);

                // Create new OleDbDataAdapter that is used to build a DataSet
                // based on the preceding SQL SELECT statement.
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter();

                // Pass the Select command to the adapter.
                objAdapter1.SelectCommand = objCmdSelect;

                // Create new DataSet to hold information from the worksheet.


                // Fill the DataSet with the information from the worksheet.

                objAdapter1.Fill(objDataset1, "XLData");
            }
            catch (Exception ex)
            {
                //lblErrorMsg.Text = ShowMessage(Resources.GlobalMessages.Client_ImportExcel_FileUploadException + ex.Message, MessageBoxType.Error);
                throw ex;// ShowMessage("" + ex.Message, lblErrorMsg, MessageBoxType.Error);
            }
            finally
            {
                // Clean up objects.
                objConn.Close();
            }

            return objDataset1;
        }

        /// <summary>
        /// Return text file or csv file data as datatable
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static DataTable ReturnTxtCsvData(string fileName, bool isHeader)
        {
            DataTable dt = new DataTable();

            if (File.Exists(fileName))
            {
                FileInfo fileInfo = new FileInfo(fileName);

                string symbol =string.Empty ;

                if (fileInfo.Extension  == ".csv")
                    symbol = ",";
                else
                    symbol = "\t";

                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(fileName);
                    dt = GetDataTableFromStreamReader(sr, symbol, isHeader);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    sr.Close();
                }
            }           

            return dt;
        }

        /// <summary>
        /// Return datatable from stream reader
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromStreamReader(StreamReader sr,string symbol, bool isHeader)
        {
            DataTable dt = new DataTable();

            if (sr != null)
            {
                string line;
                int lineNumber = 0;
                string[] symbols = new string[1];
                symbols[0] = symbol;
                try
                {                   
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] array = line.Trim().Split(symbols, StringSplitOptions.None);

                        if (lineNumber == 0)
                        {
                            if (isHeader)
                            {
                                for (int i = 0; i < array.Length; i++)
                                {
                                    if (array[i].ToString().Trim().Contains("."))
                                    {
                                        array[i] = array[i].ToString().Replace(".", "#");
                                    }

                                    dt.Columns.Add(array[i].ToString().Trim().TrimEnd(';'));
                                }
                            }
                            else
                            {
                                for (int i = 0; i < array.Length; i++)
                                {
                                    dt.Columns.Add("Column" + i);
                                }
                            }
                        }

                        if (!isHeader || lineNumber > 0)
                        {
                            DataRow dr = dt.NewRow();
                            if (array.Length == dt.Columns.Count)
                            {
                                for (int i = 0; i < array.Length; i++)
                                {
                                    dr[i] = array[i];
                                }
                                dt.Rows.Add(dr);
                            }
                        }

                        lineNumber++;
                    }
                }
                catch (Exception ex)
                {

                }

                finally
                {
                    sr.Close();
                }
            }
            else
            {
            }

            return dt;
        }


        /// <summary>
        /// Check Duplicate records in table for selected columns
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <param name="primaryColumn"></param>
        /// <param name="primaryValue"></param>
        public static bool CheckDuplicateRecords(string tableName, string columnName, string columnValue, string primaryColumn, string primaryValue, string columnName2 = "", string column2Value = "")
        {
            Microsoft.Practices.EnterpriseLibrary.Data.Database db = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase();
            using (System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("UspGeneralCheckDuplicate"))
            {
                db.AddInParameter(dbCommand, "@tableName", DbType.String, tableName);
                db.AddInParameter(dbCommand, "@columnName", DbType.String, columnName);
                db.AddInParameter(dbCommand, "@columnNameValue", DbType.String, columnValue);
                db.AddInParameter(dbCommand, "@columnName2", DbType.String, columnName2);
                db.AddInParameter(dbCommand, "@columnName2Value", DbType.String, column2Value);
                db.AddInParameter(dbCommand, "@primaryKey", DbType.String, primaryColumn);
                db.AddInParameter(dbCommand, "@primaryKeyValue", DbType.String, primaryValue);

                return (db.ExecuteDataSet(dbCommand).Tables[0].Rows.Count > 0);
            }
            db = null;
        }

        public static DataTable getPeriodMonthYear()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("GETPeriodIds");                
                return db.ExecuteDataSet(dbCommand).Tables[0];
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

        public static DataTable getPeriodIds()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("BindPeriods");                
                return db.ExecuteDataSet(dbCommand).Tables[0];
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

        public static DataTable getPeriodMonthYearForLandfill()
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("GETPeriodIdForLandfill");
                return db.ExecuteDataSet(dbCommand).Tables[0];
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

        public static decimal GetWaterMonthlyLimit(int EmployeeId, int Year)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("GetEmployeeWaterUsageLimit");
                db.AddInParameter(dbCommand, "@EmployeeId", DbType.Int32, EmployeeId);
                db.AddInParameter(dbCommand, "@Year", DbType.Int32, Year);
                return ConvertTo.Decimal(db.ExecuteScalar(dbCommand));
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
