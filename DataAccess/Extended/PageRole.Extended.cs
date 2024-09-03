using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
    /// <summary>
    /// Data access class for PageRole table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>26-Sep-2013</CreatedDate>
    public partial class PageRoleList : List<PageRole>
    { }

    /// <summary>
    /// Data access class for PageRole table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>26-Sep-2013</CreatedDate>
    public partial class PageRole
    {
        #region "Enum"

        /// <summary>
        /// Enum oll Pages of system
        /// </summary>
        public enum SystemPages
        {
            AssetTypes = 1,
            AssetTypesDetail = 2,
            ElectricUnitPrices = 5,
            ElectricUnitPricesDetail = 6,
            EmployeeAccountMapping = 7,
            EmployeeEdit = 8,
            EmployeeList = 9,
            EmployeesReport = 10,
            GenerateInvoice = 11,
            HouseEdit = 12,
            HouseList = 13,
            HouseTariffMapping = 14,
            HouseTypes = 15,
            HouseTypesDetail = 16,
            InvoiceCompare = 17,
            InvoiceCompareDetail = 18,
            PeriodMonthMappingEdit = 22,
            PeriodMonthMappingList = 23,
            PopUpBasicHouseTariffMapping = 24,
            PopupDrillDownChart = 25,
            TariffRate = 26,
            TariffRateDetail = 27,
            Tariffs = 28,
            TariffsDetail = 29,
            TokenRequest = 30,
            TokenSheetManagement = 31,
            WaterRateLimits = 33,
            WaterRateLimitsDetail = 34,
            WaterScales = 35,
            WaterScalesDetail = 36,
            WECharts = 37,
            WESubCharts = 38,
            DynamicReports = 39,
            PayrollSubmissionReport = 40,
            WaterExcessUsageReport = 41,
            ChangePassword = 42,
            ResetPassword = 43,
            ManageRights = 44,
            LandfillList = 45,
            LandfillEdit = 46,
            PrepaidElecAndOther = 47,
            ManualTokenImport = 48,
            AdhocTokenDetail=49
        }


        #endregion

        #region Methods/Functions

        /// <summary>
        /// Give the List object of PageRole as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static PageRoleList SelectList(DataSet ds)
        {
            PageRoleList lstPageRole = new PageRoleList();
            PageRole objPageRole = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objPageRole = new PageRole();
                    objPageRole.MakeObject(dr);
                    lstPageRole.Add(objPageRole);
                }
            }
            return lstPageRole;
        }

        /// <summary>
        /// Set pagemaster rights
        /// </summary>
        /// <param name="selectedRights"></param>
        /// <param name="roleId"></param>
        public static void SetRights(string selectedRights, int roleId)
        {
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("PageRole_SetRights");
                db.AddInParameter(dbCommand, "SelectedRights", DbType.String, selectedRights);
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                db.ExecuteNonQuery(dbCommand);
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
