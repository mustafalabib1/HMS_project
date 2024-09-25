using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class DoctorScheduleLookup
    {
        public virtual WeekDays Day { get; set; }
        public virtual TimeOnly StartTime {  get; set; }
        public virtual TimeOnly EndTime { get; set; }

        #region One2Many With Doctors
        public long DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
    }
}
