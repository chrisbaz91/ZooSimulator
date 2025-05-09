using ZooSimulator.Models;

namespace ZooSimulator.DataAccess
{
    public class SeedData
    {
        public static async Task SetInitialData()
        {
            var elephants = new List<Elephant>()
            {
                new("Chris", 10, Gender.Male),
                new("Ellie", 8, Gender.Female),
                new("Eric", 5, Gender.Intersex),
                new("Edward", 12, Gender.Male),
                new("Elvis", 15, Gender.Unknown)
            };

            var giraffes = new List<Giraffe>()
            {
                new("Chris", 6, Gender.Male),
                new("Gemma", 7, Gender.Female),
                new("George", 3, Gender.Intersex),
                new("Geoff", 9, Gender.Male),
                new("Genevieve", 5, Gender.Unknown)
            };

            var monkeys = new List<Monkey>()
            {
                new("Chris", 3, Gender.Male),
                new("Molly", 1, Gender.Female),
                new("Mike", 2, Gender.Intersex),
                new("Malcolm", 6, Gender.Male),
                new("Michelle", 4, Gender.Unknown)
            };

            var elephantEnclosure = new Enclosure(SpeciesType.Elephant, "128024");
            var giraffeEnclosure = new Enclosure(SpeciesType.Giraffe, "129426");
            var monkeyEnclosure = new Enclosure(SpeciesType.Monkey, "128018");

            using var context = new ZooContext();

            await context.Animals.AddRangeAsync(elephants);
            await context.Animals.AddRangeAsync(giraffes);
            await context.Animals.AddRangeAsync(monkeys);

            await context.Enclosures.AddRangeAsync(elephantEnclosure, giraffeEnclosure, monkeyEnclosure);

            await context.SaveChangesAsync();
        }
    }
}
