using System;
namespace FootballTransfer
{
    public class Club
    {
        public string Name { get; set; }
        public string Country { get; set; }

        public Club(string name, string country)
        {
            Name = name;
            Country = country;
        }
    }

 
    public class Player
    {
        public string Name { get; set; }
        public decimal MarketValue { get; set; }

        public Player(string name, decimal marketValue)
        {
            Name = name;
            MarketValue = marketValue;
        }
    }

    public class Contract
    {
        public string Status { get; set; }
        public DateTime ContractUntil { get; set; }

        public Player Signatory { get; set; }
        public Club Owner { get; set; }

        public Contract(
            string status,
            DateTime contractUntil,
            Player signatory,
            Club owner)
        {
            Status = status;
            ContractUntil = contractUntil;
            Signatory = signatory;
            Owner = owner;
        }
    }

    public class TransferWindow
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public TransferWindow(
            string name,
            DateTime startDate,
            DateTime endDate)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class Regulator
    {
        public string Name { get; set; }

        public Regulator(string name)
        {
            Name = name;
        }
    }

    public class Transfer
    {
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }


        public Club SellingClub { get; set; }
        public Club BuyingClub { get; set; }
        public Player Player { get; set; }
        public TransferWindow Window { get; set; }
        public Regulator RegisteringBody { get; set; }

        public Transfer(
            decimal price,
            string status,
            DateTime date,
            Club sellingClub,
            Club buyingClub,
            Player player,
            TransferWindow window,
            Regulator registeringBody)
        {
            Price = price;
            Status = status;
            Date = date;
            SellingClub = sellingClub;
            BuyingClub = buyingClub;
            Player = player;
            Window = window;
            RegisteringBody = registeringBody;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Club sellingClub = new Club(
                "North FC", "England");

            Club buyingClub = new Club(
                "United FC", "England");

            Player player = new Player(
                "Ivan Petrov", 12000000m);

            
            Contract currentContract = new Contract(
                "ACTIVE",
                new DateTime(2027, 6, 30),
                player,
                sellingClub);

            TransferWindow summerWindow = new TransferWindow(
                "Summer 2026",
                new DateTime(2026, 6, 1),
                new DateTime(2026, 8, 31));

            Regulator regulator = new Regulator(
                "Football Transfer Authority");

            Transfer transfer = new Transfer(
                10000000m,
                "REGISTERED",
                new DateTime(2026, 8, 15),
                sellingClub,
                buyingClub,
                player,
                summerWindow,
                regulator);

            Console.WriteLine("=== FOOTBALL TRANSFER ===");

            Console.WriteLine(
                $"Player: {transfer.Player.Name}");

            Console.WriteLine(
                $"Selling club: {transfer.SellingClub.Name}");

            Console.WriteLine(
                $"Buying club: {transfer.BuyingClub.Name}");

            Console.WriteLine(
                $"Transfer price: {transfer.Price} EUR");

            Console.WriteLine(
                $"Transfer status: {transfer.Status}");

            Console.WriteLine(
                $"Transfer date: {transfer.Date:dd.MM.yyyy}");

            Console.WriteLine(
                $"Contract status: {currentContract.Status}");

            Console.WriteLine(
                $"Contract owner: {currentContract.Owner.Name}");

            Console.WriteLine(
                $"Transfer window: {transfer.Window.Name}");

            Console.WriteLine(
                $"Registered by: {transfer.RegisteringBody.Name}");
        }
    }
}

