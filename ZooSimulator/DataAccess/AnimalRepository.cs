using Microsoft.EntityFrameworkCore;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.DataAccess
{
    public class AnimalRepository(ZooContext context) : IAnimalRepository
    {
        public async Task<List<Animal>> GetAnimals()
        {
            return await context.Animals.ToListAsync();
        }

        public async Task<List<Animal>> GetTypeAnimals(SpeciesType type)
        {
            return await context.Animals.Where(x => x.Type == type).ToListAsync();
        }

        public async Task<bool> AnimalExists(Guid id)
        {
            return await context.Animals.AnyAsync(e => e.Id == id);
        }

        public async Task<Animal> GetAnimal(Guid id)
        {
            return await context.Animals.FindAsync(id);
        }

        public async Task<Guid> AddAnimal(Animal animal)
        {
            await context.Animals.AddAsync(animal);

            await context.SaveChangesAsync();

            return animal.Id;
        }

        public async Task<bool> UpdateAnimalHealth(UpdateAnimalHealthModel model)
        {
            var animal = await GetAnimal(model.Id);

            animal.Health = model.Health;

            // Health can be a minimum of 0 and a maximum of 100
            if (animal.Health < 0)
            {
                animal.Health = 0;
            }
            else if (animal.Health > 100)
            {
                animal.Health = 100;
            }

            switch (animal.ThresholdType)
            {
                // If the species is the Starvation threshold type then check death immediately
                case ThresholdType.Starvation:
                    DeathCheck(animal);

                    break;
                // If the species is the Walking threshold type then check death only if the animal was not previously walking
                case ThresholdType.Walking:
                    if (!animal.Walking)
                    {
                        DeathCheck(animal);
                    }

                    // Update Walking true or false according to the current health being above or below the threshold
                    animal.Walking = !(animal.Health < animal.HealthThreshold);

                    break;
                default:
                    break;
            }

            await context.SaveChangesAsync();

            return true;
        }

        private static void DeathCheck(Animal animal)
        {
            // If the animal's current health is below the threshold then its health becomes zero and it dies
            if (animal.Health < animal.HealthThreshold)
            {
                animal.Health = 0;
            }
        }

        public async Task<bool> UpdateAnimal(EditModel model)
        {
            var animal = await GetAnimal(model.Id);

            UpdateAnimalModel(animal, model);

            await context.SaveChangesAsync();

            return true;
        }

        private static void UpdateAnimalModel(Animal animal, EditModel model)
        {
            animal.Name = model.Name;
            animal.Age = model.Age;
            animal.Gender = model.Gender;
        }

        public async Task<bool> RemoveAnimal(Guid id)
        {
            var animal = await GetAnimal(id);

            context.Animals.Remove(animal);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
