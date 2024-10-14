using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DALProject.model;
using Microsoft.EntityFrameworkCore;

namespace PLProject.ViewModels
{
    public class MedicationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Medication Name is required.")]
        [Display(Name = "Medication Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Strength is required.")]
        [Display(Name = "Strength")]
        [Range(1, int.MaxValue, ErrorMessage = "Strength must be a positive number.")]
        public int Strength { get; set; }

        // This property can hold the IDs of active substances associated with the medication
        public List<int> ActiveSubstanceIds { get; set; } = new List<int>();

        // This property can hold a list of active substances for display purposes
        public List<ActiveSubstanceViewModel> ActiveSubstances { get; set; } = new List<ActiveSubstanceViewModel>();

        // Consider removing ActiveSubstanceNames if not used
        // public List<string> ActiveSubstanceNames { get; set; } = new List<string>();


    }
}
