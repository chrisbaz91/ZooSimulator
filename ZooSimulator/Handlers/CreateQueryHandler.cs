using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class CreateQueryHandler
    {
        public CreateModel Handle(CreateQuery query)
        {
            return new CreateModel()
            {
                Type = query.Type
            };
        }
    }
}
