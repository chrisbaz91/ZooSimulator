using ZooSimulator.DataAccess;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulatorIntegrationTests
{
    public class EnclosureRepositoryTests : IntegrationTest
    {
        private readonly EnclosureRepository repo;
        private readonly Enclosure testElephantEnclosure;
        private readonly Enclosure testGiraffeEnclosure;
        private readonly Enclosure testMonkeyEnclosure;

        public EnclosureRepositoryTests()
        {
            repo = new(context);
            testElephantEnclosure = new(SpeciesType.Elephant, "");
            testGiraffeEnclosure = new(SpeciesType.Giraffe, "");
            testMonkeyEnclosure = new(SpeciesType.Monkey, "");
        }

        [Fact]
        public async Task GetEnclosures_NoEnclosures_ReturnsEmptyList()
        {
            var results = await repo.GetEnclosures();

            Assert.NotNull(results);
            Assert.Empty(results);
        }

        [Fact]
        public async Task GetEnclosures_EnclosuresExist_ReturnsAllEnclosures()
        {
            var enclosures = new List<Enclosure>()
            {
                testElephantEnclosure,
                testGiraffeEnclosure,
                testMonkeyEnclosure
            };
            await InsertRangeAsync(enclosures);

            var results = await repo.GetEnclosures();

            Assert.NotNull(results);
            Assert.Equal(enclosures.Count, results.Count);
        }

        [Fact]
        public async Task GetEnclosure_SearchedEnclosureDoesNotExist_ReturnsNull()
        {
            var enclosures = new List<Enclosure>()
            {
                testElephantEnclosure,
                testGiraffeEnclosure
            };
            await InsertRangeAsync(enclosures);

            var result = await repo.GetEnclosure(SpeciesType.Monkey);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEnclosure_EnclosuresExist_ReturnsCorrectEnclosure()
        {
            var enclosures = new List<Enclosure>()
            {
                testElephantEnclosure,
                testGiraffeEnclosure,
                testMonkeyEnclosure
            };
            await InsertRangeAsync(enclosures);

            var result = await repo.GetEnclosure(enclosures.First().Type);

            Assert.NotNull(result);
            Assert.Equal(enclosures.First().Id, result.Id);
            Assert.Equal(enclosures.First().Type, result.Type);
            Assert.Equal(enclosures.First().FedThisHour, result.FedThisHour);
        }

        [Fact]
        public async Task UpdateFedThisHour_True_SuccessfullyUpdatesCorrectEnclosure()
        {
            var enclosures = new List<Enclosure>()
            {
                testElephantEnclosure,
                testGiraffeEnclosure,
                testMonkeyEnclosure
            };
            await InsertRangeAsync(enclosures);

            var model = new UpdateFedModel(testGiraffeEnclosure.Type, true);

            var result = await repo.UpdateFedThisHour(model);

            var enclosure = await FindAsync<Enclosure>(testGiraffeEnclosure.Id);

            Assert.True(result);
            Assert.Equal(model.Type, enclosure.Type);
            Assert.Equal(model.Fed, enclosure.FedThisHour);
        }
    }
}
