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
    /// <CreatedDate>01-Oct-2013</CreatedDate>
    public partial class User
    {
        #region Fields

        private int userId;
        private string userName;
        private string password;
        private int roleId;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User(DataSet ds)
        {
            MakeObject(ds);
        }

        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User(int userId)
        {
            DataSet ds = Select(userId);
            MakeObject(ds);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the UserId value.
        /// </summary>
        public virtual int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// Gets or sets the UserName value.
        /// </summary>
        public virtual string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// Gets or sets the Password value.
        /// </summary>
        public virtual string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// Gets or sets the RoleId value.
        /// </summary>
        public virtual int RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.userName = string.Empty;
            this.password = string.Empty;
            this.roleId = 0;
        }

        /// <summary>
        /// Create object by DataSet
        /// </summary>
        /// <param name="ds"></param>
        private void MakeObject(DataSet ds)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MakeObject(ds.Tables[0].Rows[0]);
            }
            else
                InitVariables();
        }

        /// <summary>
        /// Create object by DataRow
        /// </summary>
        /// <param name="dr"></param>
        private void MakeObject(DataRow dr)
        {
            DataTable dt = dr.Table;
            if (dt.Columns.Contains("UserId"))
            {
                int.TryParse(Convert.ToString(dr["UserId"]), out userId);
            }
            if (dt.Columns.Contains("UserName"))
            {
                userName = Convert.ToString(dr["UserName"]);
            }
            if (dt.Columns.Contains("Password"))
            {
                password = Convert.ToString(dr["Password"]);
            }
            if (dt.Columns.Contains("RoleId"))
            {
                int.TryParse(Convert.ToString(dr["RoleId"]), out roleId);
            }
        }

        /// <summary>
        /// Saves a record to the tbl_Users table.
        /// </summary>
        public void Save()
        {
            if (userId > 0)
                Update();
            else
                Insert();
        }

        /// <summary>
        /// Inserts a record into the Users table.
        /// </summary>
        public void Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_UsersInsert"))
            {
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                db.AddInParameter(dbCommand, "Password", DbType.String, password);
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                // Execute the query and return the new identity value
                int returnValue = Convert.ToInt32(db.ExecuteScalar(dbCommand));

                userId = returnValue;
            }
            db = null;
        }

        /// <summary>
        /// Updates a record in the Users table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_UsersUpdate"))
            {
                db.AddInParameter(dbCommand, "UserId", DbType.Int32, userId);
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                db.AddInParameter(dbCommand, "Password", DbType.String, password);
                db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }

        /// <summary>
        /// Deletes a record from the Users table by a composite primary key.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>String</returns>
        public static string Delete(int userId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_UsersDelete"))
            {
                db.AddInParameter(dbCommand, "UserId", DbType.Int32, userId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547)
                        return "Users is already in use. You can not delete this Users.";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }

        /// <summary>
        /// Selects a single record from the Users table.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>DataSet</returns>
        private static DataSet Select(int userId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_UsersSelect"))
            {
                db.AddInParameter(dbCommand, "UserId", DbType.Int32, userId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Selects all records from the Users table.
        /// </summary>
        /// <returns>DataSet</returns>
        public static UserList SelectAll()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_UsersSelectAll"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        /// <summary>
        /// Search records from the Users table as per criteria
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <returns>DataSet</returns>
        public UserList Search()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("USP_tbl_UsersSearch"))
            {
                if (userId > 0)
                    db.AddInParameter(dbCommand, "UserId", DbType.Int32, userId);
                if (userName != string.Empty)
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                if (password != string.Empty)
                    db.AddInParameter(dbCommand, "Password", DbType.String, password);
                if (roleId > 0)
                    db.AddInParameter(dbCommand, "RoleId", DbType.Int32, roleId);

                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }

        #endregion
    }
}
