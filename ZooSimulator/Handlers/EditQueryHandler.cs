using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class EditQueryHandler(IAnimalRepository repo)
    {
        public async Task<EditModel> Handle(EditQuery query)
        {
            var animal = await repo.GetAnimal(query.Id);

            return new EditModel()
            {
                Id = animal.Id,
                Type = animal.Type,
                Name = animal.Name,
                Age = animal.Age,
                Gender = animal.Gender
            };
        }
    }
}
