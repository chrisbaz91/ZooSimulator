using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class PassTimeQueryHandler(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo)
    {
        public async Task<bool> Handle()
        {
            var enclosures = await enclosureRepo.GetEnclosures();
            var animals = await animalRepo.GetAnimals();

            foreach (var enclosure in enclosures)
            {
                // Only if the enclosure's animals haven't been recently fed do they lose health
                if (!enclosure.FedThisHour)
                {
                    foreach (var animal in animals.Where(x => x.Type == enclosure.Type))
                    {
                        // Random double between 0 and 20
                        var randomAmount = Math.Round((double)new Random().NextDouble(), 2) * 20;

                        // 0-20% of the current health
                        var healthPercentage = randomAmount / 100 * animal.Health;

                        // Remove the percentage from the current health
                        var updatedHealth = animal.Health - healthPercentage;

                        var model = new UpdateAnimalHealthModel(animal.Id, updatedHealth);

                        await animalRepo.UpdateAnimalHealth(model);
                    }
                }
                // If they had been fed in the past hour then now mark the enclosure as not fed for the proceeding hour
                else
                {
                    var fedModel = new UpdateFedModel(enclosure.Type, false);

                    await enclosureRepo.UpdateFedThisHour(fedModel);
                }
            }

            return true;
        }
    }
}
