namespace FootballTransfers;

public class ClubRepresentative : Person
{
    public Club Club { get; }

    public ClubRepresentative(string name, Club club) : base(Guid.NewGuid(), name)
    {
        Club = club ?? throw new ArgumentNullException(nameof(club));
    }

    public Transfer SubmitTransfer(Controller controller, Player player, decimal price,
        DateTime date, TransferWindow window, Regulator regulator)
    {
        ArgumentNullException.ThrowIfNull(controller);
        return controller.ProcessTransfer(this, player, price, date, window, regulator);
    }

    public override string GetRole() => "Club representative";
}
