using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class FeedQueryHandler(FeedQuery query, IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo)
    {
        public async Task<bool> Handle()
        {
            var animals = await animalRepo.GetTypeAnimals(query.Type);

            // Random double between 10 and 25
            var randomAmount = Math.Round((double)new Random().NextDouble(), 2) * (25 - 10) + 10;

            foreach (var animal in animals)
            {
                // Only if the animal is alive can it be fed and thus health raised
                if (animal.Health > 0)
                {
                    // 10-25% of the current health
                    var healthPercentage = randomAmount / 100 * animal.Health;

                    // Add the percentage to the current health
                    var updatedHealth = animal.Health + healthPercentage;

                    var healthModel = new UpdateAnimalHealthModel(animal.Id, updatedHealth);

                    await animalRepo.UpdateAnimalHealth(healthModel);
                }                
            }

            // If animals have been fed in this enclosure then mark this enclosure as fed this hour
            if (animals.Any(x => x.Health > 0))
            {
                var fedModel = new UpdateFedModel(query.Type, true);

                await enclosureRepo.UpdateFedThisHour(fedModel);
            }

            return true;
        }
    }
}
