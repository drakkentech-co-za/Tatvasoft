using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for PageMaster table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>26-Sep-2013</CreatedDate>
	public partial class PageMasterList : List<PageMaster>
	{}

	/// <summary>
	/// Data access class for PageMaster table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>26-Sep-2013</CreatedDate>
	public partial class PageMaster
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of PageMaster as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static PageMasterList SelectList(DataSet ds)
		{
			PageMasterList lstPageMaster = new PageMasterList();
			PageMaster objPageMaster = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objPageMaster = new PageMaster();
					objPageMaster.MakeObject(dr);
					lstPageMaster.Add(objPageMaster);
				}
			}
			return lstPageMaster;
		}

		#endregion
	}
}
