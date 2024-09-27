using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public enum Gender 
    {
        Male ,
        Female ,
    }

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
