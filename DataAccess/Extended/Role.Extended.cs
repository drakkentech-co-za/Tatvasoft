using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Role table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>26-Sep-2013</CreatedDate>
	public partial class RoleList : List<Role>
	{}

	/// <summary>
	/// Data access class for Role table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>26-Sep-2013</CreatedDate>
	public partial class Role
    {
        #region "Enum"

        /// <summary>
        /// Enum for users role
        /// </summary>
        public enum UserRoles
        {
            Admin = 1,
            Approver = 2,
            InvoiceAnalyst = 3,
            Employee = 4
        }

        #endregion

        #region Methods/Functions

        /// <summary>
		/// Give the List object of Role as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static RoleList SelectList(DataSet ds)
		{
			RoleList lstRole = new RoleList();
			Role objRole = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objRole = new Role();
					objRole.MakeObject(dr);
					lstRole.Add(objRole);
				}
			}
			return lstRole;
		}
		#endregion
	}
}
