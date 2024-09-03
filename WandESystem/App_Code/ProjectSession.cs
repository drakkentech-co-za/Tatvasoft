using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using System.Collections;
using System.Data;
using System.Configuration;

/// <summary>
/// This class is used to store different common session used in application
/// </summary>
/// <CreatedBy>Darpan Khandhar</CreatedBy>
/// <CreatedDate> 25-Jun-2013 </CreatedDate>
/// <ModifiedBy>Kaushik Patel</ModifiedBy>
/// <ModifiedDate> 02-Jul-2013 </ModifiedDate>
public class ProjectSession
{
    #region Constructor

    public ProjectSession()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Properties

    /// <summary>
    /// Return Current User ID
    /// </summary>
    public static int UserID
    {
        get
        {
            if (HttpContext.Current.Session["UserId"] == null)
            {
                return 0;
            }
            else
            {
                return ConvertTo.Integer(HttpContext.Current.Session["UserId"].ToString());
            }
        }
        set { HttpContext.Current.Session["UserId"] = value; }
    }

    /// <summary>
    /// Return Current User Name
    /// </summary>
    public static string UserName
    {
        get
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                return "";
            }
            else
            {
                return System.Convert.ToString(HttpContext.Current.Session["UserName"]);
            }
        }
        set { HttpContext.Current.Session["UserName"] = value; }
    }       

    /// <summary>
    /// Return Current User has admin role or not
    /// </summary>
    public static bool IsAdmin
    {
        get
        {
            if (HttpContext.Current.Session["IsAdmin"] == null)
            {
                return false;
            }
            else
            {
                return ConvertTo.Boolean(HttpContext.Current.Session["IsAdmin"]);
            }
        }
        set { HttpContext.Current.Session["IsAdmin"] = value; }
    }

    /// <summary>
    /// Return Current User has payroll role or not
    /// </summary>
    public static bool IsPayRoll
    {
        get
        {
            if (HttpContext.Current.Session["IsPayRoll"] == null)
            {
                return false;
            }
            else
            {
                return ConvertTo.Boolean(HttpContext.Current.Session["IsPayRoll"]);
            }
        }
        set { HttpContext.Current.Session["IsPayRoll"] = value; }
    }

    /// <summary>
    /// Return Current User has Employee role or not
    /// </summary>
    public static bool IsEmployee
    {
        get
        {
            if (HttpContext.Current.Session["IsEmployee"] == null)
            {
                return false;
            }
            else
            {
                return ConvertTo.Boolean(HttpContext.Current.Session["IsEmployee"]);
            }
        }
        set { HttpContext.Current.Session["IsEmployee"] = value; }
    }

    /// <summary>
    /// Return Current User has Employee role or not
    /// </summary>
    public static bool IsMaint
    {
        get
        {
            if (HttpContext.Current.Session["IsMaint"] == null)
            {
                return false;
            }
            else
            {
                return ConvertTo.Boolean(HttpContext.Current.Session["IsMaint"]);
            }
        }
        set { HttpContext.Current.Session["IsMaint"] = value; }
    }

    /// <summary>
    /// Return Current Culture
    /// </summary>
    public static string Culture
    {
        get
        {
            if (HttpContext.Current.Session["Culture"] == null)
            {
                return ProjectConfiguration.Culture;
            }
            else
            {
                return System.Convert.ToString(HttpContext.Current.Session["Culture"]);
            }
        }
        set { HttpContext.Current.Session["Culture"] = value; }
    }

    /// <summary>
    /// Return role list
    /// </summary>
    public static List<DataAccess.Role> SystemRole
    {
        get
        {
            if (HttpContext.Current.Session["SystemRole"] == null)
            {
                RoleList roleList = DataAccess.Role.SelectAll();
                List<DataAccess.Role> lstRole  = roleList.OrderBy(x => x.RoleName).ToList();
                HttpContext.Current.Session["SystemRole"] = lstRole;
                return lstRole;
            }
            else
            {
                return (List<DataAccess.Role>)(HttpContext.Current.Session["SystemRole"]);
            }
        }
        set { HttpContext.Current.Session["SystemRole"] = value; }
    }

    /// <summary>
    /// Return role list
    /// </summary>
    public static List<User> SystemUsers
    {
        get
        {
            if (HttpContext.Current.Session["Systemusers"] == null)
            {
                UserList userList = DataAccess.User.SelectAll();
                List<User> lstUser = userList.OrderBy(x => x.UserName).ToList();
                HttpContext.Current.Session["Systemusers"] = lstUser;
                return lstUser;
            }
            else
            {
                return (List<User>)(HttpContext.Current.Session["Systemusers"]);
            }
        }
        set { HttpContext.Current.Session["Systemusers"] = value; }
    }

    /// <summary>
    /// Return page list
    /// </summary>
    public static List<PageMaster> SystemPages
    {
        get
        {
            if (HttpContext.Current.Session["SystemPages"] == null)
            {
                PageMasterList pageList = PageMaster.SelectAll();
                List<PageMaster> lstPage = pageList.OrderBy(x => x.PageAlias).ToList();
                HttpContext.Current.Session["SystemPages"] = lstPage;
                return lstPage;
            }
            else
            {
                return (List<PageMaster>)(HttpContext.Current.Session["SystemPages"]);
            }
        }
        set { HttpContext.Current.Session["SystemPages"] = value; }
    }

    /// <summary>
    /// return users page access rights
    /// </summary>
    public static List<int> PageAccessRights
    {
        get
        {
            if (HttpContext.Current.Session["SystemPages"] == null)
            {
                return null;
            }
            else
            {
                return (List<int>)(HttpContext.Current.Session["SystemPages"]);
            }
        }
       set { HttpContext.Current.Session["SystemPages"] = value; }
    }

    #endregion
}