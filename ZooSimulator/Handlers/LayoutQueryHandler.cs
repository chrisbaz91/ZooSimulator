using ZooSimulator.DataAccess;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class LayoutQueryHandler(IZooRepository zooRepo)
    {
        public async Task<LayoutModel> Handle()
        {
            var zoo = await zooRepo.GetZoo();

            var model = new LayoutModel()
            {
                Money = zoo.Money,
                TicketPrice = zoo.TicketPrice,
                CurrentHour = zoo.CurrentHour
            };

            return model;
        }
    }
}
