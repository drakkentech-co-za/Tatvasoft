using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using System.Web.Security;
using System.Security.Principal;
using DataAccess;

/// <summary>
/// Base Page
/// </summary>
/// <CreatedBy>Darpan Khandhar</CreatedBy>
/// <CreatedDate> 25-Jun-2013 </CreatedDate>
/// <ModifiedBy>Darpan Khandhar</ModifiedBy>
/// <ModifiedDate> 26-Jun-2013 </ModifiedDate>
public class BasePage : System.Web.UI.Page
{
    #region Enum

    public enum MessageBoxType
    {
        Success,
        Error,
        Warning,
        Information
    };

    protected enum ActionType
    {
        Save,
        Update,
        Delete
    };

    protected enum Float
    {
        Left,
        Right
    };
    #endregion

    #region Property Declaration

    protected string SortBy
    {
        get { return Convert.ToString(ViewState["SortBy"]); }
        set { ViewState["SortBy"] = value; }
    }

    protected bool IsAllowInsert
    {
        get
        {
            if (ViewState["IsAllowInsert"] == null)
                return false;
            else
                return DataAccess.ConvertTo.Boolean(ViewState["IsAllowInsert"]);
        }
        set { ViewState["IsAllowInsert"] = value; }
    }

    protected bool IsAllowEdit
    {
        get
        {
            if (ViewState["IsAllowEdit"] == null)
                return false;
            else
                return DataAccess.ConvertTo.Boolean(ViewState["IsAllowEdit"]);
        }
        set { ViewState["IsAllowEdit"] = value; }
    }

    protected bool IsAllowDelete
    {
        get
        {
            if (ViewState["IsAllowDelete"] == null)
                return false;
            else
                return DataAccess.ConvertTo.Boolean(ViewState["IsAllowDelete"]);
        }
        set { ViewState["IsAllowDelete"] = value; }
    }

    protected bool RedirectonSamePage
    {
        get
        {
            if (ViewState["RedirectonSamePage"] == null)
                return false;
            else
                return DataAccess.ConvertTo.Boolean(ViewState["RedirectonSamePage"]);
        }
        set { ViewState["RedirectonSamePage"] = value; }
    }

    protected string OrderBy
    {
        get
        {
            if (Convert.ToString(ViewState["OrderBy"]) == "")
            {
                ViewState["OrderBy"] = DataAccess.SystemEnum.SortDirection.asc.ToString();
            }                
            return Convert.ToString(ViewState["OrderBy"]);
        }
        set { ViewState["OrderBy"] = value.ToString(); }
    }

    protected string SearchString
    {
        get
        {
            return "Search";
        }
    }

    protected string SaveMessage(string saveFor)
    {
        return "alert('" + saveFor + " saved successfully');";
    }
    #endregion

    #region Constructor

    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Methods

    /// <summary>
    ///Override InitializeCulture for Initialize the Current Culture
    /// </summary>
    protected override void InitializeCulture()
    {
        try
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ProjectSession.Culture.Substring(0, 2));
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ProjectSession.Culture.Substring(0, 2));
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = CultureInfo.GetCultureInfo(CultureInfo.GetCultureInfo(ProjectSession.Culture).LCID).DateTimeFormat;
        }
        catch
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ProjectConfiguration.Culture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ProjectConfiguration.Culture);
        }
        finally
        {
            base.InitializeCulture();
        }
    }

    /// <summary>
    /// Override the Page Loag Method
    /// </summary>
    /// <param name="e"></param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    /// <summary>
    /// Override PreInt for set current Theme
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    /// <summary>
    /// Show Message in label
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messageType"></param>
    /// <returns></returns>
    protected void ShowMessage(string message, Label messageLabel, MessageBoxType messageType, bool isMsgTypeAsPrefix = true)
    {
        messageLabel.Visible = true;
        messageLabel.Text = isMsgTypeAsPrefix ? "<b>" + messageType.ToString() + "</b>" + @"!  " + message : message;
        messageLabel.CssClass = messageType.ToString().ToLower();
    }

    /// <summary>
    /// Get Pain Query String Value 
    /// </summary>
    /// <param name="queryStringName"></param>
    /// <returns></returns>
    protected string GetPlainQueryString(string queryStringName)
    {
        string currentValue = string.Empty;
        if (Request.QueryString[queryStringName] != null)
            currentValue = Convert.ToString(Request.QueryString[queryStringName]);

        return currentValue;
    }

    /// <summary>
    /// Get Query String Value after description
    /// </summary>
    /// <param name="queryStringName"></param>
    /// <returns></returns>
    protected string GetQueryString(string queryStringName)
    {
        string currentValue = string.Empty;
        if (Request.QueryString[queryStringName] != null)
            currentValue = EncryptionDecryption.GetDecrypt(Convert.ToString(Request.QueryString[queryStringName]));

        return currentValue;
    }

    /// <summary>
    /// Set Query encrypted query string value
    /// </summary>
    /// <param name="queryStringName"></param>
    /// <param name="queryStringValue"></param>
    /// <returns></returns>
    protected string SetQueryString(string queryStringName, string queryStringValue)
    {
        return General.SetQueryString(queryStringName, queryStringValue);
    }

    /// <summary>
    /// To register javascript code behind.
    /// </summary>
    /// <param name="script"></param>
    protected void RunScript(string script)
    {
        System.Web.UI.Page executingPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        ScriptManager.RegisterStartupScript(executingPage, executingPage.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
    }

    /// <summary>
    /// Set Rights
    /// </summary>
    protected void SetRights(int systemPage)
    {
        if (!ProjectSession.IsAdmin )
        {
            System.Web.UI.Page executingPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

            if (ProjectSession.PageAccessRights != null)
            {
                if (!ProjectSession.PageAccessRights.Contains(systemPage))
                    ScriptManager.RegisterStartupScript(executingPage, executingPage.GetType(), Guid.NewGuid().ToString(), "window.location.href='Default.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(executingPage, executingPage.GetType(), Guid.NewGuid().ToString(), "window.location.href='Default.aspx';", true);
            }

        }
    }

    /// <summary>
    /// To register javascript code behind.
    /// </summary>
    /// <param name="script"></param>
    protected void RunScriptBlock(string script)
    {
        System.Web.UI.Page executingPage = (System.Web.UI.Page)System.Web.HttpContext.Current.Handler;

        ScriptManager.RegisterClientScriptBlock(executingPage, executingPage.GetType(), Guid.NewGuid().ToString(), script.ToString(), true);
    }

    /// <summary>
    /// Sort the Generic List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sort"></param>
    /// <param name="propertyName"></param>
    protected void SortGenericList<T>(ref List<T> list)
    {

        if (!string.IsNullOrEmpty(SortBy) && list != null && list.Count > 0)
        {
            Type t = list[0].GetType();

            if (OrderBy == DataAccess.SystemEnum.SortDirection.asc.ToString())
            {
                list = list.OrderBy(
                    a => t.InvokeMember(SortBy, System.Reflection.BindingFlags.GetProperty, null, a, null)).ToList();
            }
            else
            {
                list = list.OrderByDescending(
                    a => t.InvokeMember(SortBy, System.Reflection.BindingFlags.GetProperty, null, a, null)).ToList();
            }
        }
    }

    #endregion
}



public class BasePageHome : BasePage
{
    #region Constructor

    public BasePageHome()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Methods

    /// <summary>
    /// Override PreInt for set current Theme & Validate User
    /// </summary>
    /// <param name="e"></param>
    protected override void OnPreInit(EventArgs e)
    {
        if (string.IsNullOrEmpty(ProjectSession.UserName) || !HttpContext.Current.Request.IsAuthenticated)
        {
            Response.Redirect(URLHelper.GetPath("Login.aspx"));
        }
        base.OnPreInit(e);
    }

    #endregion
}
