namespace ZooSimulator.Models
{
    public class Enclosure(SpeciesType type, string emoji) : Entity
    {
        public SpeciesType Type { get; set; } = type;

        public string Emoji { get; set; } = emoji;

        public bool FedThisHour { get; set; }
    }
}