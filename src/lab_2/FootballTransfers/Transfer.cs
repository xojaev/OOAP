using System.Text.Json.Serialization;

namespace FootballTransfers;

public class Transfer
{
    public Guid Id { get; }
    public Player Player { get; }
    public Club? SellingClub { get; }
    public Club BuyingClub { get; }
    public decimal Price { get; }
    public TransferStatus Status { get; private set; }
    public TransferWindow Window { get; }
    public DateTime Date { get; }

    public Transfer(Player player, Club? sellingClub, Club buyingClub, decimal price,
        DateTime date, TransferWindow window)
        : this(Guid.NewGuid(), player, sellingClub, buyingClub, price,
            TransferStatus.PendingApproval, window, date) { }

    [JsonConstructor]
    public Transfer(Guid id, Player player, Club? sellingClub, Club buyingClub, decimal price,
        TransferStatus status, TransferWindow window, DateTime date)
    {
        if (id == Guid.Empty) throw new ArgumentException("Transfer ID cannot be empty.", nameof(id));
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));

        Id = id;
        Player = player ?? throw new ArgumentNullException(nameof(player));
        SellingClub = sellingClub;
        BuyingClub = buyingClub ?? throw new ArgumentNullException(nameof(buyingClub));
        Price = price;
        Status = status;
        Window = window ?? throw new ArgumentNullException(nameof(window));
        Date = date;
    }

    public bool SetStatus(TransferStatus status)
    {
        if (Status != TransferStatus.PendingApproval || status == TransferStatus.PendingApproval)
            return false;

        Status = status;
        return true;
    }
}
