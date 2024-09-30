namespace DALProject.model
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }

        #region One2Many With PrescriptionItem
        public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new HashSet<PrescriptionItem>();
        #endregion

        #region One2Many With Pharmacist
        public long? PharmacistId { get; set; }
        public virtual Pharmacist Pharmacist { get; set; } = null!;
        #endregion
        
        #region One2One With Appointment
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }=null!;
        #endregion

        #region One2Many With Doctor
        public long DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
    }
}
