using ZooSimulator.Models;

namespace ZooSimulator.ViewModels
{
    public class UpdateFedModel(SpeciesType type, bool fed)
    {
        public SpeciesType Type { get; set; } = type;

        public bool Fed { get; set; } = fed;
    }
}