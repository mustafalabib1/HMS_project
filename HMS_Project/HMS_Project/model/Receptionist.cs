using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Receptionist : HmsUser
    {
        public int ReceptionistID { get; set; }

        #region One2Many With Reception
        public int ReceptionID { get; set; }
        public Reception Reception { get; set; } = null!; 
        #endregion

        //public long SSN { get; set; }
    }
}
