using System.Text.Json.Serialization;

namespace FootballTransfers;

public class Club
{
    public Guid Id { get; }
    public string Name { get; }
    public string Country { get; }
    public bool AllowsRelease { get; }
    public bool AllowsOffer { get; }

    public Club(string name, string country, bool allowsRelease = true, bool allowsOffer = true)
        : this(Guid.NewGuid(), name, country, allowsRelease, allowsOffer) { }

    [JsonConstructor]
    public Club(Guid id, string name, string country, bool allowsRelease, bool allowsOffer)
    {
        if (id == Guid.Empty) throw new ArgumentException("Club ID cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Club name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country is required.", nameof(country));

        Id = id;
        Name = name;
        Country = country;
        AllowsRelease = allowsRelease;
        AllowsOffer = allowsOffer;
    }

    public bool ConfirmRelease(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);
        return AllowsRelease && transfer.SellingClub?.Id == Id;
    }

    public bool ConfirmOffer(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);
        return AllowsOffer && transfer.BuyingClub.Id == Id;
    }
}
