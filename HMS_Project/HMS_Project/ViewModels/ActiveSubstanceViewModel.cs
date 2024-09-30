using DALProject.Data.Contexts;
using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    internal class ActiveSubstanceViewModel
    {

        [Required]
        public string ActiveSubstancesName { get; set; } = null!;
        public ICollection<ActiveSubstance> ActiveSubstancesInDB { get; set; }= new List<ActiveSubstance>();
        public ICollection<MedicationViewModel> Medications { get; set; } = new HashSet<MedicationViewModel>();
        public ICollection<ActiveSubstanceInteractionViewModel> Interactions { get; set; } = new HashSet<ActiveSubstanceInteractionViewModel>();

        public static explicit operator ActiveSubstance(ActiveSubstanceViewModel viewModel)
        {
            var activeSubstance = new ActiveSubstance
            {
                ActiveSubstancesName = viewModel.ActiveSubstancesName,
                Medications = viewModel.Medications.Select(med => (Medication)med).ToHashSet()
            };
            foreach (var interactionViewModel in viewModel.Interactions)
            {
                activeSubstance.ActSub1.Add(new ActiveSubstanceInteraction
                {
                    Interaction = interactionViewModel.Interaction,
                    ActiveSubstanceId2= interactionViewModel.ActSubId
                });
            }

            return activeSubstance;
        }

        public static explicit operator ActiveSubstanceViewModel(ActiveSubstance activeSubstance)
        {
            return new ActiveSubstanceViewModel
            {
                ActiveSubstancesName = activeSubstance.ActiveSubstancesName,

                // Mapping medications
                Medications = activeSubstance.Medications.Select(med => (MedicationViewModel)med).ToHashSet(),

                // Mapping interactions from ActSub1 and ActSub2
                Interactions = activeSubstance.ActSub1.Select(interaction => new ActiveSubstanceInteractionViewModel
                {
                    Interaction = interaction.Interaction,
                    OtherSubstanceName = interaction.ActSub2?.ActiveSubstancesName ?? "Unknown"
                })
                .Concat(activeSubstance.ActSub2.Select(interaction => new ActiveSubstanceInteractionViewModel
                {
                    Interaction = interaction.Interaction,
                    OtherSubstanceName = interaction.ActSub1?.ActiveSubstancesName ?? "Unknown"
                }))
                .ToHashSet()
            };
        }
    }
}
