using Microsoft.EntityFrameworkCore;
using ZooSimulator.Models;

namespace ZooSimulator.DataAccess
{
    public class ZooRepository(ZooContext context) : IZooRepository
    {
        public async Task<Zoo> GetZoo()
        {
            return await context.Zoos.SingleOrDefaultAsync();
        }

        public async Task<bool> UpdateCurrentHour()
        {
            var zoo = await GetZoo();

            zoo.CurrentHour++;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateMoney(double moneyChange)
        {
            var zoo = await GetZoo();

            zoo.Money += moneyChange;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateTicketPrice(bool increase)
        {
            var zoo = await GetZoo();

            zoo.TicketPrice = increase ? zoo.TicketPrice + 0.25 : zoo.TicketPrice - 0.25;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
