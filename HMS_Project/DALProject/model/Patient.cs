﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Patient : HmsUser
    {
        #region One2Many with ActiveSubstance Active Substance that has from it allergy
        public virtual ICollection<ActiveSubstance> ActSbuAllergies { get; set; } = new HashSet<ActiveSubstance>(); 
        #endregion

        #region One2Many With Appointment
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        #endregion
    }
}
