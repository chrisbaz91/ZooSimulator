using System.ComponentModel.DataAnnotations;
using ZooSimulator.Models;

namespace ZooSimulator.ViewModels
{
    public class DetailsModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        public SpeciesType Type { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public double Health { get; set; }
    }
}