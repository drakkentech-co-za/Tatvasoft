using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Payroll_Submission table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>18-Sep-2013</CreatedDate>
	public partial class Payroll_SubmissionList : List<Payroll_Submission>
	{}

	/// <summary>
	/// Data access class for Payroll_Submission table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>18-Sep-2013</CreatedDate>
	public partial class Payroll_Submission
	{

		#region Methods/Functions

		/// <summary>
		/// Give the List object of Payroll_Submission as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static Payroll_SubmissionList SelectList(DataSet ds)
		{
			Payroll_SubmissionList lstPayroll_Submission = new Payroll_SubmissionList();
			Payroll_Submission objPayroll_Submission = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objPayroll_Submission = new Payroll_Submission();
					objPayroll_Submission.MakeObject(dr);
					lstPayroll_Submission.Add(objPayroll_Submission);
				}
			}
			return lstPayroll_Submission;
		}
		#endregion
	}
}
