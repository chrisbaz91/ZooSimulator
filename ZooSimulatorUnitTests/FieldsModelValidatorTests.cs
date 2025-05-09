using ZooSimulator.Validators;
using ZooSimulator.ViewModels;
using FluentValidation.TestHelper;
using ZooSimulator.Models;

namespace ZooSimulatorUnitTests
{
    public class FieldsModelValidatorTests
    {
        private readonly FieldsModelValidator validator;
        private readonly CreateModel testAnimal;

        public FieldsModelValidatorTests()
        {
            validator = new();
            testAnimal = new()
            {
                Name = "TestAnimal",
                Age = 20,
                Gender = Gender.Male
            };
        }

        [Fact]
        public async Task Validate_ValidData_NoValidationErrors()
        {
            var results = await validator.TestValidateAsync(testAnimal);

            Assert.NotNull(results);
            Assert.True(results.IsValid);
            results.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_NameIsEmpty_ValidationErrorForName()
        {
            testAnimal.Name = "";

            var results = await validator.TestValidateAsync(testAnimal);

            Assert.NotNull(results);
            Assert.False(results.IsValid);
            results.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public async Task Validate_NameTooLong_ValidationErrorForName()
        {
            testAnimal.Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            var results = await validator.TestValidateAsync(testAnimal);

            Assert.NotNull(results);
            Assert.False(results.IsValid);
            results.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public async Task Validate_AgeTooLow_ValidationErrorForAge()
        {
            testAnimal.Age = -1;

            var results = await validator.TestValidateAsync(testAnimal);

            Assert.NotNull(results);
            Assert.False(results.IsValid);
            results.ShouldHaveValidationErrorFor(x => x.Age);
        }

        [Fact]
        public async Task Validate_AgeTooHigh_ValidationErrorForAge()
        {
            testAnimal.Age = 110;

            var results = await validator.TestValidateAsync(testAnimal);

            Assert.NotNull(results);
            Assert.False(results.IsValid);
            results.ShouldHaveValidationErrorFor(x => x.Age);
        }
    }
}
