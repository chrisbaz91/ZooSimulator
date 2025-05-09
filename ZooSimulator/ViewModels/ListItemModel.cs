using System.ComponentModel.DataAnnotations;

namespace ZooSimulator.ViewModels
{
    public class ListItemModel
    {
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public double Health { get; set; }
    }
}