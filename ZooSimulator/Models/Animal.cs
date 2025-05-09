namespace ZooSimulator.Models
{
    public class Animal : Species
    {
        public Animal(string name, int age, Gender gender, SpeciesType type, ThresholdType thresholdType, double healthThreshold) :
            base(type, thresholdType, healthThreshold)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (name.Length > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }
            if (age < 0 || age > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(age));
            }

            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public double Health { get; set; } = 100;

        public bool Walking { get; set; } = true;
    }
}