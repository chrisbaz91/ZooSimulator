using ZooSimulator.DataAccess;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class CreateModelHandler(IAnimalRepository repo)
    {
        public async Task<Guid> Handle(CreateModel model)
        {
            var animal = CreateAnimal/*<Elephant>*/(model);

            return await repo.AddAnimal(animal);
        }

        private static Animal CreateAnimal/*<T>*/(CreateModel model)/* where T : Animal, new()*/
        {
            return model.Type switch
            {
                SpeciesType.Elephant => new Elephant(
                                        model.Name,
                                        model.Age,
                                        model.Gender
                                        ),
                SpeciesType.Giraffe => new Giraffe(
                                        model.Name,
                                        model.Age,
                                        model.Gender
                                        ),
                SpeciesType.Monkey => new Monkey(
                                        model.Name,
                                        model.Age,
                                        model.Gender
                                        ),
                _ => new Elephant(
                                        model.Name,
                                        model.Age,
                                        model.Gender
                                        ),
            };

            //return new T()
            //{
            //    Name = model.Name,
            //    Age = model.Age,
            //    Gender = model.Gender
            //};
        }
    }
}
