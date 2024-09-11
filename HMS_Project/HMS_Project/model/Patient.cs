using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Patient : HmsUser
    {
       
        private int _patientID;
        public int PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        private string _patAddress;
        public string PatAddress
        {
            get { return _patAddress; }
            set { _patAddress = value; }
        }

        private long _ssn;
        public long SSN
        {
            get { return _ssn; }
            set { _ssn = value; }
        }












    }
}
