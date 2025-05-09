namespace ZooSimulator.Models
{
    public class Monkey(string name, int age, Gender gender) : 
        Animal(name, age, gender, SpeciesType.Monkey, ThresholdType.Starvation, 30)
    {
    }
}