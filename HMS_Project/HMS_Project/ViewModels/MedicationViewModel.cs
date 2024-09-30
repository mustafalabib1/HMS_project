using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class MedicationViewModel
    {
        public MedicationViewModel() { }

        public MedicationViewModel(Medication medication)
        {
            MedicationCode = medication.MedicationCode;
            MedName = medication.MedName;
            Strength = medication.Strength;
            ActSubInMed =medication.ActiveSubstances.ToHashSet();
        }
        public ICollection<ActiveSubstance> ActSubDateReader { get; set; } = new HashSet<ActiveSubstance>();
        [Required]
        public string MedicationCode { get; set; } = null!;

        [Required]
        public string MedName { get; set; } = null!;

        [Required]
        public int Strength { get; set; }
        public ICollection<ActiveSubstance> ActSubInMed { get; set; } = new HashSet<ActiveSubstance>();



        public static explicit operator Medication(MedicationViewModel medViewModel)
        {
            return new Medication()
            {
                MedicationCode = medViewModel.MedicationCode,
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
