using FootballTransfers;

var today = DateTime.UtcNow;
var window = new TransferWindow(today.AddDays(-1), today.AddDays(1));
var regulator = new Regulator("League");
var buyingClub = new Club("North FC", "England");
var sellingClub = new Club("South FC", "England");
var representative = new ClubRepresentative("Alex", buyingClub);

TransferRepository repository = new InMemoryTransferRepository();
var controller = new Controller(new TransferService(repository));

var playerWithClub = new Player("Sam", sellingClub,
    new Contract("Active", today.AddYears(1)), 25_000_000m);
playerWithClub.AcceptTerms(buyingClub, 20_000_000m);

var first = representative.SubmitTransfer(controller, playerWithClub,
    20_000_000m, today, window, regulator);

var freeAgent = new Player("Chris", null, null, 2_000_000m);
freeAgent.AcceptTerms(buyingClub, 0m);

var second = representative.SubmitTransfer(controller, freeAgent,
    0m, today, window, regulator);

Console.WriteLine($"{first.Player.Name}: {first.Status}");
Console.WriteLine($"{second.Player.Name}: {second.Status}");
Console.WriteLine($"Transfers in this window: {repository.GetByWindow(window).Count}");
