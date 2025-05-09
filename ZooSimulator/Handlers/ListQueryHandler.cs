using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class ListQueryHandler(IEnclosureRepository enclosureRepo, IAnimalRepository animalRepo)
    {
        public async Task<ListModel> Handle()
        {
            var enclosures = await enclosureRepo.GetEnclosures();
            var animals = await animalRepo.GetAnimals();

            var model = new ListModel();

            foreach (var enclosure in enclosures)
            {
                model.Enclosures.Add
                (
                    new EnclosureModel()
                    {
                        Type = enclosure.Type,
                        FedThisHour = enclosure.FedThisHour,
                        Emoji = enclosure.Emoji,
                        Animals = animals
                                .Where(x => x.Type == enclosure.Type)
                                .Select(x => new ListItemModel()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Age = x.Age,
                                    Gender = x.Gender.ToString(),
                                    Health = x.Health
                                })
                    }
                );
            }

            return model;
        }
    }
}
