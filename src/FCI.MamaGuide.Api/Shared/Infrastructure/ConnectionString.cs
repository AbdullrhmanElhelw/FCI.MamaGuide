namespace FCI.MamaGuide.Api.Shared.Infrastructure;

public sealed class ConnectionString
{
    public const string DefaultConnection = "DefaultConnection";

    public ConnectionString(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}