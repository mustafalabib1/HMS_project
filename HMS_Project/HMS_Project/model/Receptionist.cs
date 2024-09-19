using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    internal class Receptionist
    {
        private int _receptionistID;
        public int ReceptionistID
        {
            get { return _receptionistID; }
            set { _receptionistID = value; }
        }

        private int _receptionID;
        public int ReceptionID
        {
            get { return _receptionID; }
            set { _receptionID = value; }
        }

        private long _ssn;
        public long SSN
        {
            get { return _ssn; }
            set { _ssn = value; }
        }
    }
}
