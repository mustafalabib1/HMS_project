namespace DALProject.model
{
    public class Prescription : ModelBase
    {

        #region One2Many With PrescriptionItem
        public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new HashSet<PrescriptionItem>();
        #endregion

        #region One2Many With Pharmacist
        public int? PharmacistId { get; set; }
        public virtual Pharmacist Pharmacist { get; set; } = null!;
        #endregion
        
        #region One2One With Apointment
        public int ApointmentId { get; set; }
        public virtual Apointment Apointment { get; set; }=null!;
        #endregion

        #region One2Many With Doctor
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
    }
}
