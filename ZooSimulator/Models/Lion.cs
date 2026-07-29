namespace ZooSimulator.Models
{
    public class Lion(string name, int age, Gender gender) : 
        Animal(name, age, gender, SpeciesType.Giraffe, ThresholdType.Starvation, 40)
    {
    }
}