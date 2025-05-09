using ZooSimulator.Models;
using ZooSimulator.ViewModels;

namespace ZooSimulator.DataAccess
{
    public interface IEnclosureRepository
    {
        public Task<List<Enclosure>> GetEnclosures();

        public Task<Enclosure> GetEnclosure(SpeciesType type);

        public Task<bool> UpdateFedThisHour(UpdateFedModel model);
    }
}
