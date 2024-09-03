using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace DataAccess
{
    public class SystemEnum
    {
        #region Enum

        /// <summary>
        /// enum for SortDirection
        /// </summary>
        public enum SortDirection
        {
            asc,
            desc
        };

        /// <summary>
        /// enum for Gender
        /// </summary>
        public enum Gender
        {
            Male = 1,
            Female = 2,
        };

        /// <summary>
        /// enum for User Type
        /// </summary>
        public enum UserRole
        {
            Admin = 1,
            Role2 = 2,
            Role3= 3,
            Role4 = 3 
        };

        /// <summary>
        /// enum of all user rights.
        /// </summary>
        public enum UserRights
        {
            IsViewAllow,
            IsInsertAllow,
            IsDeleteAllow,
            IsEditAllow,
            IsPrintAllow
        };

        /// <summary>
        /// enum for file types
        /// </summary>
        public enum FileType
        {
            XLS,
            XLSX,
            CSV,
            PNG,
            JPEG,
            JPG,
            BMP,
            GIF,
            TXT,
            PDF
        }       
        #endregion
    }
}
