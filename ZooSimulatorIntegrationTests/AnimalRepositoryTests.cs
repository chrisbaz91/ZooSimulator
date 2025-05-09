using ZooSimulator.DataAccess;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulatorIntegrationTests
{
    public class AnimalRepositoryTests : IntegrationTest
    {
        private readonly AnimalRepository repo;
        private readonly Elephant testElephant;
        private readonly Giraffe testGiraffe;
        private readonly Monkey testMonkey;

        public AnimalRepositoryTests()
        {
            repo = new(context);
            testElephant = new("TestElephant", 10, Gender.Male);
            testGiraffe = new("TestGiraffe", 5, Gender.Female);
            testMonkey = new("TestMonkey", 2, Gender.Intersex);
        }

        [Fact]
        public async Task GetAnimals_NoAnimals_ReturnsEmptyList()
        {
            var results = await repo.GetAnimals();

            Assert.NotNull(results);
            Assert.Empty(results);
        }

        [Fact]
        public async Task GetAnimals_AnimalsExist_ReturnsAllAnimals()
        {
            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var results = await repo.GetAnimals();

            Assert.NotNull(results);
            Assert.Equal(animals.Count, results.Count);
        }

        [Fact]
        public async Task GetAnimal_SearchedAnimalDoesNotExist_ReturnsNull()
        {
            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var result = await repo.GetAnimal(new Guid());

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAnimal_AnimalsExist_ReturnsCorrectAnimal()
        {
            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var result = await repo.GetAnimal(animals.First().Id);

            Assert.NotNull(result);
            Assert.Equal(animals.First().Id, result.Id);
            Assert.Equal(animals.First().Name, result.Name);
            Assert.Equal(animals.First().Age, result.Age);
            Assert.Equal(animals.First().Gender, result.Gender);
            Assert.Equal(animals.First().Health, result.Health);
        }

        [Fact]
        public async Task AnimalExists_SearchedAnimalDoesNotExist_ReturnsFalse()
        {
            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var result = await repo.AnimalExists(new Guid());

            Assert.False(result);
        }

        [Fact]
        public async Task AnimalExists_AnimalsExist_ReturnsTrue()
        {
            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var result = await repo.AnimalExists(animals.First().Id);

            Assert.True(result);
        }

        [Fact]
        public async Task AddAnimal_NewValidAnimal_SuccessfullyAddsNewAnimal()
        {
            await InsertAsync(testElephant);

            var result = await repo.AddAnimal(testGiraffe);

            var animal = await FindAsync<Animal>(result);

            Assert.Equal(testGiraffe.Id, result);
            Assert.Equal(testGiraffe.Id, animal.Id);
            Assert.Equal(testGiraffe.Name, animal.Name);
            Assert.Equal(testGiraffe.Age, animal.Age);
            Assert.Equal(testGiraffe.Gender, animal.Gender);
            Assert.Equal(testGiraffe.Health, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimal_ValidAnimalChanges_SuccessfullyUpdatesCorrectAnimal()
        {
            await InsertAsync(testElephant);

            var model = new EditModel()
            {
                Id = testElephant.Id,
                Name = "UpdatedName",
                Age = testElephant.Age + 1,
                Gender = Gender.Intersex
            };

            var result = await repo.UpdateAnimal(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.Equal(model.Name, animal.Name);
            Assert.Equal(model.Age, animal.Age);
            Assert.Equal(model.Gender, animal.Gender);
        }

        [Fact]
        public async Task RemoveAnimal_ValidAnimal_SuccessfullyRemovesCorrectAnimal()
        {
            var animals = new List<Animal>()
            {
                testElephant,
                testGiraffe,
                testMonkey
            };
            await InsertRangeAsync(animals);

            var result = await repo.RemoveAnimal(testElephant.Id);

            var removedAnimal = await FindAsync<Animal>(testElephant.Id);
            var currentAnimals = await repo.GetAnimals();

            Assert.True(result);
            Assert.Null(removedAnimal);
            Assert.Equal(animals.Count - 1, currentAnimals.Count);
        }

        [Fact]
        public async Task UpdateAnimalHealth_ValidHealthChange_SuccessfullyUpdatesCorrectAnimalHealth()
        {
            await InsertAsync(testElephant);

            var model = new UpdateAnimalHealthModel(testElephant.Id, 50);

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.Equal(model.Health, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_LessThanZeroHealthChange_SuccessfullyUpdatesCorrectAnimalHealthToZero()
        {
            await InsertAsync(testElephant);

            var model = new UpdateAnimalHealthModel(testElephant.Id, 10)
            {
                Health = -10
            };

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.Equal(0, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_MoreThan100HealthChange_SuccessfullyUpdatesCorrectAnimalHealthTo100()
        {
            await InsertAsync(testElephant);

            var model = new UpdateAnimalHealthModel(testElephant.Id, 100)
            {
                Health = 150
            };

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.Equal(100, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_GiraffeStarvationTypeHealthThresholdPassed_SuccessfullyUpdatesCorrectAnimalHealthToZero()
        {
            await InsertAsync(testGiraffe);

            var model = new UpdateAnimalHealthModel(testGiraffe.Id, 40);

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.Equal(0, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_MonkeyStarvationTypeHealthThresholdPassed_SuccessfullyUpdatesCorrectAnimalHealthToZero()
        {
            await InsertAsync(testMonkey);

            var model = new UpdateAnimalHealthModel(testMonkey.Id, 20);

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.Equal(0, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_ElephantWalkingTypeHealthThresholdPassed_SuccessfullyUpdatesCorrectAnimalWalkingToFalse()
        {
            await InsertAsync(testElephant);

            var model = new UpdateAnimalHealthModel(testElephant.Id, 60);

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.False(animal.Walking);
            Assert.Equal(model.Health, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_NonWalkingElephantWalkingTypeHealthThresholdReturned_SuccessfullyUpdatesCorrectAnimalWalkingToTrue()
        {
            testElephant.Walking = false;

            await InsertAsync(testElephant);

            var model = new UpdateAnimalHealthModel(testElephant.Id, 80);

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.True(animal.Walking);
            Assert.Equal(model.Health, animal.Health);
        }

        [Fact]
        public async Task UpdateAnimalHealth_NonWalkingElephantWalkingTypeHealthThresholdPassed_SuccessfullyUpdatesCorrectAnimalHealthToZero()
        {
            testElephant.Walking = false;

            await InsertAsync(testElephant);

            var model = new UpdateAnimalHealthModel(testElephant.Id, 60);

            var result = await repo.UpdateAnimalHealth(model);

            var animal = await FindAsync<Animal>(model.Id);

            Assert.True(result);
            Assert.Equal(model.Id, animal.Id);
            Assert.False(animal.Walking);
            Assert.Equal(0, animal.Health);
        }
    }
}
