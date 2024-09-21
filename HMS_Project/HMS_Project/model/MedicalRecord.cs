using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.model
{
    public class MedicalRecord
    {
        public int RecordID { get; set; }
        public string Diagnosis { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string LabResults { get; set; } = null!;

        #region One2Many With Patient
        public int PatientID { get; set; }
        public Patient Patient { get; set; } = null!;
        #endregion

        #region One2Many With Doctor
        public virtual Doctor Doctor { get; set; } = null!;
        #endregion
    }
}
