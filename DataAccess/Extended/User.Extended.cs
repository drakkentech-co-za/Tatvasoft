using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DataAccess
{
	/// <summary>
	/// Data access class for Users table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>26-Sep-2013</CreatedDate>
	public partial class UserList : List<User>
	{}

	/// <summary>
	/// Data access class for Users table.
	/// </summary>
	/// <CreatedBy>Darpan Khandhar</CreatedBy>
	/// <CreatedDate>26-Sep-2013</CreatedDate>
	public partial class User
    {
        #region "Enum"

        /// <summary>
        /// Enum for user roles
        /// </summary>
        public enum UserRoles
        {
            Admin = 1,
            Payroll = 2,
            Employee = 3,
            Maint = 4
        }

        #endregion

        #region Methods/Functions

        /// <summary>
		/// Give the List object of Users as per DataSet
		/// </summary>
		/// <param name="ds"></param>
		/// <returns>List</returns>
		public static UserList SelectList(DataSet ds)
		{
			UserList lstUsers = new UserList();
			User objUsers = null;
			if(ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					objUsers = new User();
					objUsers.MakeObject(dr);
					lstUsers.Add(objUsers);
				}
			}
			return lstUsers;
		}

        /// <summary>
        /// Authentication get role
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static DataTable GetRole(string userName)
        {
            DataTable dt = null;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("Users_GetUserRole");
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName );

                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];

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

            return dt;
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static  DataTable GetUserByUserName(string username)
        {
            DataTable dt = null;
            Database db = null;
            DbCommand dbCommand = null;
            try
            {
                db = DatabaseFactory.CreateDatabase();
                dbCommand = db.GetStoredProcCommand("Users_GetUserByUserName");
                db.AddInParameter(dbCommand, "UserName", DbType.String, username);

                DataSet ds = db.ExecuteDataSet(dbCommand);

                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
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

            return dt;
        }

		#endregion
	}
}
