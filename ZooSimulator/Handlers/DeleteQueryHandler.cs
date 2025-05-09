using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class DeleteQueryHandler(IAnimalRepository repo)
    {
        public async Task<bool> Handle(DeleteQuery query)
        {
            try
            {
                await repo.RemoveAnimal(query.Id);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
