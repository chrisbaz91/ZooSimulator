namespace ZooSimulator.Models
{
    public class Giraffe(string name, int age, Gender gender) : 
        Animal(name, age, gender, SpeciesType.Giraffe, ThresholdType.Starvation, 50)
    {
    }
}