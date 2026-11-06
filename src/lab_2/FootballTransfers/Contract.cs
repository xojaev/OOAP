namespace FootballTransfers;

public class Contract
{
    public string Status { get; }
    public DateTime ContractUntil { get; }

    public Contract(string status, DateTime contractUntil)
    {
        if (string.IsNullOrWhiteSpace(status)) throw new ArgumentException("Status is required.", nameof(status));

        Status = status;
        ContractUntil = contractUntil;
    }

    public bool CheckValidity(DateTime date) =>
        string.Equals(Status, "Active", StringComparison.OrdinalIgnoreCase)
        && date <= ContractUntil;
}
