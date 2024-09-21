using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class Apointment
    {
        public  int ApointmentId { get; set; }
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly ApointmentTime { get; set; }
        public char ApointmentStatus { get; set; }

        #region One2Many With Reception
        public int ReceptionId { get; set; }
        public Reception Reception { get; set; } = null!;
        #endregion

        #region One2Many With Clinic
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; } = null!;
        #endregion

        #region One2Many With Patient
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!; 
        #endregion
    }
}
