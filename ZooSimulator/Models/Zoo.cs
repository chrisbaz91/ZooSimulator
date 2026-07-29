namespace ZooSimulator.Models
{
    public class Zoo : Entity
    {
        public Zoo(string name, int currentHour, double money, double ticketPrice)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (name.Length <= 0 || name.Length > 50)
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }
            if (ticketPrice < 0 || ticketPrice > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(ticketPrice));
            }

            Name = name;
            CurrentHour = currentHour;
            Money = money;
            TicketPrice = ticketPrice;
        }

        public string Name { get; set; }

        public int CurrentHour { get; set; }

        public double Money { get; set; }

        public double TicketPrice { get; set; }
    }
}