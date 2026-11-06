using System.Data.Common;
using System.Text.Json;

namespace FootballTransfers;

public class DbTransferRepository : TransferRepository
{
    private readonly Func<DbConnection> connectionFactory;

    public DbTransferRepository(Func<DbConnection> connectionFactory)
    {
        this.connectionFactory = connectionFactory
            ?? throw new ArgumentNullException(nameof(connectionFactory));

        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Transfers (
                Id TEXT PRIMARY KEY,
                WindowId TEXT NOT NULL,
                TransferJson TEXT NOT NULL
            )
            """;
        command.ExecuteNonQuery();
    }

    public bool Save(Transfer transfer)
    {
        ArgumentNullException.ThrowIfNull(transfer);

        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = """
            INSERT INTO Transfers (Id, WindowId, TransferJson)
            VALUES (@Id, @WindowId, @TransferJson)
            """;
        AddParameter(command, "@Id", transfer.Id.ToString());
        AddParameter(command, "@WindowId", transfer.Window.Id.ToString());
        AddParameter(command, "@TransferJson", JsonSerializer.Serialize(transfer));
        return command.ExecuteNonQuery() == 1;
    }

    public List<Transfer> GetByWindow(TransferWindow window)
    {
        ArgumentNullException.ThrowIfNull(window);

        using var connection = OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT TransferJson FROM Transfers WHERE WindowId = @WindowId";
        AddParameter(command, "@WindowId", window.Id.ToString());

        var result = new List<Transfer>();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var transfer = JsonSerializer.Deserialize<Transfer>(reader.GetString(0))
                ?? throw new InvalidOperationException("Stored transfer could not be read.");
            result.Add(transfer);
        }

        return result;
    }

    private DbConnection OpenConnection()
    {
        var connection = connectionFactory()
            ?? throw new InvalidOperationException("Connection factory returned null.");
        try
        {
            connection.Open();
            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static void AddParameter(DbCommand command, string name, string value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value;
        command.Parameters.Add(parameter);
    }
}
