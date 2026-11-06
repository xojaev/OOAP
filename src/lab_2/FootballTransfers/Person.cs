namespace FootballTransfers;

public abstract class Person
{
    public Guid Id { get; }
    public string Name { get; }

    protected Person(Guid id, string name)
    {
        if (id == Guid.Empty) throw new ArgumentException("Person ID cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));

        Id = id;
        Name = name;
    }

    public abstract string GetRole();
}
