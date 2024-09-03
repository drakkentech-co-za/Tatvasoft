using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Web.UI;

/// <summary>
/// This class defines some general methods used throughout application
/// </summary>
/// <CreatedBy>Darpan Khandhar</CreatedBy>
/// <CreatedDate> 25-Jun-2013 </CreatedDate>
/// <ModifiedBy>Kaushik Patel</ModifiedBy>
/// <ModifiedDate> 2-Jul-2013 </ModifiedDate>
public class General
{
    #region Variable Declaration

    #endregion

    #region Constructor

    public General()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Methods/Functions

    /// <summary>
    /// Create a encrypted Query String Value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string SetQueryString(string key, string value)
    {
        string queryString = string.Empty;
        queryString = key + "=" + HttpContext.Current.Server.UrlEncode(EncryptionDecryption.GetEncrypt(value));
        return queryString;
    }

    /// <summary>
    /// Bind the GridView with DataTable
    /// </summary>
    /// <param name="gridView"></param>
    /// <param name="dataTable"></param>
    public static void GridBind(ref GridView gridView, System.Data.DataTable dataTable)
    {
        gridView.DataSource = dataTable;
        gridView.DataBind();
    }

    /// <summary>
    /// Bind the GridView with DataSet
    /// </summary>
    /// <param name="gridView"></param>
    /// <param name="dataTable"></param>
    public static void GridBind(ref GridView gridView, System.Data.DataSet dataSet)
    {
        gridView.DataSource = dataSet;
        gridView.DataBind();
    }

    /// <summary>
    /// Bind the GridView with ListTable object
    /// </summary>
    /// <param name="gridView"></param>
    /// <param name="dataTable"></param>
    public static void GridBind(ref GridView gridView, object dataObject)
    {
        gridView.DataSource = dataObject;
        gridView.DataBind();
    }

    /// <summary>
    /// Bind the DropDownList with ListTable object
    /// </summary>
    /// <param name="dropDown"></param>
    /// <param name="dataList"></param>
    /// <param name="SelectNeed"></param>
    /// <param name="ValueField"></param>
    /// <param name="DataField"></param>
    /// <param name="SelectValue"></param>
    public static void DropDownListBind(ref DropDownList dropDown, object dataList, bool SelectNeed, string ValueField, string DataField, string SelectData, string SelectValue = "0")
    {
        dropDown.DataSource = dataList;
        dropDown.DataTextField = DataField;
        dropDown.DataValueField = ValueField;
        dropDown.DataBind();
        if (SelectNeed)
            dropDown.Items.Insert(0, new ListItem(SelectData, SelectValue));
    }

    /// <summary>
    /// Bind the RadioButtonList
    /// </summary>
    /// <param name="radiobuttonlist"></param>
    /// <param name="dataList"></param>
    /// <param name="SelectNeed"></param>
    /// <param name="ValueField"></param>
    /// <param name="DataField"></param>
    /// <param name="SelectData"></param>
    /// <param name="SelectValue"></param>
    public static void RadioButtonListBind(ref RadioButtonList radiobuttonlist, object dataList, bool SelectNeed, string ValueField, string DataField, string SelectData, string SelectValue = "0")
    {
        radiobuttonlist.DataSource = dataList;
        radiobuttonlist.DataTextField = DataField;
        radiobuttonlist.DataValueField = ValueField;
        radiobuttonlist.DataBind();
        if (SelectNeed)
        {
            radiobuttonlist.Items.Insert(0, new ListItem(SelectData, SelectValue));
            radiobuttonlist.Items[0].Selected = true;
        }
    }

    /// <summary>
    /// Bind the checkboxlist
    /// </summary>
    /// <param name="checkboxlist"></param>
    /// <param name="dataList"></param>
    /// <param name="SelectNeed"></param>
    /// <param name="ValueField"></param>
    /// <param name="DataField"></param>
    /// <param name="SelectData"></param>
    /// <param name="SelectValue"></param>
    public static void CheckBoxListBind(ref CheckBoxList checkboxlist, object dataList, bool SelectNeed, string ValueField, string DataField, string SelectData, string SelectValue = "0")
    {
        checkboxlist.DataSource = dataList;
        checkboxlist.DataTextField = DataField;
        checkboxlist.DataValueField = ValueField;
        checkboxlist.DataBind();
        if (SelectNeed)
        {
            checkboxlist.Items.Insert(0, new ListItem(SelectData, SelectValue));
            checkboxlist.Items[0].Selected = true;
        }
    }

    #region "Bind General Dropdowns"

    ///// <summary>
    ///// bind role dropdown control
    ///// </summary>
    //public static void BindRole(ref DropDownList ddlRole)
    //{
    //    List<Role> lstTemp = Role.SelectAll();
    //    List<Role> lstRole = new List<Role>();

    //    foreach (Role role in lstTemp)
    //    {
    //        if (ProjectSession.UserRoleID == SystemEnum.UserRole.WebAdministrator.GetHashCode())
    //        {
    //            lstRole.Add(role);
    //        }

    //        else if (ProjectSession.UserRoleID == SystemEnum.UserRole.ChainAdmin.GetHashCode())
    //        {
    //            if (role.RoleName.ToLower() != SystemEnum.UserRole.WebAdministrator.ToString().ToLower())
    //            {
    //                lstRole.Add(role);
    //            }
    //        }

    //        else if (ProjectSession.UserRoleID == SystemEnum.UserRole.StoreUser.GetHashCode())
    //        {
    //            if (role.RoleName.ToLower() == SystemEnum.UserRole.StoreUser.ToString().ToLower())
    //            {
    //                lstRole.Add(role);
    //            }
    //        }
    //    }

    //    General.DropDownListBind(ref ddlRole, lstRole, true, "RoleId", "RoleName", HttpContext.GetGlobalResourceObject("WebResource", "PleaseSelect").ToString());
    //}

    /// <summary>
    /// Bind employee type from enum  in radiobuttonlist control
    /// </summary>
    /// <param name="ddlPeriod"></param>
    /// <param name="selectNeed"></param>
    public static void BindEmployeeType(ref RadioButtonList rdbEmployeeType, bool selectNeed = true)
    {
        rdbEmployeeType.Items.Add(new ListItem(Employee.EmployeeTypeName.Package.ToString(), Employee.EmployeeTypeName.Package.GetHashCode().ToString()));
        rdbEmployeeType.Items.Add(new ListItem(Employee.EmployeeTypeName.Bargaining.ToString(), Employee.EmployeeTypeName.Bargaining.GetHashCode().ToString()));
        rdbEmployeeType.Items.Add(new ListItem(Employee.EmployeeTypeName.Exco.ToString(), Employee.EmployeeTypeName.Exco.GetHashCode().ToString()));
    }

    public static void BindEmployeeType(ref DropDownList ddlEmployeeType, bool SelectNeed, string selectstring = "", string selectvalue = "0")
    {
        if (SelectNeed)
        {
            ddlEmployeeType.Items.Add(new ListItem(selectstring, selectvalue));
        }

        string[] enumNames = Enum.GetNames(typeof(Employee.EmployeeTypeName));
        foreach (string item in enumNames)
        {
            int value = (int)Enum.Parse(typeof(Employee.EmployeeTypeName), item);
            ddlEmployeeType.Items.Add(new ListItem(item, value.ToString()));
        }
    }

    /// <summary>
    /// Bind teriff from tbl_Teriff table in dropdown control
    /// </summary>
    /// <param name="ddlPeriod"></param>
    /// <param name="selectNeed"></param>
    public static void BindTeriff(ref DropDownList ddlTeriff, bool selectNeed = true)
    {
        TeriffList teriffList = Teriff.SelectAll();
        List<Teriff> lstTeriff = teriffList.OrderBy(x => x.CategoryName).ToList();
        General.DropDownListBind(ref ddlTeriff, lstTeriff, selectNeed, "TeriffId", "CategoryName", "--Please Select--");
    }

    /// <summary>
    /// Bind assettype from tbl_AssetType table in dropdown control 
    /// </summary>
    /// <param name="ddlAssetType"></param>
    /// <param name="selecNeed"></param>
    public static void BindAssetType(ref DropDownList ddlAssetType, bool selecNeed = true)
    {
        AssetTypeList assetTypeList = AssetType.SelectAll();
        List<AssetType> lstAssetType = assetTypeList.OrderBy(x => x.AssetTypeName).ToList();
        General.DropDownListBind(ref ddlAssetType, lstAssetType, selecNeed, "AssetTypeId", "AssetTypeName", "--Please Select--");
    }

    /// <summary>
    /// Bind EmployeeNumber from tbl_Employee table in dropdown control
    /// </summary>
    /// <param name="ddlEmployeeNumber"></param>
    /// <param name="selectNeed"></param>
    public static void BindEmployeeNumber(ref DropDownList ddlEmployeeNumber, bool selectNeed = true)
    {
        EmployeeList employeeList = Employee.SelectAll();
        List<Employee> lstEmployee = employeeList.OrderBy(x => x.EmployeeNo).ToList();
        General.DropDownListBind(ref ddlEmployeeNumber, employeeList, selectNeed, "EmployeeId", "EmployeeNo", "--Please Select--");
    }

    /// <summary>
    /// Bind AccountNumber from tbl_house table in dropdown control
    /// </summary>
    /// <param name="ddlAccount"></param>
    /// <param name="selectNeed"></param>
    public static void BindAccount(ref DropDownList ddlAccount, bool selectNeed = true, string selectString = "--Please Select--")
    {
        HouseList houseList = House.SelectAll();
        List<House> lstHouse = houseList.OrderBy(x => x.AccountNo).ToList();
        General.DropDownListBind(ref ddlAccount, houseList, selectNeed, "HouseId", "AccountNo", selectString);
    }

    public static void BindAccount(ref DropDownList ddlAccount,string employeeNo, bool selectNeed = true, string selectString = "--Please Select--")
    {
        DataTable dt = Employee_House.GetByEmployeeNumber(employeeNo);
        General.DropDownListBind(ref ddlAccount, dt, selectNeed, "HouseId", "AccountNo", selectString);
    }

    /// <summary>
    /// Bind Role from tbl_Role table in dropdown control
    /// </summary>
    /// <param name="ddlRole"></param>
    /// <param name="selectNeed"></param>
    /// <param name="selectString"></param>
    public static void BindRole(ref DropDownList ddlRole, bool selectNeed = true, string selectString = "--Please Select--")
    {
        General.DropDownListBind(ref ddlRole, ProjectSession.SystemRole, selectNeed, "RoleId", "RoleName", selectString);
    }

    /// <summary>
    /// Bind pages from tbl_PageMaster table in checkbox control
    /// </summary>
    /// <param name="chkPage"></param>
    /// <param name="selectNeed"></param>
    /// <param name="selectString"></param>
    public static void BindPages(ref CheckBoxList chkPage, bool selectNeed = true, string selectString = "--Please Select--")
    {
        foreach (int value in Enum.GetValues(typeof(PageRole.SystemPages)))
        {
            chkPage.Items.Add(new ListItem(Enum.GetName(typeof(PageRole.SystemPages), value), value.ToString()));
        }

        chkPage.DataBind();
    }

    /// <summary>
    /// Bind users from tbl_Users table in dropdown control
    /// </summary>
    /// <param name="ddlUsers"></param>
    /// <param name="selectNeed"></param>
    /// <param name="selectString"></param>
    public static void BindUsers(ref DropDownList ddlUsers, bool selectNeed = true, string selectString = "Please Select--")
    {
        General.DropDownListBind(ref ddlUsers, ProjectSession.SystemUsers, selectNeed, "UserId", "UserName", selectString);
    }

    /// <summary>
    /// Bind Gender dropdown control
    /// </summary>
    /// <param name="ddlGender"></param>
    public static void BindGender(ref DropDownList ddlGender)
    {
        ddlGender.Items.Add(new ListItem(HttpContext.GetGlobalResourceObject("WebResource", "PleaseSelect").ToString(), "-1"));
        ddlGender.Items.Add(new ListItem(HttpContext.GetGlobalResourceObject("WebResource", "Male").ToString(), "1"));
        ddlGender.Items.Add(new ListItem(HttpContext.GetGlobalResourceObject("WebResource", "Female").ToString(), "2"));
        ddlGender.DataBind();
    }

    /// <summary>
    ///Bind DroDown with All months
    /// </summary>
    /// <param name="dropDown">dropDownlist</param>
    public static void BindMonthDropDown(ref DropDownList dropDown)
    {
        string[] ShortMonths = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        dropDown.Items.Clear();

        for (int i = 1; i <= 12; i++)
        {
            dropDown.Items.Add(new ListItem(ShortMonths[i - 1], ConvertTo.String(i)));
        }
    }

    /// <summary>
    ///Bind DroDown with year
    /// </summary>
    /// <param name="dropDown">dropDownlist</param>
    public static void BindYearDropDown(ref DropDownList dropDown, int startYear, int endYear)
    {
        dropDown.Items.Clear();

        for (int i = startYear; i <= endYear; i++)
        {
            dropDown.Items.Add(new ListItem(ConvertTo.String(i), ConvertTo.String(i)));
        }
    }

    /// <summary>
    /// Bind role in checkbox control
    /// </summary>
    /// <param name="chkRole"></param>
    /// <param name="selectNeed"></param>
    /// <param name="selectString"></param>
    public static void BindRole(ref CheckBoxList chkRole, bool selectNeed = true, string selectString = "--Please Select--")
    {
        General.CheckBoxListBind(ref chkRole, ProjectSession.SystemRole, selectNeed, "RoleId", "RoleName", selectString);
    }

    #endregion

    /// <summary>
    /// Export CSV File 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="fileName"></param>
    /// <param name="includeHearder"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string ExportData(DataTable dt, string fileName, bool includeHearder)
    {
        //Each row of the file must end with semicolon as well, then carriage return and line feed.

        StringBuilder sb = default(StringBuilder);
        sb = new StringBuilder();
        string header = "";
        string del = null;

        del = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ListSeparator;

        if (includeHearder == true)
        {
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                header += dt.Columns[i].ColumnName.ToString() + del;
            }
            //  if (header.Length > 0)
            // {
            //    header = header.Substring(header.Length - 1, 1);
            //}

            sb.AppendLine(header);
        }

        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            header = "";
            for (int c = 0; c <= dt.Columns.Count - 1; c++)
            {
                header += "\"" + dt.Rows[i][c].ToString() + "\"" + del;
            }

            //if (header.Length > 0)
            //{
            //    header = header.Substring(header.Length - 1, 1);
            //}

            sb.AppendLine(header);
        }

        if (string.IsNullOrEmpty(fileName))
        {
            fileName = "SearchData";
        }

        CreateFile(sb.ToString(), fileName);
        return sb.ToString();

    }

    /// <summary>
    /// Create File (Exported CSV)
    /// </summary>
    /// <param name="content"></param>
    /// <param name="fileName"></param>
    /// <remarks></remarks>
    public static void CreateFile(string content, string fileName)
    {
        if (content.Length > 0)
        {
            string newFile = null;
            string path = null;

            newFile = fileName + "_" + ProjectSession.UserID.ToString() + ".csv";
            path = HttpContext.Current.Server.MapPath("~/StaticContent/Files");
            path = path + "\\" + newFile;

            if (content.ToString().LastIndexOf("\r\n") == content.Length - 2)
            {
                content = content.Remove(content.Length - 2);
            }

            StreamWriter sw = new StreamWriter(path);
            sw.Write(content.ToString());

            sw.Close();
            sw.Dispose();

            FileStream LiveFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] fileBuffer = new byte[Convert.ToInt32(LiveFileStream.Length) + 1];

            LiveFileStream.Read(fileBuffer, 0, Convert.ToInt32(LiveFileStream.Length));
            LiveFileStream.Close();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.AddHeader("Content-Length", fileBuffer.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename= " + fileName + ".csv");
            HttpContext.Current.Response.BinaryWrite(fileBuffer);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

    public static void BindCategoryType(ref DropDownList dropDown)
    {
        dropDown.DataSource = CategoryType();
        dropDown.DataValueField = "key";
        dropDown.DataTextField = "value";
        dropDown.DataBind();
    }


    public static Dictionary<int, string> CategoryType()
    {
        Dictionary<int, string> dcCategoryType = new Dictionary<int, string>();
        dcCategoryType.Add(1, "Water-Basic");
        dcCategoryType.Add(2, "Water-Consumption");
        dcCategoryType.Add(8, "Water Consumption Fixed"); // Newly added
        dcCategoryType.Add(3, "Electricity-Basic");
        dcCategoryType.Add(4, "Electricity-Consumption");
        dcCategoryType.Add(5, "Sewerage");
        dcCategoryType.Add(6, "Refuse");
        dcCategoryType.Add(7, "Other");        
        return dcCategoryType;
    }

    public static string FindCategoryType(int key)
    {
        Dictionary<int, string> dc = CategoryType();
        if (dc.ContainsKey(key))
        {
            return dc[key];
        }
        else
        {
            return "";
        }
    }

    public static void BindPeriodIds(ref DropDownList ddlPeriod)
    {
        DataTable dt = DataAccess.General.getPeriodIds();
        ddlPeriod.DataSource = dt;
        ddlPeriod.DataTextField = "Period";
        ddlPeriod.DataValueField = "Period";
        ddlPeriod.DataBind();
        //if (ddlPeriod.Items.Count > 0)
        //    ddlPeriod.SelectedValue = ddlPeriod.Items[ddlPeriod.Items.Count - 1].Value;
    }

    public static void BindPeriodMonthYear(ref DropDownList ddlPeriod, bool ExceptAdded = false)
    {
        DataTable dt = DataAccess.General.getPeriodMonthYear();
        ddlPeriod.DataSource = dt;
        if (ExceptAdded)
        {
            DataRow dr = dt.NewRow();
            dr["iPeriodID"] = 0;
            dr["Period"] = "All";

            dt.Rows.InsertAt(dr, 0);
        }
      
        ddlPeriod.DataTextField = "Period";
        ddlPeriod.DataValueField = "iPeriodID";
        ddlPeriod.DataBind();
        
        //if (ddlPeriod.Items.Count > 0)
        //    ddlPeriod.SelectedValue = ddlPeriod.Items[ddlPeriod.Items.Count - 1].Value;
    }

    public static void BindPeriodForLandfill(ref DropDownList ddlPeriod)
    {
        DataTable dt = DataAccess.General.getPeriodMonthYearForLandfill();
        ddlPeriod.DataSource = dt;
        ddlPeriod.DataTextField = "Period";
        ddlPeriod.DataValueField = "iPeriodID";
        ddlPeriod.DataBind();
        //if (ddlPeriod.Items.Count > 0)
        //    ddlPeriod.SelectedValue = ddlPeriod.Items[ddlPeriod.Items.Count - 1].Value;
    }


    #region "Date Function"

    /// <summary>
    /// Get Date 
    /// </summary>
    /// <param name="paramDate">String of datetime</param>
    /// <returns>Datetime</returns>
    public static DateTime GetDateWithOutTime(string paramDate)
    {
        DateTime tempDate;
        IFormatProvider viewCulture = CultureInfo.CreateSpecificCulture(ProjectSession.Culture);
        if (DateTime.TryParse(paramDate, viewCulture, DateTimeStyles.None, out tempDate) == true)
        {
            return tempDate;
        }
        else
            return DateTime.MinValue;
    }

    /// <summary>
    /// Get String for valid Datetime or else return empty
    /// </summary>
    /// <param name="paramDate">String of Datetime object</param>
    /// <returns>string in valid format</returns>
    public static string GetStringDate(string paramDate)
    {
        DateTime tempDate;
        string strDate = string.Empty;
        if (DateTime.TryParse(paramDate, out tempDate))
        {
            if (tempDate != DateTime.MinValue)
                strDate = tempDate.ToString("dd/MM/yyyy");
        }
        return strDate;
    }

    #endregion

    #endregion
}