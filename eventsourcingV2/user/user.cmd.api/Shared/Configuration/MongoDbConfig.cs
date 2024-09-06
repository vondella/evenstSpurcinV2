namespace user.cmd.api.Shared.Configuration;

public sealed class MongoDbConfig
{
    public string ConnectionString { get; init; }
    public string Database { get; init; }
    public string Collection { get; init; }
}