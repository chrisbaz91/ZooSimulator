using ZooSimulator.DataAccess;
using ZooSimulator.Handlers;
using ZooSimulator.Models;

namespace ZooSimulatorIntegrationTests
{
    public class PassTimeQueryHandlerTests : IntegrationTest
    {
        private readonly AnimalRepository animalRepo;
        private readonly EnclosureRepository enclosureRepo;
        private readonly Elephant testElephant;
        private readonly Giraffe testGiraffe;
        private readonly Monkey testMonkey;
        private readonly Enclosure testElephantEnclosure;
        private readonly Enclosure testGiraffeEnclosure;
        private readonly Enclosure testMonkeyEnclosure;
        private readonly int startingHealth = 100;

        public PassTimeQueryHandlerTests()
        {
            animalRepo = new(context);
            enclosureRepo = new(context);
            testElephant = new("TestElephant", 10, Gender.Male)
            {
                Health = startingHealth
            };
            testGiraffe = new("TestGiraffe", 5, Gender.Female)
            {
                Health = startingHealth
            };
            testMonkey = new("TestMonkey", 2, Gender.Intersex)
            {
                Health = startingHealth
            };
            testElephantEnclosure = new(SpeciesType.Elephant, "");
            testGiraffeEnclosure = new(SpeciesType.Giraffe, "");
            testMonkeyEnclosure = new(SpeciesType.Monkey, "");
        }

        [Fact]
        public async Task Handle_AllFedAnimals_DoesNotDecreaseHealthForAnyAnimalsAndUpdatesAllEnclosuresToUnfed()
        {
            testElephantEnclosure.FedThisHour = true;
            testGiraffeEnclosure.FedThisHour = true;
            testMonkeyEnclosure.FedThisHour = true;

            var enclosures = new List<Enclosure>()
            {
                testElephantEnclosure,
                testGiraffeEnclosure,
                testMonkeyEnclosure
            };
            await InsertRangeAsync(enclosures);

            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var handler = new PassTimeQueryHandler(animalRepo, enclosureRepo);

            var result = await handler.Handle();

            var currentAnimals = await animalRepo.GetAnimals();

            var currentEnclosures = await enclosureRepo.GetEnclosures();

            Assert.True(result);

            foreach (var currentAnimal in currentAnimals)
            {
                Assert.False(currentAnimal.Health < startingHealth);
            }

            foreach (var currentEnclosure in currentEnclosures)
            {
                Assert.False(currentEnclosure.FedThisHour);
            }
        }

        [Fact]
        public async Task Handle_AllUnfedAnimals_DecreasesHealthForAllAnimalsAndAllEnclosuresRemainUnfed()
        {
            var enclosures = new List<Enclosure>()
            {
                testElephantEnclosure,
                testGiraffeEnclosure,
                testMonkeyEnclosure
            };
            await InsertRangeAsync(enclosures);

            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var handler = new PassTimeQueryHandler(animalRepo, enclosureRepo);

            var result = await handler.Handle();

            var currentAnimals = await animalRepo.GetAnimals();

            var currentEnclosures = await enclosureRepo.GetEnclosures();

            Assert.True(result);

            foreach (var currentAnimal in currentAnimals)
            {
                Assert.True(currentAnimal.Health < startingHealth);
            }

            foreach (var currentEnclosure in currentEnclosures)
            {
                Assert.False(currentEnclosure.FedThisHour);
            }
        }
    }
}
