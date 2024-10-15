namespace DALProject.model
{
    public class Prescription : ModelBase
    {

        #region One2Many With PrescriptionItem
        public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new HashSet<PrescriptionItem>();
        #endregion

        #region One2Many With Pharmacist
        public string? PharmacistId { get; set; }
        public virtual Pharmacist Pharmacist { get; set; } = null!;
        #endregion

        #region One2Many With Doctor
        public string DoctorId { get; set; } = null!;
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
        public virtual Apointment Apointment { get; set; }=null!;
    }
}
