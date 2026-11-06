namespace FootballTransfers;

public class InMemoryTransferRepository : TransferRepository
{
    private readonly Dictionary<Guid, Transfer> transfers = new();

    public bool Save(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);
        return transfers.TryAdd(transfer.Id, transfer);
    }

    public List<Transfer> GetByWindow(TransferWindow window)
    {
        ArgumentNullException.ThrowIfNull(window);
        return transfers.Values.Where(transfer => transfer.Window.Id == window.Id).ToList();
    }
}
