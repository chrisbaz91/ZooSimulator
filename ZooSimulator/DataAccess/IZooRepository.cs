using ZooSimulator.Models;

namespace ZooSimulator.DataAccess
{
    public interface IZooRepository
    {
        public Task<Zoo> GetZoo();

        public Task<bool> UpdateCurrentHour();

        public Task<bool> UpdateMoney(double moneyChange);

        public Task<bool> UpdateTicketPrice(bool increase);
    }
}
