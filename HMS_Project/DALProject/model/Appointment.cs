using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALProject.model
{
    public class Apointment : ModelBase
    {
        public DateOnly ApointmentDate { get; set; }
        public TimeOnly? ApointmentTime { get; set; }
        public ApointmentStatusEnum ApointmentStatus { get; set; } = ApointmentStatusEnum.Scheduled;
        public virtual string? Examination { get; set; } = null!;

        #region One2Many With Receptionist
        public int? ReceptionistId { get; set; }
        public virtual Receptionist Receptionist { get; set; } = null!;
        #endregion

        #region One2Many With Clinic
        public int ClinicId { get; set; }
        public virtual Clinic Clinic { get; set; } = null!;
        #endregion

        #region One2Many With Patient
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
        #endregion

        #region One2Many With Doctor
        public virtual int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion

        #region One2One With Invoice
        public virtual Invoice Invoice { get; set; } = null!;
		#endregion

		#region One2One With Prescription
		[Column("PrescriptionId")]
		public int? PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; } = null!;
        #endregion

    }
}
