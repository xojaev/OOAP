using System.Text.Json.Serialization;

namespace FootballTransfers;

public class Player : Person
{
    public Club? CurrentClub { get; }
    public Contract? CurrentContract { get; }
    public decimal MarketValue { get; }
    public Guid? AcceptedBuyerId { get; private set; }
    public decimal? AcceptedPrice { get; private set; }

    public Player(string name, Club? currentClub, Contract? currentContract, decimal marketValue)
        : this(Guid.NewGuid(), name, currentClub, currentContract, marketValue, null, null) { }

    [JsonConstructor]
    public Player(Guid id, string name, Club? currentClub, Contract? currentContract,
        decimal marketValue, Guid? acceptedBuyerId, decimal? acceptedPrice) : base(id, name)
    {
        if (marketValue < 0) throw new ArgumentOutOfRangeException(nameof(marketValue));

        CurrentClub = currentClub;
        CurrentContract = currentContract;
        MarketValue = marketValue;
        AcceptedBuyerId = acceptedBuyerId;
        AcceptedPrice = acceptedPrice;
    }

    public void AcceptTerms(Club buyingClub, decimal price)
    {
        ArgumentNullException.ThrowIfNull(buyingClub);
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));

        AcceptedBuyerId = buyingClub.Id;
        AcceptedPrice = price;
    }

    public bool HasAcceptedTerms(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);
        return AcceptedBuyerId == transfer.BuyingClub.Id && AcceptedPrice == transfer.Price;
    }

    public override string GetRole() => "Player";
}
