﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class DoctorScheduleLookup : ModelBase
    {
        //public virtual DayOfWeek Day { get; set; }
        public DayOfWeek Day { get; set; }
        public  TimeOnly StartTime {  get; set; }
        public  TimeOnly EndTime { get; set; }

        #region One2Many With Doctors
        public string DoctorUserId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
    }
}
