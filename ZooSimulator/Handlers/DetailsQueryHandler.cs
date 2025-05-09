using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class DetailsQueryHandler(IAnimalRepository repo)
    {
        public async Task<DetailsModel> Handle(DetailsQuery query)
        {
            var animal = await repo.GetAnimal(query.Id);

            return new DetailsModel()
            {
                Id = animal.Id,
                Type = animal.Type,
                Name = animal.Name,
                Age = animal.Age,
                Gender = animal.Gender,
                Health = animal.Health
            };
        }
    }
}
