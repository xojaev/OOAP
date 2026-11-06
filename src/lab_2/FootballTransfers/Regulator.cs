namespace FootballTransfers;

public class Regulator
{
    public string Name { get; }
    public bool RegistrationEnabled { get; }

    public Regulator(string name, bool registrationEnabled = true)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
        Name = name;
        RegistrationEnabled = registrationEnabled;
    }

    public TransferStatus Register(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);
        return RegistrationEnabled && transfer.Window.IsOpen(transfer.Date)
            ? TransferStatus.Registered
            : TransferStatus.Rejected;
    }
}
