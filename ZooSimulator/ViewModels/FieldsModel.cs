using System.ComponentModel.DataAnnotations;
using ZooSimulator.Models;

namespace ZooSimulator.ViewModels
{
    public class FieldsModel
    {
        [Required]
        public SpeciesType Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [Range(0, 100)]
        public double Health { get; set; }
    }
}