namespace ZooSimulator.Models
{
    public class Tiger(string name, int age, Gender gender) : 
        Animal(name, age, gender, SpeciesType.Giraffe, ThresholdType.Starvation, 35)
    {
    }
}