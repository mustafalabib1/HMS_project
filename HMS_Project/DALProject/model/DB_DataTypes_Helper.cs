using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    #region required enumes
    public enum Gender
    {
        Male,
        Female
    }
    public enum PaymentType
    {
        Cash = 1,
        CreditCard = 2
    }
    public enum ApointmentStatusEnum
    {
        Scheduled = 1,
        Completed = 2,
        Cancelled = 3,
        Confirmed= 4,
        NoShow = 5
    }

    #endregion

    internal class DB_DataTypes_Helper
    {
        public const string nvarchar = "nvarchar";
        public const string varchar = "varchar";
        public const string _char = "char";
        public const string bit = "bit";
        public const string date = "date";
        public const string time = "time";
    }
}
