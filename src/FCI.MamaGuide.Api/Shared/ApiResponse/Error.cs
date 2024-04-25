namespace FCI.MamaGuide.Api.Shared.ApiResponse;

public sealed record Error
    (string Message)
{
    public string Message { get; } = Message;
    public static readonly Error None = new(string.Empty);
    public static implicit operator Error(string message) => new(message);
    public static implicit operator string(Error error) => error.Message;
}