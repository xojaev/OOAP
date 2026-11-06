using System.Text.Json.Serialization;

namespace FootballTransfers;

public class TransferWindow
{
    public Guid Id { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public TransferWindow(DateTime startDate, DateTime endDate)
        : this(Guid.NewGuid(), startDate, endDate) { }

    [JsonConstructor]
    public TransferWindow(Guid id, DateTime startDate, DateTime endDate)
    {
        if (id == Guid.Empty) throw new ArgumentException("Window ID cannot be empty.", nameof(id));
        if (endDate < startDate) throw new ArgumentException("End date must not precede start date.", nameof(endDate));

        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }

    public bool IsOpen(DateTime date) => date >= StartDate && date <= EndDate;
}
