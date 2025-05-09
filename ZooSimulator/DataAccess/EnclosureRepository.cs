using Microsoft.EntityFrameworkCore;
using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.DataAccess
{
    public class EnclosureRepository(ZooContext context) : IEnclosureRepository
    {
        public async Task<List<Enclosure>> GetEnclosures()
        {
            return await context.Enclosures.ToListAsync();
        }

        public async Task<Enclosure> GetEnclosure(SpeciesType type)
        {
            return await context.Enclosures.Where(x => x.Type == type).SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateFedThisHour(UpdateFedModel model)
        {
            var enclosure = await GetEnclosure(model.Type);

            enclosure.FedThisHour = model.Fed;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
