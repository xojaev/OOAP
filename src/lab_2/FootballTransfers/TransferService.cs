namespace FootballTransfers;

public class TransferService
{
    private readonly TransferRepository repository;

    public TransferService(TransferRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public TransferStatus ProcessTransfer(ClubRepresentative representative, Player player,
        decimal price, DateTime date, TransferWindow window, Regulator regulator)
    {
        ArgumentNullException.ThrowIfNull(representative);
        ArgumentNullException.ThrowIfNull(player);
        ArgumentNullException.ThrowIfNull(window);
        ArgumentNullException.ThrowIfNull(regulator);

        var sellingClub = player.CurrentClub;
        var buyingClub = representative.Club;
        var transfer = new Transfer(player, sellingClub, buyingClub, price, date, window);

        bool valid = window.IsOpen(date)
            && (sellingClub is null || sellingClub.Id != buyingClub.Id)
            && player.HasAcceptedTerms(transfer)
            && (sellingClub is null || player.CurrentContract is null
                || player.CurrentContract.CheckValidity(date))
            && (sellingClub is null || sellingClub.ConfirmRelease(transfer))
            && buyingClub.ConfirmOffer(transfer);

        if (!valid)
        {
            transfer.SetStatus(TransferStatus.Rejected);
            return transfer.Status;
        }

        transfer.SetStatus(regulator.Register(transfer));
        if (transfer.Status == TransferStatus.Registered && !repository.Save(transfer))
            throw new InvalidOperationException("Registered transfer was not saved.");

        return transfer.Status;
    }
}
