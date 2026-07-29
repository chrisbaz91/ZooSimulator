namespace ZooSimulator.Models
{
    public class Rhino(string name, int age, Gender gender) : 
        Animal(name, age, gender, SpeciesType.Elephant, ThresholdType.Walking, 55)
    {
    }
}