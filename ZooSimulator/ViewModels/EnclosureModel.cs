using ZooSimulator.Models;

namespace ZooSimulator.ViewModels
{
    public class EnclosureModel
    {
        public SpeciesType Type { get; set; }

        public string Emoji { get; set; } = "";

        public IEnumerable<ListItemModel> Animals { get; set; } = [];

        public bool FedThisHour { get; set; }
    }
}