namespace FootballTransfers;

public class Controller
{
    private readonly TransferService service;

    public Controller(TransferService service)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public Transfer ProcessTransfer(ClubRepresentative representative, Player player,
        decimal price, DateTime date, TransferWindow window, Regulator regulator)
    {
        return service.ProcessTransfer(representative, player, price, date, window, regulator);
    }
}
