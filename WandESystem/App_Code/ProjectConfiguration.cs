using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary Description for Project Configuration
/// </summary>
/// <CreatedBy>Darpan Khandhar</CreatedBy>
/// <CreatedDate> 25-Jun-2013 </CreatedDate>
/// <ModifiedBy>Darpan Khandhar</ModifiedBy>
/// <ModifiedDate> 26-Jun-2013 </ModifiedDate>
public class ProjectConfiguration
{
    #region Constructor

    public ProjectConfiguration()
    {
    }

    #endregion

    #region Variable
    private static string rootPath;
    #endregion

    #region Methods

    /// <summary>
    /// Set the Default Value in Projects
    /// </summary>
    /// <param name="RootPath"></param>
    public static void OnApplicationStart(string RootPath)
    {
        rootPath = RootPath;
        DataAccess.General.ApplicationPath = RootPath;
    }

    #endregion

    #region Property

    #region AppSettings Key

    /// <summary>
    /// Return Culture
    /// </summary>
    public static string Culture
    {
        get { return Convert.ToString(ConfigurationManager.AppSettings["Culture"]); }
    }

    /// <summary>
    /// From Display
    /// </summary>
    public static string FromDisplay
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["FromDisplay"]); }
    }

    /// <summary>
    /// Support Email
    /// </summary>
    public static string SupportEmail
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SupportEmail"]); }
    }

    /// <summary>
    /// Admin Email
    /// </summary>
    public static string AdminEmail
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["AdminEmail"]); }
    }

    /// <summary>
    /// Return Error Email ID
    /// </summary>
    public static string ErrorEmail
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"]); }
    }

    /// <summary>
    /// Return InfoEmail ID
    /// </summary>
    public static string InfoEmail
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["InfoEmail"]); }
    }

    /// <summary>
    /// Return MailServerHost
    /// </summary>
    public static string MailServerHost
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MailServerHost"]); }
    }

    /// <summary>
    /// Return MailServerHost
    /// </summary>
    public static string SMTPuserName
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPuserName"]); }
    }

    /// <summary>
    /// Return SMTPpassword
    /// </summary>
    public static string SMTPpassword
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SMTPpassword"]); }
    }

    /// <summary>
    /// Return ProxyWeb
    /// </summary>
    public static string ProxyWeb
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ProxyWeb"]); }
    }

    /// <summary>
    /// Return blnWebProxy
    /// </summary>
    public static string Proxy
    {
        get { return Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["blnWebProxy"]); }
    }

    #endregion

    #region System Path

    /// <summary>
    /// Return the Root Path of the Project
    /// </summary>
    public static string ApplicationRootPath
    {
        get
        {
            if (rootPath.EndsWith("\\"))
            {
                return rootPath;
            }
            else
            {
                return rootPath + "\\";
            }
        }

    }

    /// <summary>
    /// Return HostName
    /// </summary>
    public static string HostName
    {
        get { return HttpContext.Current.Request.Url.Host; }
    }

    /// <summary>
    /// Retrun Url Suffix
    /// </summary>
    private static string UrlSuffix
    {
        get
        {
            if (HttpContext.Current.Request.ApplicationPath == "/")
            {
                return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath;
            }
            else
            {
                return HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/";
            }
        }
    }

    /// <summary>
    /// Retrun Secure User Base
    /// </summary>
    public static string SecureUrlBase
    {
        get
        {
            return "https://" + UrlSuffix;
        }

    }

    /// <summary>
    /// Retrun Url Base
    /// </summary>
    public static string UrlBase
    {
        get
        {
            return "http://" + UrlSuffix;
        }
    }

    /// <summary>
    /// Retrun Site Url Base
    /// </summary>
    public static string SiteUrlBase
    {
        get
        {
            if (HttpContext.Current.Request.IsSecureConnection)
            {
                return SecureUrlBase;
            }
            else
            {
                return UrlBase;
            }
        }
    }

    /// <summary>
    /// Return Image Path
    /// </summary>
    public static string Images
    {
        get
        {
            return (SiteUrlBase + "Images/").ToLower();
        }
    }

    //for Common Images
    public static string ImagesDefault
    {
        get
        {
            return (SiteUrlBase + "Images/").ToLower();
        }
    }

    /// <summary>
    /// Get Page Url
    /// </summary>
    /// <param name="strPage"></param>
    /// <returns></returns>
    public static string GetPageURL(string strPage)
    {
        return (SiteUrlBase + strPage).ToLower();
    }
    #endregion

    #region Other Setting

    /// <summary>
    /// Return Current Culture Date Format
    /// </summary>
    public static string DateFormat
    {
        get { return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern; }// "dd/MM/yyyy"; }
    }

    /// <summary>
    /// Return Current Culture DateTime Format
    /// </summary>
    public static string DateTimeFormat
    {
        get { return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern; }// "dd/MM/yyyy"; }
    }

    /// <summary>
    /// Return Time Format
    /// </summary>
    public static string TimeFormat
    {
        get { return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern; }// "dd/MM/yyyy"; }
    }

    /// <summary>
    /// Return Decimal Format
    /// </summary>
    public static string DecimalFormat
    {
        get { return "#,###,##0.##"; }// "1,234,567.89"; }
    }

    public static string EmailTemplatePath
    {
        get
        {
            return HttpContext.Current.Server.MapPath("~/Email/");
        }
    }

    #endregion

    #endregion
}