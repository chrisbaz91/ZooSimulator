using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class IndexQueryHandler(IEnclosureRepository enclosureRepo, IAnimalRepository animalRepo)
    {
        public async Task<IndexModel> Handle()
        {
            var listQueryHandler = new ListQueryHandler(enclosureRepo, animalRepo);

            var model = new IndexModel()
            {
                List = await listQueryHandler.Handle()
            };

            return model;
        }
    }
}
