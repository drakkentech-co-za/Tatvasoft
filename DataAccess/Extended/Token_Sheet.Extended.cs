using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// Data access class for Token_Sheet table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>13-Sep-2013</CreatedDate>
    public partial class Token_SheetList : List<Token_Sheet>
    { }

    /// <summary>
    /// Data access class for Token_Sheet table.
    /// </summary>
    /// <CreatedBy>Darpan Khandhar</CreatedBy>
    /// <CreatedDate>13-Sep-2013</CreatedDate>
    public partial class Token_Sheet
    {

        #region Methods/Functions

        /// <summary>
        /// Give the List object of Token_Sheet as per DataSet
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>List</returns>
        public static Token_SheetList SelectList(DataSet ds)
        {
            Token_SheetList lstToken_Sheet = new Token_SheetList();
            Token_Sheet objToken_Sheet = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objToken_Sheet = new Token_Sheet();
                    objToken_Sheet.MakeObject(dr);
                    lstToken_Sheet.Add(objToken_Sheet);
                }
            }
            return lstToken_Sheet;
        }

        #endregion
    }
}
