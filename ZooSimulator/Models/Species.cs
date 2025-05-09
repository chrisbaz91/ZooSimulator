namespace ZooSimulator.Models
{
    public abstract class Species : Entity
    {
        public Species(SpeciesType type, ThresholdType thresholdType, double healthThreshold)
        {
            if (healthThreshold < 0 || healthThreshold > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(healthThreshold));
            }

            Type = type;
            ThresholdType = thresholdType;
            HealthThreshold = healthThreshold;
        }
        public SpeciesType Type { get; set; }

        public ThresholdType ThresholdType { get; set; }

        public double HealthThreshold { get; set; }
    }
}