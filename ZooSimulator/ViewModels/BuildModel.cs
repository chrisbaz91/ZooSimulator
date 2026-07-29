using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ZooSimulator.Models;

namespace ZooSimulator.ViewModels
{
    public class BuildModel
    {
        [Required]
        public SpeciesType Type { get; set; }

        public List<SelectListItem> List { get; set; }
    }
}