using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class EditModelHandler(IAnimalRepository repo)
    {
        public async Task<bool> Handle(EditModel model)
        {
            try
            {
                await repo.UpdateAnimal(model);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
