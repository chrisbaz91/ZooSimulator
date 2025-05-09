using ZooSimulator.DataAccess;
using ZooSimulator.Handlers;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulatorIntegrationTests
{
    public class FeedQueryHandlerTests : IntegrationTest
    {
        private readonly AnimalRepository animalRepo;
        private readonly EnclosureRepository enclosureRepo;

        public FeedQueryHandlerTests()
        {
            animalRepo = new(context);
            enclosureRepo = new(context);
        }

        [Fact]
        public async Task Handle_ValidSpeciesTypeButHealthIsZero_DoesNotFeedAnySpeciesAnimalsAndDoesNotUpdateEnclosureToFed()
        {
            var startingHealth = 0;

            var enclosure = new Enclosure(SpeciesType.Elephant, "");
            await InsertAsync(enclosure);

            var animals = new List<Elephant>()
            {
                new("TestElephant1", 1, Gender.Male)
                {
                    Health = startingHealth
                },
                new("TestElephant2", 1, Gender.Male)
                {
                    Health = startingHealth
                },
                new("TestElephant3", 1, Gender.Male)
                {
                    Health = startingHealth
                }
            };
            await InsertRangeAsync(animals);

            var query = new FeedQuery()
            {
                Type = SpeciesType.Elephant
            };

            var handler = new FeedQueryHandler(query, animalRepo, enclosureRepo);

            var result = await handler.Handle();

            var currentEnclosure = await enclosureRepo.GetEnclosure(SpeciesType.Elephant);

            var currentAnimals = await animalRepo.GetTypeAnimals(SpeciesType.Elephant);

            Assert.True(result);

            Assert.False(currentEnclosure.FedThisHour);

            foreach (var currentAnimal in currentAnimals)
            {
                Assert.False(currentAnimal.Health > startingHealth);
            }
        }

        [Fact]
        public async Task Handle_ValidSpeciesType_SuccessfullyFeedsAllSpeciesAnimalsAndUpdatesEnclosureToFed()
        {
            var startingHealth = 75;

            var enclosure = new Enclosure(SpeciesType.Elephant, "");
            await InsertAsync(enclosure);

            var animals = new List<Elephant>()
            {
                new("TestElephant1", 1, Gender.Male)
                {
                    Health = startingHealth
                },
                new("TestElephant2", 1, Gender.Male)
                {
                    Health = startingHealth
                },
                new("TestElephant3", 1, Gender.Male)
                {
                    Health = startingHealth
                }
            };
            await InsertRangeAsync(animals);

            var query = new FeedQuery()
            {
                Type = SpeciesType.Elephant
            };

            var handler = new FeedQueryHandler(query, animalRepo, enclosureRepo);

            var result = await handler.Handle();

            var currentEnclosure = await enclosureRepo.GetEnclosure(SpeciesType.Elephant);

            var currentAnimals = await animalRepo.GetTypeAnimals(SpeciesType.Elephant);

            Assert.True(result);

            Assert.True(currentEnclosure.FedThisHour);

            foreach (var currentAnimal in currentAnimals)
            {
                Assert.True(currentAnimal.Health > startingHealth);
            }
        }
    }
}
