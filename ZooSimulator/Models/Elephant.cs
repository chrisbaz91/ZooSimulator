namespace ZooSimulator.Models
{
    public class Elephant(string name, int age, Gender gender) : 
        Animal(name, age, gender, SpeciesType.Elephant, ThresholdType.Walking, 70)
    {
    }
}