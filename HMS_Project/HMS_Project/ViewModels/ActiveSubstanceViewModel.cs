using DALProject.Data.Contexts;
using DALProject.model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.ViewModels
{
    public class ActiveSubstanceViewModel
    {
        public ActiveSubstanceViewModel()
        {
        }
        public ActiveSubstanceViewModel(ActiveSubstance activeSubstance)
        {
            ActiveSubstancesName = activeSubstance.ActiveSubstancesName;

            // Mapping medications
            Medications = activeSubstance.Medications.ToHashSet();

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
            .ToHashSet();
        }

        [Required]
        public string ActiveSubstancesName { get; set; } = null!;
        public IEnumerable<ActiveSubstance>? ActiveSubstancesDateReader { get; set; } = new List<ActiveSubstance>();
        public IEnumerable<Medication>? MedicationsDateReader { get; set; } 
        public ICollection<Medication> Medications { get; set; } = new HashSet<Medication>();
        public ICollection<ActiveSubstanceInteractionViewModel> Interactions { get; set; } = new HashSet<ActiveSubstanceInteractionViewModel>();

        public static explicit operator ActiveSubstance(ActiveSubstanceViewModel viewModel)
        {
            var activeSubstance = new ActiveSubstance
            {
                ActiveSubstancesName = viewModel.ActiveSubstancesName,
                Medications = viewModel.Medications
            };
            foreach (var interactionViewModel in viewModel.Interactions)
            {
                activeSubstance.ActSub1.Add(new ActiveSubstanceInteraction
                {
                    Interaction = interactionViewModel.Interaction,
                    ActiveSubstanceId2 = interactionViewModel.ActSubId
                });
            }
            return activeSubstance;
        }

        public static explicit operator ActiveSubstanceViewModel(ActiveSubstance activeSubstance)
        {
            return new ActiveSubstanceViewModel(activeSubstance);
        }
    }
}
