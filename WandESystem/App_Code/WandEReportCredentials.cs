using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Configuration;

/// <summary>
/// Summary description for WandEReportCredentials
/// </summary>
/// <CreatedBy> Darpan Khandhar </CreatedBy>
/// <CreatedDate> 23-Sep-2013 </CreatedDate>
/// <ModifiedBy> Darpan Khandhar </ModifiedBy>
/// <ModifiedDate>23-Sep-2013 </ModifiedDate>
public class WandEReportCredentials : IReportServerCredentials
{
	public WandEReportCredentials()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public WindowsIdentity ImpersonationUser
    {
        get
        {
            // Use the default Windows user.  Credentials will be
            // provided by the NetworkCredentials property.
            return null;
        }
    }
    public ICredentials NetworkCredentials
    {
        get
        {
            // Read the user information from the Web.config file.  
            // By reading the information on demand instead of 
            // storing it, the credentials will not be stored in 
            // session, reducing the vulnerable surface area to the
            // Web.config file, which can be secured with an ACL.

            // User name
            string userName =
                ConfigurationManager.AppSettings
                    ["SSRSUserName"];

            if (string.IsNullOrEmpty(userName))
                throw new Exception(
                    "Missing UserName");

            // Password
            string password =
                ConfigurationManager.AppSettings
                    ["SSRSPassword"];

            if (string.IsNullOrEmpty(password))
                throw new Exception(
                    "Missing password from web.config file");

            // Domain
            string domain =
                ConfigurationManager.AppSettings
                    ["SSRSDomain"];

            if (string.IsNullOrEmpty(domain))
                throw new Exception(
                    "Missing domain from web.config file");

            return new NetworkCredential(userName, password, domain);
        }
    }

    public bool GetFormsCredentials(out Cookie authCookie,
                out string userName, out string password,
                out string authority)
    {
        authCookie = null;
        userName = null;
        password = null;
        authority = null;

        // Not using form credentials
        return false;
    }
}