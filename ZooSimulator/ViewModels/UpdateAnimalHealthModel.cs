namespace ZooSimulator.ViewModels
{
    public class UpdateAnimalHealthModel(Guid id, double health)
    {
        public Guid Id { get; set; } = id;

        public double Health { get; set; } = health;
    }
}