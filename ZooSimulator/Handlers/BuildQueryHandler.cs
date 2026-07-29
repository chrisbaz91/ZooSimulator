using Microsoft.AspNetCore.Mvc.Rendering;
using ZooSimulator.DataAccess;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.Handlers
{
    public class BuildQueryHandler(IEnclosureRepository repo)
    {
        public async Task<BuildModel> Handle()
        {
            var list = new List<SelectListItem>();

            foreach (var type in Enum.GetValues<SpeciesType>())
            {
                if (await repo.GetEnclosure(type) == null)
                {
                    list.Add(new SelectListItem()
                    {
                        Text = type.ToString(),
                        Value = type.ToString()
                    });
                }
            }

            return new BuildModel()
            {
                List = list
            };
        }
    }
}
