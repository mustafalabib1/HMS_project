﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Pharmacist : HmsUser
    {
        public int pharmacistID;
        public int PharmacistID
        {
            get { return pharmacistID; }
            set { pharmacistID = value; }
        }

        public int pharmacyId;
        public int PharmacyId
        {
            get { return pharmacyId; }
            set { pharmacyId = value; }
        }

        public long ssn;
        public long SSN
        {
            get { return ssn; }
            set { ssn = value; }
        }

        public Pharmacy Pharmacy { get; set; }

        
    }
}
