using ZooSimulator.DataAccess;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class BuildModelHandler(IEnclosureRepository repo)
    {
        public async Task<Guid> Handle(BuildModel model)
        {
            var emojiDictionary = new Dictionary<SpeciesType, string>()
            {
                [SpeciesType.Elephant] = "128024",
                [SpeciesType.Giraffe] = "129426",
                [SpeciesType.Monkey] = "128018",
                [SpeciesType.Rhino] = "129423",
                [SpeciesType.Tiger] = "128005",
                [SpeciesType.Lion] = "129409",
            };

            var enclosure = new Enclosure(model.Type, emojiDictionary[model.Type]);

            return await repo.AddEnclosure(enclosure);
        }
    }
}
