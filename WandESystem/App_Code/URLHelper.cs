using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

/// -----------------------------------------------------------------------------
/// <summary>
/// Central place to define URL related functionality
/// </summary>
/// <remarks>
/// There is room here to define more functions to resolve image paths, tracking URL referrers etc..
/// </remarks>
/// -----------------------------------------------------------------------------
public class URLHelper
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// Resolve Path To Application Root which is starting with ~ \ / or .
    /// </summary>
    /// <param name="Url">the partial URL to resolve</param>
    /// <remarks>
    /// </remarks>
    /// -----------------------------------------------------------------------------
    public static string GetPath(string Url)
    {
        string returnString = null;
        string appPath = null;

        try
        {
            appPath = System.Web.HttpContext.Current.Request.ApplicationPath;

            if (string.IsNullOrEmpty(appPath))
            {
                appPath = "http";

                if (System.Web.HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"] == "1")
                {
                    appPath += "s";
                }

                appPath += "://" + System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"];

                string scriptName = System.Web.HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"];

                appPath += scriptName.Substring(0, scriptName.LastIndexOf("/"));
            }

            if (string.IsNullOrEmpty(Url))
                return appPath;

            switch (Url.Substring(0, 1))
            {
                case "/":
                    returnString = (appPath + Url);
                    break;
                case "~":
                    returnString = (appPath + Url.Substring(1));
                    break;
                case "\\":
                    returnString = (appPath + "/" + Url.Substring(1));
                    break;
                case ".":
                    returnString = Url;
                    break;
                default:
                    returnString = (appPath + "/" + Url);
                    break;
            }

            return returnString.Replace("//", "/");
        }
        finally
        {
            Common.DisposeOf(appPath);
        }
    }

}
