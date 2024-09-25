using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Receptionist : HmsUser
    {
        #region One2Many With Reception
        public int? ReceptionID { get; set; }
        public Reception Reception { get; set; } = null!; 
        #endregion
    }
}
