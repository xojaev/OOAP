namespace FootballTransfers;

public interface TransferRepository
{
    bool Save(Transfer transfer);
    List<Transfer> GetByWindow(TransferWindow window);
}
