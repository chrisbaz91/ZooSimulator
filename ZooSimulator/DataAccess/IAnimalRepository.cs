using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.DataAccess
{
    public interface IAnimalRepository
    {
        public Task<List<Animal>> GetAnimals();

        public Task<List<Animal>> GetTypeAnimals(SpeciesType type);

        public Task<bool> AnimalExists(Guid id);

        public Task<Animal> GetAnimal(Guid id);

        public Task<Guid> AddAnimal(Animal Animal);

        public Task<bool> UpdateAnimalHealth(UpdateAnimalHealthModel model);

        public Task<bool> UpdateAnimal(EditModel model);

        public Task<bool> RemoveAnimal(Guid id);
    }
}
