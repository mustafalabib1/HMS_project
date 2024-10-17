using DALProject.model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLProject.ViewModels
{
    public class ActiveSubstanceInteractionViewModel
    {
        [Required]
        public int ActSubId { get; set; }
        [Required]
        public string Interaction { get; set; } = null!;
        public string? OtherSubstanceName { get; set; }
    }
}
