using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class MedicationViewModel
    {
        public MedicationViewModel() { }

        public MedicationViewModel(Medication medication)
        {
            MedicationId = medication.MedicationId;
            MedName = medication.MedName;
            Strength = medication.Strength;
            ActSubInMed = medication.ActiveSubstances.ToHashSet();
        }
        public ICollection<PrescriptionItem> PrescriptionItemsReader { get; set; } = new HashSet<PrescriptionItem>();
        public ICollection<ActiveSubstance> ActSubDateReader { get; set; } = new HashSet<ActiveSubstance>();
        [Required]
        public int MedicationId { get; set; }

        [Required]
        public string MedName { get; set; } = null!;

        [Required]
        public int Strength { get; set; }
        public ICollection<ActiveSubstance> ActSubInMed { get; set; } = new HashSet<ActiveSubstance>();
        public ICollection<PrescriptionItem> PrescriptionItemInMed { get; set; } = new HashSet<PrescriptionItem>();


        public static explicit operator Medication(MedicationViewModel medViewModel)
        {
            return new Medication()
            {
                MedicationId = medViewModel.MedicationId,
                MedName = medViewModel.MedName,
                Strength = medViewModel.Strength,
                ActiveSubstances = medViewModel.ActSubInMed
            };
        }


        public static explicit operator MedicationViewModel(Medication medication)
        {
            return new MedicationViewModel(medication);
        }


    }
}
